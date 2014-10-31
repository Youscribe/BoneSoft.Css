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

    public class Whith_quoted_directive_charset : And_ParseText
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

    public class Whith_rule_containing_content_with_empty_value : And_ParseText
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

    public class Whith_rule_containing_content_with_quoted_value : And_ParseText
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

    public class Whith_rule_containing_important_class : And_ParseText
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

    public class Whith_directive_containing_quoted_value_and_url : And_ParseText
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
}