using BoneSoft.CSS;
using NBehave.Spec.Xunit;
using Xunit;
using XunitShould;
using System.Linq; 

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

    public class With_Important_Flag : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"body{
margin-left: 2em;
margin-right: 2em;
text-align:justify;
}
a
{
text-decoration: none;
}
p.cover{
margin:0%;
padding:0%;
text-align:center;
}
img
{
max-width: 100% !Important;
}
smalla
{
font-size:0.9em;
}
.fpage{
page-break-after: always;
}
.u
{
text-decoration:underline;
}
span.gray
{
color:#808285;
}
.bctitle{
font-size:160%;
margin-top:5%;
margin-bottom:0.2%;
text-align:center;
color:#6658a6;
font-weight:normal;
margin-right:0em;
margin-left:0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
.bctitl-e{
font-size:160%;
margin-top:0.5%;
margin-bottom:1em;
text-align:center;
color:#6658a6;
font-weight:bold;
margin-right:0em;
margin-left:0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}

p.bcstitle
{
font-size:100%;
margin-top:0em;
margin-bottom:2em;
text-align:left;
font-weight:normal;
margin-right:0em;
margin-left:0em;
}
.noindent{
margin-top:0.5em;
margin-bottom:0.5em;
margin-left:1.5em;
text-align:justify;
}
.noinden-t{
margin-top:0.5em;
margin-bottom:0.5em;
margin-left:1.5em;
text-align:left;
font-family:sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
.noinden-tt{
margin-top:0.5em;
margin-bottom:0.5em;
margin-left:1.5em;
text-align:justify;
font-family:sans-serif;
font-size:90%;
}
.noindentz
{
font-size:110%;
margin-top:0em;
margin-bottom:0.3em;
margin-left:1.5em;
text-align:justify;
}
span.vtext{
font-size:100%;
margin-top:0em;
margin-bottom:0.3em;
text-align:left;
margin-left:0em;
padding-left:0.2em;
background-color:#ece8f1;
color:#7e64af;
}
span.bb{
float:left;
padding-right:0.5em;
}
.btext
{
font-size:100%;
margin-top:0em;
margin-bottom:2em;
text-align:left;
margin-left:3em;
}
.boxa
{
	font-size:80%;
	margin-top:0em;
	margin-bottom:2em;
	text-align:left;
	margin-left:3em;
	text-indent:-0em;
	background-color:#ece8f1;
	color:#7e64a4;
}
.boxa-b
{
	font-size:80%;
	margin-top:0em;
	margin-bottom:0em;
	text-align:left;
	margin-left:3em;
	text-indent:-0em;
	background-color:#ece8f1;
	color:#7e64a4;
}
.boxaa
{
font-size:80%;
margin-top:0em;
margin-bottom:0em;
text-align:left;
margin-left:3em;
text-indent:-1.3em;
}
.boxaa-b{
font-size:80%;
margin-top:0em;
margin-bottom:2em;
text-align:left;
margin-left:3em;
text-indent:-1.3em;
}
.boxaaa{
font-size:80%;
margin-top:0em;
margin-bottom:2em;
text-align:left;
margin-left:3em;
text-indent:-1.3em;
}
.bodytext{
text-align: justify;
margin-top: 0.5em;
margin-bottom: 0.5em;
margin-left:1.5em;
text-indent:1.4em;
}
.bodytext1{
margin-left:1.6em;
font-size:100%;
margin-top:0em;
margin-bottom:1em;
text-indent:1.5em;
text-align:justify;
}
.bodytexts{
margin-left:1.6em;
margin-top:0em;
margin-bottom:2em;
text-indent:1.4em;
text-align:justify;
}
.bodytext-s{
margin-left:1.6em;
margin-top:1.5em;
margin-bottom:2em;
text-indent:1.4em;
text-align:justify;
}
.noindentL
{
font-size:100%;
margin-top:0em;
margin-bottom:0.5em;
margin-left:1em;
text-align:justify;
}
.noindentL1
{
font-size:100%;
margin-top:0em;
margin-bottom:0.5em;
text-align:right;
}
.noindent1
{
font-size:100%;
margin-top:0em;
margin-bottom:0em;
text-align:justify;
}
.noindents{
margin-left:1.5em;
margin-top:0em;
margin-bottom:1em;
text-align:justify;
}
.noindentss
{
font-size:80%;
margin-left:1.5em;
margin-top:1em;
margin-bottom:2em;
text-align:justify;
}
.noindents1
{
font-size:100%;
margin-top:0em;
margin-bottom:2em;
text-align:justify;
}
li.l1{
font-size:80%;
margin-top:0.3em;
margin-bottom:0.2em;
margin-left:0em;
font-family:serif;
}
li.l2
{
font-size:80%;
margin-top:0.3em;
margin-bottom:2em;
margin-left:0em;
font-family:serif;
}
.fmimage
{
font-size:120%;
margin-top:1em;
margin-bottom:1em;
text-align:left;
font-weight:normal;
margin-right:0em;
margin-left:0em;
}
.copy
{
font-size:85%;
margin-top:0em;
margin-bottom:2em;
text-align:justify;
text-indent:0em;
}
.copy-1
{
font-size:85%;
margin-top:1em;
margin-bottom:0em;
text-align:center;
}
.copyt
{
font-size:85%;
margin-top:3em;
margin-bottom:2em;
text-align:center;
}
.copy1{
font-size:85%;
margin-top:0.8em;
margin-bottom:15em;
text-align:center;
}
.copy-11{
font-size:85%;
margin-top:0.8em;
margin-bottom:0em;
text-align:center;
}
.author{
color:#87888a;
font-size:140%;
margin-top:1em;
margin-bottom:3em;
text-align:center;
font-weight:normal;
margin-right:0em;
margin-left:0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
.title{
font-size:270%;
margin-top:0em;
margin-bottom:3.5em;
text-align:center;
font-weight:normal;
margin-right:0em;
margin-left:0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
.centerimage{
margin-top:0.5em;
margin-bottom:0.5em;
text-align:center;
}
.centerima-ge{
margin-top:0em;
margin-bottom:0em;
text-align:center;
}
.leftimage{
margin-top:0em;
margin-bottom:0.5em;
text-align:left;
}
.centerimage1{
margin-top:0.5em;
margin-bottom:1em;
text-align:center;
}
.centerimage2{
margin-top:0.5em;
margin-bottom:2em;
text-align:center;
}
.fmtitle{
font-size:160%;
margin-top:1em;
margin-bottom:2em;
color:#7e64a4;
text-align:center;
font-weight:normal;
margin-right:0em;
margin-left:0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
.fmtitle1{
font-size:245%;
margin-top:1em;
margin-bottom:2em;
color:#7e64a4;
text-align:left;
font-weight:normal;
margin-right:0em;
margin-left:0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
.fmtitle2{
font-size:150%;
margin-top:4em;
margin-bottom:2em;
color:#7e64a4;
text-align:center;
font-weight:bold;
margin-right:0em;
margin-left:0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}

div.sty{
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
span.sup {
font-size: 70%;
vertical-align:super;
}
span.sup1 {
vertical-align:super;
}
span.sub {
vertical-align:bottom;
font-size:70%;
}
small {
font-size:0.7em;
}
.toc
{
font-family:sans-serif;
font-size:100%;
margin-top:0.7em;
margin-bottom:0em;
text-align:left;
font-weight:normal;
margin-right:0em;
margin-left:0em;
}
.toc-s{
font-size:90%;
margin-top:0.3em;
margin-bottom:0.3em;
text-align:left;
font-weight:normal;
margin-right:0em;
margin-left:1.5em;
text-indent:0em;
}
.toc-p{
font-family:sans-serif;
font-size:110%;
margin-top:2em;
margin-bottom:0.7em;
text-align:center;
font-weight:normal;
margin-right:0em;
margin-left:0em;
}
.toc-c{
font-family:sans-serif;
font-size:105%;
margin-top:1em;
margin-bottom:0.3em;
text-align:left;
font-weight:bold;
margin-right:0em;
margin-left:5.25em;
text-indent:-5.25em;
}
h1.h1{
color:#7e64a4;
font-size:130%;
margin-top:1em;
margin-bottom:0.7em;
text-align:left;
margin-left:0em;
text-indent:0em;
font-weight:bold;
text-indent:-1.3em;
margin-left:1.3em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
h1.h2{
color:#7e64a4;
font-size:130%;
margin-top:1em;
margin-bottom:0.7em;
text-align:left;
margin-left:0em;
text-indent:0em;
font-weight:bold;
text-indent:-1.8em;
margin-left:1.8em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
h1.h3{
color:#7e64a4;
font-size:130%;
margin-top:1em;
margin-bottom:0.7em;
text-align:left;
margin-left:0em;
text-indent:0em;
font-weight:bold;
text-indent:0em;
margin-left:0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}

h2.h2{
font-size:120%;
margin-top:0em;
margin-bottom:0.5em;
text-align:left;
font-weight:bold;
color:#7e64a4;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
ul.bull {
list-style-image: url(images/square.jpg);
font-size:100%;
margin-left: 2.9em;
margin-top:0.3em;
margin-bottom:0.3em;
padding: 0%;
}
ul.bull li {
margin-top:0.3em;
margin-bottom:0.3em;
}


ul.bull-e{
font-size:90%;
margin-top:0.5em;
margin-bottom:0.5em;
margin-left:1.4em;
font-style:normal;
color:#58585a;
padding:0%;
text-align:justify;
font-family:sans-serif;
list-style-image:url(images/bull-e.jpg);
}
ul.bull-e li {
margin-top:0.3em;
margin-bottom:0.3em;
}


ul.bullv{
font-size:95%;
margin-top:0.5em;
margin-bottom:0.5em;
margin-left:2.8em;
font-style:normal;
padding:0%;
text-align:justify;
font-family:sans-serif;
list-style-image:url(images/bullv.jpg);
}
ul.bullv li {
margin-top:0.3em;
margin-bottom:0.3em;
}


h5.h5
{
font-size:95%;
margin-top:0em;
margin-bottom:0.3em;
text-align:left;
font-weight:bold;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
.h5-1
{
font-size:90%;
margin-top:0em;
margin-bottom:0.3em;
text-align:center;
font-weight:bold;
color:#6d6e71;
}
.h5a
{
font-size:105%;
margin-top:0em;
margin-bottom:0.3em;
text-align:left;
font-weight:bold;
color:#6d6e71;
}
.hang1
{
font-size:80%;
margin-top:0em;
margin-bottom:0em;
margin-left:3.5em;
text-indent:-2em;
text-align:justify;
}
.hanga
{
	font-size:80%;
	margin-top:0em;
	margin-bottom:0em;
	margin-left:4em;
	text-indent:-1.8em;
	text-align:justify;
}
.hang2
{
margin-top:0em;
margin-bottom:0.5em;
margin-left:3.5em;
text-indent:-2em;
text-align:justify;
}
.hangs
{
margin-top:0em;
margin-bottom:2em;
margin-left:3.5em;
text-indent:-2em;
text-align:justify;
}
.fnotet
{
font-size:90%;
text-indent:-1em;
margin-top:1.5em;
margin-left:1em;
margin-right:0em;
margin-bottom:0em;
text-align:justify;
}
span.fff
{
	padding-right:1em;
}
span.fff0 {
padding-right:0.5em;
}
span.fff1 {
padding-right:2em;
}
.fnote
{
font-size:90%;
margin-left:2.5em;
text-indent:-2em;
margin-top:0.2em;
margin-bottom:0em;
text-align:justify;
}
.fnote1
{
font-size:90%;
margin-left:2.5em;
text-indent:-2.5em;
margin-top:0.2em;
margin-bottom:0em;
text-align:justify;
}
p.tcenter {
text-align: center;
margin-top: 0em;
margin-bottom: 0em;
}
.chapnum{
font-size: 130%;
text-align: left;
font-weight:bold;
margin-top: 0em;
margin-bottom: 0.3em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
margin-left:0em;
}
.chaptitle{
font-size: 240%;
text-align: left;
font-weight:normal;
margin-top: 0%;
margin-bottom: 1.5em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
margin-left:0em;
}
.subtitle
{
font-size:100%;
margin-top:0em;
margin-bottom:5em;
text-align:right;
font-weight:normal;
margin-right:0em;
margin-left:0em;
}
.extract
{
font-size:80%;
margin-top:0em;
margin-bottom:0.5em;
text-indent:0em;
margin-left:3em;
text-align:justify;
font-weight:normal;
}
.extract1
{
font-size:80%;
margin-top:0em;
margin-bottom:1em;
text-indent:0em;
margin-left:3em;
text-align:justify;
font-weight:normal;
}
.extracts
{
font-size:80%;
margin-top:0em;
margin-bottom:2em;
text-indent:0em;
margin-left:3em;
text-align:justify;
font-weight:normal;
}
.extracthead
{
color:#7e64a4;
font-size:120%;
margin-top:0em;
margin-bottom:0.5em;
text-indent:0em;
margin-left:3.2em;
text-align:justify;
font-weight:normal;
}
.extracta1
{
margin-top:0em;
margin-bottom:2em;
text-indent:0em;
margin-left:1.5em;
text-align:justify;
font-weight:normal;
}

div.pbox{
padding-bottom:2em;
padding-top:2em;
background-color:#b2a2c9;
padding-left:0em;
padding-right:0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
div.pbo-x{
padding-bottom:0.5em;
padding-top:2em;
background-color:#b2a2c9;
padding-left:0em;
padding-right:0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
span.part
{
	color:#7e64a4;
}
span.part-1
{
	margin-left:0em;
	padding-right:0em;
	padding-left:0em;
	color:#000000;
}
div.pboxw{
font-weight:normal;
text-align:left;
margin-top:2em;
margin-left:0em;
margin-right:0em;
margin-bottom:1em;
padding-left:1em;
padding-top:1em;
padding-bottom:1em;
padding-right:0em;
background-color:#FFFFFF;
font-family:sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
.boxtext
{
margin-top:0em;
margin-bottom:0.5em;
text-indent:0em;
margin-left:1.5em;
text-align:justify;
font-weight:normal;
}
.boxtext1
{
margin-top:0em;
margin-bottom:2em;
text-indent:0em;
margin-left:1.5em;
text-align:justify;
font-weight:normal;
}
.tcaption{
margin-top:0.5em;
margin-bottom:1em;
text-indent:0em;
margin-left:1.5em;
text-align:center;
font-weight:bold;
font-size:100%;
font-family: sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
.tcaption1
{
margin-top:0.5em;
margin-bottom:1em;
text-indent:0em;
margin-left:1.5em;
text-align:right;
font-weight:normal;
font-size:90%;
}
.right{
text-align:right;
margin-top:5em;
margin-bottom:0em;
}
.righ-t{
text-align:right;
margin-top:1em;
margin-bottom:0em;
}
.pararight{
text-align:right;
margin-left:0em;
margin-right:1em;
margin-top:4em;
margin-bottom:0.3em;
font-family:sans-serif;
font-size:110%;
}
.pararights{
text-align:right;
margin-left:0em;
margin-right:1em;
margin-top:0.5em;
margin-bottom:0em;
font-family:sans-serif;
font-size:110%;
}
div.cbox{
font-weight:normal;
font-size:100%;
text-align:justify;
margin-top:0em;
margin-left:0em;
margin-right:0em;
margin-bottom:1.5em;
padding:0.5em;
background-color:#ece8f1;
font-family:sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
div.bbox{
font-size:130%;
color:#ffffff;
padding:0.7em 0.7em 0.7em 0.7em;
margin-left:0em;
text-align:center;
margin-top:0em;
margin-bottom:1em;
background-color:#7e64a4;
font-family:sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
.bhead
{
font-size:120%;
margin-left:0em;
text-align:left;
margin-top:0em;
color:#b2a2c9;
margin-bottom:1em;
}
.boxhead
{
font-size:100%;
text-align:left;
margin-top:0.5em;
margin-bottom:1em;
font-weight:bold;
color:#7e64a4;
}
.boxhead1{
margin-bottom:1em;
text-align:center;
font-size:120%;
font-weight:normal;
padding-left:1.5em;
padding-right:1.5em;
margin-top:0em;
font-family:sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
color:#7e64a4;
background-image: url('images/pg-iia.jpg');
background-repeat: no-repeat;
background-position:left top;
}
.boxhead2{
font-size:105%;
text-align:center;
margin-top:1em;
margin-bottom:1em;
font-weight:bold;
color:#7e64a4;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
div.vbox
{
margin-top:0em;
margin-bottom:1em;
width:25%;
font-size:110%;
padding-top:0.25em;
padding-bottom:0.25em;
padding-left:0em;
padding-right:0em;
text-align:left;
margin-left:1.2em;
text-indent:0em;
background-color:#cbc1db;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
div.vbox1
{
margin-top:0em;
margin-bottom:1em;
width:20%;
font-size:125%;
padding-top:0.5em;
padding-bottom:0.5em;
padding-left:0.5em;
padding-right:0.5em;
text-align:left;
margin-left:1.5em;
text-indent:0em;
background-color:#cbc1db;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
span.boxborder
{
border:1px solid black;
text-align:center;
width:100%;
margin-left:0em;
margin-top:0em;
margin-bottom:2em;
padding: 0.2em 1em 0.2em 1em;
text-indent:0em;
}
.extracthang
{
margin-top:0em;
margin-bottom:0em;
margin-left:3em;
text-indent:-1.5em;
text-align:justify;
}
.partno{
font-size: 120%;
text-align: left;
font-weight:normal;
margin-top: 0em;
margin-bottom: 0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
.partno-1{
font-size: 160%;
font-weight:normal;
text-align: left;
margin-top: 0%;
margin-bottom: 0%;
margin-left: 0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
.parthead{
text-align: left;
font-size:110%;
margin-top: 1em;
margin-bottom: 0.5em;
font-family:sans-serif;
text-indent:-6.5em;
margin-left:7.5em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
.parthead1{
text-align: left;
font-size:110%;
margin-top: 1em;
margin-bottom: 0.5em;
font-family:sans-serif;
text-indent:-9em;
margin-left:10em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
span.dropcap {
float:left;
color:#7e64a4;
font-size:400%;
line-height:80%;
padding-right:1%;
padding-top:0%;
padding-bottom:0%;
font-family:sans-serif;
}
div.box-x{
margin-bottom:0em;
margin-top:0em;
margin-left:0em;
margin-right:0em;
font-family:sans-serif;
}
span.dropcap1{
float:left;
margin-top:0em;
font-weight:bold;
padding-right:0.1em;
font-size:350%;
color:#7e64a4;
}
.h3{
font-size:100%;
margin-top:0em;
margin-bottom:0.3em;
margin-left:0em;
font-weight:normal;
text-indent:0em;
text-align:left;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
td.left{
font-size:95%;	
padding-top:0em;
padding-bottom:0.3em;
padding-left:0.2em;
text-align:left;
}
td.head{
vertical-align:top;
padding-top:0em;
padding-bottom:0em;
padding-left:0em;
font-weight:bold;
text-align:left;
background-color:#d8d0e4;
}
div.sbox{
font-weight:normal;
font-size:95%;
text-align:justify;
margin-top:1.5em;
margin-left:0em;
margin-right:0em;
margin-bottom:1.5em;
padding:0.5em;
background-color:#f2eff6;
border:1px solid #9883b6;
page-break-inside:avoid;
font-family:sans-serif;
}
.thead{
margin-top:0em;
margin-bottom:1em;
text-align:center;
font-weight:bold;
font-family:sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
.boxhead3{
font-size:130%;
text-align:center;
margin-top:0.5em;
margin-bottom:1em;
padding-bottom:0.5em;
border-bottom:1px solid #7e64a4;
font-weight:bold;
font-family: sans-serif;
color:#7e64a4;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
span.ss
{
	float:right;
	margin-right:8em;
	color:#000000;
}
span.ss1
{
	color:#000000;
}
.indext
{
font-size:110%;
margin-top:1em;
margin-bottom:0.5em;
text-align:left;
font-weight:bold;
margin-right:0em;
margin-left:0em;
text-indent:0em;
color:#7e64a4;
font-family: sans-serif;
}
.index
{
font-size:100%;
margin-top:0em;
margin-bottom:0em;
text-align:left;
font-weight:normal;
margin-right:0em;
margin-left:2em;
text-indent:-2em;
}
.paracenter
{
font-size:100%;
margin-top:0em;
margin-bottom:0em;
text-align:left;
}
.normal{
font-size:110%;
margin-top:0em;
margin-bottom:0.3em;
margin-left:0em;
text-align:justify;
font-family:sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
.norma-l{
font-size:90%;
margin-top:1em;
margin-bottom:0.3em;
margin-left:0em;
text-align:justify;
font-family:sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}

.normalb{
font-size:90%;
margin-top:0em;
margin-bottom:1em;
margin-left:0em;
text-align:justify;
font-family:sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
.hang-1
{
font-size:80%;
margin-top:0em;
margin-bottom:0em;
margin-left:1em;
text-indent:-1em;
text-align:justify;
}
.hang-2b
{
font-size:80%;
margin-top:0em;
margin-bottom:0em;
margin-left:1em;
text-indent:-1em;
text-align:justify;
}
div.pagebreak{
page-break-after: always;
}
.hang-2{
font-size:100%;
margin-top:0em;
margin-bottom:0em;
margin-left:2em;
text-indent:-1.5em;
text-align:justify;
}
.title-1{
font-size:245%;
margin-top:1em;
margin-bottom:2em;
color:#7e64a4;
text-align:left;
font-weight:normal;
margin-right:0em;
margin-left:0em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
div.box2
{
margin-top:0em;
margin-bottom:1em;
width:20%;
font-size:125%;
padding-top:0.5em;
padding-bottom:0.5em;
padding-left:0em;
padding-right:0.5em;
text-align:left;
margin-left:1.5em;
text-indent:0em;
}
.normald{
font-size:90%;
margin-top:1em;
margin-bottom:1em;
margin-left:0em;
text-align:left;
}

div.box-f {
font-weight:normal;
text-align:justify;
margin-top:1em;
margin-left:0em;
margin-right:0em;
margin-bottom:1em;
padding-left:0.5em;
padding-right:0.5em;
padding-top:0.5em;
padding-bottom:0.5em;
background-color:#000009;
font-family:sans-serif;
}       
div.box-s {
font-weight:normal;
text-align:justify;
margin-top:1em;
margin-left:0em;
margin-right:0em;
margin-bottom:1em;
padding:0.5em 0.5em 0.5em 0.5em;
padding-top:0.5em;
padding-bottom:0.5em;
background-color:#6658a6;
font-family:sans-serif;
}       
ul.arrow-f {
list-style-image: url(images/pg-ii.jpg);
font-size:90%;
text-align:left;
margin-left: 2.4em;
margin-top:0.3em;
margin-bottom:0.3em;
padding: 0%;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}

ul.arrow-f li {
margin-top:0.8em;
margin-bottom:0.8em;
}

p.box-head {
margin-bottom:0em;
text-align:left;
font-size:120%;
font-weight:normal;
margin-left:1.3em;
margin-top:1.5em;
font-family:sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
div.box-e {
font-weight:normal;
margin-left:3em;
margin-right:0em;
padding-bottom:0em;
padding-right:0em;
padding-top:0em;
border-left:4px solid #cbc1db;
font-family:sans-serif;
margin-top:0em;
margin-bottom:1.5em;
padding:0.7em 0em 0.2em 0.5em;
}
p.box-head-e {
margin-bottom:1%;
text-align:left;
font-size:110%;
font-weight:normal;
margin-left:0em;
margin-top:0.4em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
p.box-head-ee {
margin-bottom:0.3em;
text-align:left;
font-size:120%;
font-weight:normal;
margin-left:0em;
margin-top:0em;
font-family:sans-serif;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
p.box-texte{
text-align: justify;
margin-top: 0.3em;
margin-bottom: 0.5em;
color:#58585a;
font-size:90%;
}
p.box-text-e{
text-align: left;
margin-top: 0.3em;
margin-bottom: 0.3em;
color:#58585a;
font-size:90%;
}

span.boxhead-1 {
padding:0.2em 1em 0.2em 0.5em;
margin-bottom:0em;
text-indent:0em;
color:#000000;
background-color:#cbc1db;
font-family:sans-serif;
font-weight: normal;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
border-top-left-radius: 10px;
border-bottom-left-radius: 10px;
border-left-right-radius: 10px;
border-right-left-radius: 10px;
}

div.boxb {
font-weight:normal;
font-size:90%;
text-align:left;
margin-top:0.5em;
margin-left:4em;
margin-right:0em;
margin-bottom:1.5em;
padding:0.5em;
background-color:#ece8f1;
font-family:sans-serif;
page-break-inside: avoid;
}
p.image-leftb {
margin-top: 1.5em;
margin-left:2em;
margin-bottom:-2.6em;
text-align: left;
margin-right:0em;
}
p.box-textb{
text-align:justify;
margin-top: 0em;
margin-bottom: 0.3em;
margin-left: 1.5em;
margin-right: 0em;
font-size:100%;
font-weight:bold;
}
div.figb {
font-weight:normal;
text-align:justify;
margin-top:1.5em;
margin-left:0em;
margin-right:0em;
margin-bottom:1.5em;
padding:0.5em;
border:2px solid #7e64a4;
border-radius: 15px;
page-break-inside:avoid;
}

p.caption {
text-align: center;
margin-top: 0.5em;
margin-bottom: 0em;
color:#58585a;
font-family:sans-serif;
font-size:100%;
font-weight:bold;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
p.image {
margin-top: 0.5em;
text-align: center;
margin-bottom: 0em;
}
span.big {
font-size:200%;
font-weight:bold;
}
ul.larrow {
list-style-image: url(images/rec.jpg);
font-size:90%;
text-align:left;
margin-left: 1.8em;
margin-top:0.4em;
margin-bottom:0.4em;
padding: 0%;
font-family:sans-serif;
}

ul.larrow li {
margin-top:0.3em;
margin-bottom:0.3em;
}
p.box-head-c {
margin-bottom:0.5em;
font-size:100%;
font-weight:bold;
margin-left:0.3em;
margin-top:0.5em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}

ul.bul-l {
list-style-image: url(images/hyphen.jpg);
font-size:100%;
margin-left: 4.2em;
margin-top:0.2em;
margin-bottom:0.2em;
padding: 0%;
}
ul.bul-l li {
margin-top:0.2em;
margin-bottom:0.2em;
}
ul.bul-ll {
list-style-image: url(images/hyphen.jpg);
font-size:100%;
margin-left: 5em;
margin-top:0.2em;
margin-bottom:0.2em;
padding: 0%;
text-align:justify;
}
ul.bul-ll li {
margin-top:0.2em;
margin-bottom:0.2em;
}

p.pagebreak{
page-break-after: always;
}

table.sty0-a {
border-collapse:collapse;
margin-top:1em;
margin-bottom:1em;
font-family:sans-serif;
font-size:95%;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
table.sty0-a td{
vertical-align:top;
padding-left:0.5em;
border:1px solid #87888a;
padding-right:0.5em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}

ul.bul-2 {
list-style-image: url(images/dash.jpg);
font-size:90%;
margin-left: 1.6em;
margin-top:0.2em;
margin-bottom:0.2em;
padding: 0%;
}
ul.bul-2 li {
margin-top:0.2em;
color:#58585a;
margin-bottom:0.2em;
}
td.head1{
vertical-align:top;
padding-top:0em;
padding-bottom:0em;
padding-left:0em;
font-weight:bold;
text-align:center;
background-color:#d8d0e4;
}
td.hea-d1{
vertical-align:top;
padding-top:0em;
padding-bottom:0em;
padding-left:0em;
font-weight:normal;
text-align:left;
background-color:#d8d0e4;
}
td.head-1{
vertical-align:top;
padding-top:0em;
padding-bottom:0em;
padding-left:0em;
font-weight:normal;
text-align:left;
}


td.head2{
vertical-align:top;
padding-top:0em;
padding-bottom:0em;
padding-left:0em;
font-weight:bold;
text-align:center;
background-color:#9883b6;
}
ul.bull-t {
list-style-image: url(images/squaret.jpg);
margin-left: 1.2em;
margin-top:0.3em;
margin-bottom:0.3em;
padding: 0%;
text-align:left;
}
ul.bull-t li {
margin-top:0.3em;
margin-bottom:0.3em;
}
h2.h2-z{
font-size:120%;
margin-top:0em;
margin-bottom:0.5em;
text-align:left;
font-weight:normal;
color:#7e64a4;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
ul.bul-3 {
list-style-image: url(images/hyphen1.jpg);
font-size:100%;
margin-left: 4.1em;
margin-top:0.2em;
margin-bottom:0.2em;
padding: 0%;
}
ul.bul-3 li {
margin-top:0.2em;
margin-bottom:0.2em;
}
.noindent-s{
margin-left:1.5em;
margin-top:0em;
margin-bottom:1em;
font-family: sans-serif;
text-align:justify;
font-size: 90%;
}
ul.bull-z {
list-style-image: url(images/square.jpg);
font-size:100%;
margin-left: 5em;
margin-top:0.3em;
margin-bottom:0.3em;
padding: 0%;
}
ul.bull-z li {
margin-top:0.3em;
margin-bottom:0.3em;
}
div.sans
{
font-family: sans-serif;
font-size: 90%;
margin-left: 0.5em;
margin-bottom: 0em;
}
table.sty0-a1 {
border-collapse:collapse;
margin-top:1em;
margin-bottom:1em;
font-family:sans-serif;
font-size:90%;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
table.sty0-a1 td{
padding-left:0.5em;
padding-right:0.5em;
border:1px solid #87888a;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
td.head-z{
padding-top:0em;
padding-bottom:0em;
padding-left:0em;
font-weight:bold;
text-align:left;
background-color:#d8d0e4;
}
h2.h2z{
font-size:120%;
margin-top:1em;
margin-bottom:0.5em;
text-align:left;
font-weight:bold;
color:#7e64a4;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
.h3z{
font-size:100%;
margin-top:1em;
margin-bottom:0.3em;
margin-left:0em;
font-weight:normal;
text-indent:0em;
text-align:left;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
h2.h2-z1{
font-size:120%;
margin-top:1em;
margin-bottom:0.5em;
text-align:left;
font-weight:normal;
color:#7e64a4;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
font-family:sans-serif;
}
p.source
{
font-family:sans-serif;
font-size: 90%;
margin-bottom: 1em;
margin-left: 1.8em;
margin-top: 0em;
}
.noindent-s1{
margin-left:1.5em;
margin-top:0em;
margin-bottom:0.2em;
font-family: sans-serif;
text-align:justify;
font-size: 90%;
}
.noindent-s-1{
margin-left:1.5em;
margin-top:0em;
margin-bottom:1.5em;
font-family: sans-serif;
text-align:justify;
font-size: 90%;
}
td.head-z1{
padding-top:0.3em;
padding-bottom:0.3em;
padding-left:0.3em;
font-weight:bold;
text-align:left;
background-color:#d8d0e4;
}
p.text
{
text-align: center;
margin-top: 0.5em;
margin-bottom: 0.5em;
margin-left: 1em;
margin-right: 1em;
}
div.boxborder{
border:1px solid black;
text-align:center;
width:100%;
margin-left:0em;
margin-top:1em;
margin-bottom:1em;
padding: 0em;
text-indent:0em;
}
td.td1
{
vertical-align: top;
}
td.td2
{
vertical-align: top;
text-align: justify;
padding-left:0.5em;
}
.bodytextz{
text-align: justify;
margin-top: 0em;
margin-bottom: 0.2em;
margin-left:1.5em;
text-indent:1.4em;
}
.noindent2
{
font-size:100%;
margin-top:0em;
margin-left: 1em;
margin-bottom:0.2em;
text-align:justify;
}
p.center-e {
text-align: center;
margin-top: 25%;
margin-bottom: 4%;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
p.center-e1 {
text-align: center;
margin-top: 0.5%;
margin-bottom:0%;
font-size: 90%;
}
img.img2 {
vertical-align:middle;
}
.parthea-d{
text-align: left;
font-size:110%;
margin-top: 1em;
margin-bottom: 0.5em;
font-family:sans-serif;
text-indent:-7em;
margin-left:7.5em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}
.parthea-dd{
text-align: left;
font-size:110%;
margin-top: 1em;
margin-bottom: 0.5em;
font-family:sans-serif;
text-indent:-8.9em;
margin-left:9.4em;
-webkit-hyphens: none !important;
adobe-hyphenate: none;
-moz-hyphens: none;
}

ul.b-ull {
list-style-image: url(images/square.jpg);
font-size:90%;
margin-left: 2.9em;
margin-top:0.3em;
margin-bottom:0.3em;
padding: 0%;
font-family:sans-serif;
}
ul.b-ull li {
margin-top:0.3em;
margin-bottom:0.3em;
}
";
        }

        [Fact]
        public void Then_remove_empty_expressions()
        {
            Assert.Equal(209, document.RuleSets.Count);
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

    public class With_quoted_format_value : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"@font-face {
    	src: url('resources/angelina.TTF') format('truetype')
    }";
        }

        [Fact]
        public void Then_dont_add_extra_quote()
        {
            document.ToString().ShouldContain("src: url('resources/angelina.TTF') format('truetype')");
        }
    }

    public class With_media_directive_with_expression : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"@media amzn-mobi { }";
        }

        [Fact]
        public void Then_expression_should_be_conserved()
        {
            document.ToString().ShouldContain("@media amzn-mobi");
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


    public class With_declaration_containing_rules_values_with_dot : And_ParseText
    {
        protected override void Establish_context()
        {
            base.Establish_context();

            textToParse = @"h1.quae-numero-partie {
                               font-family: Tahoma, Geneva, sans-serif;
                               text-align: left;
                               color: #808080;
                               margin-bottom: 0;
                               padding-left: .3em;
                               border-left: solid .5em black;
    	                       }";
        }

        [Fact]
        public void Then_check_if_there_is_space_before_dot_to_know_if_this_is_another_rule()
        {
            document.ToString().ShouldContain(@"border-left: solid .5em black;");
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