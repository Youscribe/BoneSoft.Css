using BoneSoft.CSS;
using NBehave.Spec.Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CssParserTests
{
    public class Specification : SpecBase
    {
    }

    public class While_use_CSSParser : Specification
    {
        protected string _inputCss, _outputCss;
        protected CSSDocument _document;

        protected CSSParser parser;

        protected override void Establish_context()
        {
            parser = new CSSParser();
        }
    }

    public class And_ParseText : While_use_CSSParser
    {
        protected string textToParse;
        protected CSSDocument document;

        protected override void Establish_context()
        {
            base.Establish_context();
        }

        protected override void Because_of()
        {
            document = parser.ParseText(textToParse);
        }
    }

    public class With_quoted_directive_charset : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"@charset ""UTF-8"";";
        }

        [Fact]
        public void Then_I_should_get_one_directive_and_text_should_contains_quoted_charset()
        {
            document.Directives.Count.ShouldEqual(1);
            document.ToString().ShouldContain(@"@charset ""UTF-8"";");
        }
    }

    public class With_rule_containing_content_with_empty_value : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"p.test:after { content:'' }";
        }

        [Fact]
        public void Then_I_should_get_one_rule_and_text_should_contains_content_with_empty_value()
        {
            document.RuleSets.Count.ShouldEqual(1);
            document.ToString().ShouldContain(@"content: ''");
        }
    }

    public class With_rule_containing_content_with_quoted_value : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"p.test:after toto { content:""\2014 \0020""; }";
        }

        [Fact]
        public void Then_I_should_get_one_rule_and_text_should_contains_content_with_quoted_value()
        {
            document.RuleSets.Count.ShouldEqual(1);
            document.ToString().ShouldContain(@"content: ""\2014 \0020"";");
        }
    }

    public class With_rule_containing_important_class : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"div.sidebar div.tip, div.sidebar div.note, div.sidebar div.warning,
div.sidebar div.caution, div.sidebar div.important {
  margin: 10px 12.5% !important;
  font-size: 90%;
  padding: 10px 5px !important;
  width: 75%;
}";
        }

        [Fact]
        public void Then_I_should_get_one_rule_and_text_should_contains_important_class()
        {
            document.RuleSets.Count.ShouldEqual(1);
            document.ToString().ShouldContain(@"div.sidebar div.important");
        }
    }

    public class With_Epub_3_rules : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @" *[epub|type='pagebreak'] {
                             display: none;
                             }
                             a[epub|type='noteref'] {
                             -webkit-text-fill-color: black;
                             text-decoration: none;
                             color: #000000;
                             }
                             a.noteref {
                             vertical-align: super;
                             font-size:70%;
                             text-decoration: none;
                             -webkit-text-fill-color: black;
                             color: #000000;
                             }";
        }

        [Fact]
        public void Then_rules_should_be_parsed()
        {
            document.RuleSets.Count.ShouldEqual(3);
            document.ToString().ShouldContain(@"a.noteref");
            document.ToString().ShouldContain(@"a[epub|type='noteref']");
            document.ToString().ShouldContain(@"*[epub|type='pagebreak']");
        }
    }

    public class With_trailing_commas : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @".image_right_in_paragraph, .image_right_in_paragraph_net, .image_right_in_paragraph_text_net, , ,{
		                    float: right;
		                    margin-left: 0.7em;
		                    margin-top: 1em;
		                    margin-bottom: 1em;
		                    max-height: 250px;
		                    max-width: 250px;
	        }";
        }

        [Fact]
        public void Then_remove_trailing_commas()
        {
            document.RuleSets.SelectMany(s => s.Selectors).Count().ShouldEqual(3);
        }
    }

    public class With_selector_with_invalid_trialing_dot : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"span. {font-family:"""";font-size:100%;} span.{font-family:"""";font-size:100%;}";
        }

        [Fact]
        public void Then_remove_trailing_dot()
        {
            document.RuleSets.SelectMany(s => s.Selectors).Count().ShouldEqual(2);
            var selectorNames = document.RuleSets.SelectMany(s => s.Selectors).SelectMany(s => s.SimpleSelectors).Select(s => s.ElementName).Distinct().ToArray();
            selectorNames.Length.ShouldEqual(1);
            selectorNames[0].ShouldEqual("span");
        }
    }

    public class With_invalid_semicolons : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @".s_rem {
	                                margin: 20pt;
	                                border: solid 1px #6269BD;	/* livre */;
	                                padding: 5pt;
	                                background-color: rgb(245,245,245);
                                    ;;
	                                font-size: 0.9em;
	                                /*width: 75%;*/
	                                line-height: 1.4em;
                                    }
	        }";
        }

        [Fact]
        public void Then_remove_empty_expressions()
        {
            document.RuleSets.SelectMany(s => s.Declarations).Select(s => s.Expression).Count().ShouldEqual(6);
        }
    }

    public class With_not_pseudo_class_combined_with_first_of_type_pseudo_class : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @".chap p:not(:first-of-type){
                            	font-size:xx-large;
                            	text-align:center;
                            	font-weight:bold;
                            }
                            .droite {text-align: right;
                            margin-right: 1em;}
	        ";
        }

        [Fact]
        public void Then_parse_should_work()
        {
            document.RuleSets.Count.ShouldEqual(2);
            document.ToString().ShouldContain(@".chap p:not(:first-of-type)");
        }
    }

    public class With_directive_containing_quoted_value_and_url_without_quote : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"@font-face { font-family: ""Free Serif""; font-style: normal; font-weight: normal;  src: url(FreeSerif.otf); }}";
        }

        [Fact]
        public void Then_I_should_get_one_directive_and_text_should_contains_quoted_value_and_url_with_quote()
        {
            document.Directives.Count.ShouldEqual(1);
            document.ToString().ShouldContain(@"@font-face");
            document.ToString().ShouldContain(@"src: url('FreeSerif.otf')");
            document.ToString().ShouldContain(@"font-family: ""Free Serif""");
        }
    }

    public class With_directive_containing_quoted_url : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"@font-face {src: url('FreeSerif.otf'); }}";
        }

        [Fact]
        public void Then_I_should_get_one_directive_and_text_should_contains_quoted_url()
        {
            document.Directives.Count.ShouldEqual(1);
            document.ToString().ShouldContain(@"src: url('FreeSerif.otf')");
        }
    }

    public class With_directive_containing_double_quoted_url : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"@font-face {src: url(""FreeSerif.otf""); }}";
        }

        [Fact]
        public void Then_I_should_get_one_directive_and_text_should_contains_quoted_url()
        {
            document.Directives.Count.ShouldEqual(1);
            document.ToString().ShouldContain(@"src: url('FreeSerif.otf')");
        }
    }
}