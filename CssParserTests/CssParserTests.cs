using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using NBehave.Spec.Xunit;
using BoneSoft.CSS;

namespace CssParserTests
{
    public class Specification : SpecBase
    {

    }

    public class CssParserTests : Specification
    {
        protected string _inputCss, _outputCss;
        protected CSSDocument _document;

        protected override void Establish_context()
        {
            base.Establish_context();

            #region cssString
            _inputCss = @"body
	{
	text-align: justify;
	-webkit-hyphens: none;
	padding-left: 2%;
	padding-right: 2%;
	}
p
	{
	text-align: justify;
	margin-top: 0;
	margin-bottom: 0;
	-webkit-hyphens: none;}
div.essential-box
	{
	margin: 2em 2em 0em 2em;
	padding: 10px;
	-webkit-border-radius: 10px;
	-webkit-box-shadow: 2px 2px 2px rgb(251,242,237);
	
	}
.lesson-opener-box
	{
	margin: 20px 30px 20px 20px;
	padding: 2%;
	background-color: #ED068D;
	color: #ffffff;
	}
.clear {clear:both}
.clear-l {clear:left}
.clear-r {clear:right}
.break
	{
	clear:both;
	border-bottom: 10px solid rgb(167,169,172);
	}
.indent-1{margin-left: 2em;} 
.indent-1a{margin-left: 1.5em;} 
/* .indent-2{margin-left: 4em;} */
.indent-10{margin-left: 10%;}
.indent-20{margin-left: 20%;}
.indent-20{margin-left: 30%;}
.indent-20{margin-left: 40%;}
.indent-20{margin-left: 50%;}
.indent-20{margin-left: 60%;}
.indent-20{margin-left: 70%;}
.indent-20{margin-left: 80%;}
.indent-20{margin-left: 90%;}
/*#lesson-page {
background-image: url('images/number-border.png');
margin: 10px 10px 10px 100px;
}*/
div.image-r
	{
	float:right;
	margin-top:1em;
	margin-left:10px;
	}
div.image-l
	{
	float:left;
	margin-top:1em;
	}
div.image-c
	{
	text-align:center;
	margin-top:1.5em;
	margin-bottom:1.5em;
	}
div.image-c2
	{
	text-align:center;
	margin-top:1em;
	}
.image-c1
	{
	text-align:center;
	margin-bottom: 1.5em;
	max-width: 100%;
	max-height: 100%;
	margin-top:1.5em;
	}
div.group
	{
	margin:1em 0 1em 0;
	width:100%;
	display:block;
	}
.flow
	{
	display:block;
	margin-top:1em;
	}
div.materials
	{ 
	background-color: gray;
	padding:5px;
	margin: 1em 0 1em 0;
	display:inline-block;
	
	}
div.box-head
	{
	-webkit-border-radius: 10px;
	background-color: rgb(255,255,255);
	padding: 5px;
	margin-top:1em;
	}
div.lets-play
	{
	border:gray thick  dotted;
	padding: 10px;
	margin-left: 5%;
	margin-top: 5%;
	-webkit-border-radius: 20px;
	background:lightgray;
	}
div.example-box_main
	{ 
	border:gray thick  solid;
	padding:0px;
	margin: 1em 0 1em 0;
	-webkit-border-radius: 10px;
	}
div.example-box
	{ 
	border:gray thick  dotted;
	padding:10px;
	}
/*icons*/
div.look-closer
	{
	width:33%;
	margin-top:1em;
	clear:both;
	}
div.sum-it-up
	{
	width:30%;
	margin-top:1em;
	clear:both;
	}
div.you-can-do-this
	{
	width:41%;
	margin-top:1em;
	clear:both;
	}
div.sharpen-your-skill
	{
	width:43%;
	margin-top:1em;
	clear:both;
	}
div.explore-and-learn
	{
	width:44%;
	margin-top:1em;
	clear:both;
	}
div.discuss-and-plan
	{
	width:43%;
	margin-top:1em;
	clear:both;
	}
div.do-it
	{
	width:27%;
	margin-top:1em;
	clear:both;
	}
div.look-back
	{
	width:33%;
	margin-top:1em;
	clear:both;
	}
div.present-your-work
	{
	width:47%;
	margin-top:1em;
	clear:both;
	}
/*width*/
.five {width: 5%}
.ten {width: 10%}
.fifteen {width: 15%}
.twenty {width: 20%}
.twenty-five {width: 25%}
.thirty {width: 30%}
.forty {width: 40%}
.forty-five {width: 45%}
.forty-eight {width: 48%}
.fifty {width: 50%}
.sixty {width: 60%}
.seventy {width: 70%}
.eighty {width: 80%}
.ninety {width: 100%}
.ninety-five {width: 95%}
.siento {width: 100%}
div.key-words{margin:1em 0em 0em 0em;}
div.lesson-icon
	{
	float:left;
	width:25%;
	}
div.team-up
	{
	width:35%;
	display:inline-block;
	}
img {max-width: 100%}

#lesson-title
	{
	padding: 15px 15px 15px 15px;
	margin: 0 0 2em 0;
	}
div.lets-play-icon
	{
	width:30%;
	float:left;
	position:absolute;
	z-index:1;
	top:0px;
	left:0px;
	}
span.lets-play
	{
	font-family: roman, 'times new roman', times, serif;
	font-size: 1.2em;
	color: #000000;
	}
input.input-box-2char
	{
	padding:2px;
	font-family:arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin: 0 .3em 0 0;
	width:25%;
	text-align:left;
	}
input.input-box-1char
	{
	padding:2px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin: 0 .3em 0 0;
	width:45%;
	text-align:center;
	}
input.input-box-1line
	{
	padding:2px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin: 0 .0em 0 0em;
	width:78%;
	text-align:left;
	text-indent:0em;
	}
input.input-box-1line2
	{
	padding:2px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin: 0 .0em 0 0em;
	width:60%;
	text-align:left;
	text-indent:0em;
	}
td input.input-box-1line
	{
	padding:1px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 0.8em;
	line-height: 1.20em;
	margin: 0;
	width:78%;
	text-align:left;
	text-indent:0em;
	}
input.input-box-2line
	{
	padding:2px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin: 0 .0em 0 0em;
	width:96%;
	text-align:left;
	text-indent:0em;
	}
input.input-box-1word
	{
	padding:2px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: .9em;
	line-height: 1.20em;
	text-align:center;
	width:100px;
	}
div.keep {display:inline-block; width:100%}
span.num
	{
	font-size:180%;
	color: #6D6E71;
	font-weight:bold;
	font-family: Arial;
    margin-right: 0.5em;
}
span.num1
	{
	font-size:100%;
	font-family: ""Wingdings"";
	}
span.num2
	{
	font-size:110%;
	font-weight:bold;
	}
span.underline
	{
	padding-bottom:.5px;
	/*display:inline-block;
	*/border-bottom:thin solid black;
	}
span.underline1
	{
	padding-bottom:.5px;
	/*display:inline-block;
	*/border-bottom:thin solid #ffffff;
	}
span.box
	{
	padding:10px;
	border: thin solid black;
	}
span.inline-img {vertical-align:middle;}
.top-space {margin-top:1em}
div.center
	{
	text-align: center;
	}
/* table */
table.lets-play-tab
	{
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	border:thin solid black;
	margin-left:1em;
	}
table.math-table
	{
	border:thick solid black;
	margin-bottom: .5em;
	}
table.simple-tab
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	border: thick solid black;
	vertical-align:top;
	}
table.simple-tab1
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	margin-top: 1em;
	margin-bottom: 1em;
	vertical-align:top;
	}
table.simple-tab2
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	margin-top: 0.6em;
	margin-bottom: 0.6em;
	vertical-align:top;
	}
td.lets-play{text-align:left;}
td.dec-tab-hd
	{
	border-left:1.3px solid #808285;
	border-right:1.3px solid #808285;
	border-bottom:1.3px solid #808285;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	color: #000000;
	background-color: #FF99CC;
	}
td.dec-tab-hd1
	{
	border:1.3px solid #808285;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	color: #000000;
	/*background-color: #FF99CC;*/
	background-color: #E4027F;
	}
td.dec-tab-hd1-h
	{
	border-bottom:1.3px solid #808285;
	border-left:1.3px solid #808285;
	border-right:1.3px solid #808285;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	color: #000000;
	background-color: #FF99CC;
	}
td.dec-tab-hd2
	{
	border-top:1.3px solid #808285;
	border-bottom:1.3px solid #808285;
	border-right:1.3px solid #808285;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	color: #000000;
	/*background-color: #FF99CC;*/
	background-color: #E4027F;
	}
td.dec-tab-90
	{
	border:thin black solid;
	text-align:left;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	color: #000000;
	-webkit-transform: rotate(-90deg);
	padding:0;
	margin:0;
	}
span.dec-tab-90
	{
	-webkit-transform: rotate(-90deg);
	}
td.dec-tab
	{
	border-bottom:1.3px solid #808285;
	border-right:1.3px solid #808285;
	background-color: lightgray;
	}
td.dec-tab1
	{
	border-top:1.3px solid #808285;
	border-right:1.3px solid #808285;
	border-bottom:1.3px solid #808285;
	}
td.dec-tab2
	{
	border:1.3px solid #808285;
	}
td.dec-tab3
	{
	border-left:1.3px solid #808285;
	border-right:1.3px solid #808285;
	border-bottom:1.3px solid #808285;
	}
td.dec-tab1-right
	{
	
	border:1.3px solid #808285;
	
	}
td.dec-tab1-right1
	{
	border-top:none;
	border-right:1.3px solid #808285;
	border-bottom:1.3px solid #808285;
	}
td.example-text
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	border-top: 2px dotted black;
	border-bottom: 2px dotted black;
	margin: 0.26em 0em;
	}
/* chapter */
#ch-opener-recto
	{
	background-color: rgb(255,253,223);
	}
div.ch-opener-txt
	{
	margin: 2em 2em 1em 2em;
	}
p.chap-caption
	{
	font-family: western, fantasy;
	font-weight: normal;
	font-style: normal;
	font-size: 2.92em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin: 0em;
	}
h1.chap-title
	{
	font-weight: bold;
	-webkit-hyphens: none;
	font-style: normal;
	font-size: 2.92em;
	line-height: 1.20em;
	text-decoration: none;
	text-indent: 0em;
	text-align: center;
	margin-top: 2em;
	margin-bottom: 2em;
	color: #808285;
	}
p.chap-title
	{
	font-weight: bold;
	-webkit-hyphens: none;
	font-style: normal;
	font-size: 2.92em;
	line-height: 1.20em;
	text-decoration: none;
	text-indent: 0em;
	text-align: center;
	margin-top: 2em;
	margin-bottom: 0.5em;
	color: #808285;
	}
p.chap-num
	{
	font-family: arial, helvetica, sans-serif;
	-webkit-hyphens: none;
	font-weight: bold;
	font-style: normal;
	font-size: 2.92em;
	line-height: 1.20em;
	text-decoration: none;
	text-indent: 0em;
	text-align: center;
	margin-top: 2em;
	margin-bottom: 0em;
	}
p.chap-title1
	{
	font-weight: bold;
	-webkit-hyphens: none;
	font-style: normal;
	font-size: 2.92em;
	line-height: 1.20em;
	text-decoration: none;
	text-indent: 0em;
	text-align: center;
	margin: 0.10em 0em;
	}
p.chap-title2
	{
	font-weight: bold;
	-webkit-hyphens: none;
	font-style: normal;
	font-size: 2.92em;
	line-height: 1.20em;
	text-decoration: none;
	text-indent: 0em;
	text-align: center;
	margin: 0.10em 0em;
	}
div.ch-opener
	{
	margin:0;
	padding:0;
	}
p.keywords-chapter
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: small-caps;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 0.2em 0em .5em 0em;
	}
p.chap-review
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: bold;
	font-style: normal;
	font-size: 1.33em;
	line-height: 1.16em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin: 0em;
	}
p.chap-review-2
	{
	font-family: ""BlacklightD"";
	font-weight: normal;
	font-style: normal;
	font-size: 2.08em;
	line-height: 1.20em;
	text-decoration: none;
	text-transform: uppercase;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin: 0em;
	}
/* toc */
p.contents
	{
/* 	font-family: western, fantasy; */
	font-weight: bold;
	font-style: normal;
	font-size: 120%;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #808285;
	margin: 0.8em 0em 0em 0em;
	padding:0em;
	}
p.contents1
	{
/* 	font-family: western, fantasy; */
	font-weight: normal;
	font-style: normal;
	font-size: 100%;
	text-decoration: none;
	font-variant: normal;
	text-indent: -1.5em;
	text-align: left;
	color: #000000;
	margin-top: 0.2em;
	margin-left: 1.5em;
	padding:0em;
	}
div.contents_chhead
	{
	display:inline-block;
	margin: 1em 0 0 0;
	padding: 1em 0 0 0;
	}
p.contents_txt
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.40em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 1em 0em 0em 0em;
	padding:0em;
	}
/* list */
ul.essential{margin:0em 0em 0em 0em;}
ul.enduring
	{
	margin:1em 0em 0em 0em;
	padding-left: 1.5em;
	}
ul {
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;}
ul.bib-1
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	margin: 0;
	text-indent: 0;
	text-decoration: none;
	font-variant: normal;
	}
li.bib
	{
	text-indent: 0;
	margin-left: 1em;
	}
li.objective
	{
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 0.83em;
	line-height: 1.40em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 0em 0em 0em 0em;
	padding:0em;
	}
/* copyright */
p.copyright
	{
	font-family: Arial;
	font-weight: normal;
	font-style: normal;
	font-size: 90%;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin: 0em 0em 0em 0em;
	padding:0em;
	}
p.copyright2
	{
	font-family: Arial;
	font-weight: normal;
	font-style: normal;
	font-size: 90%;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin-top: 1em;
	padding:0em;
	}
p.copyright-b
	{
	font-family: Arial;
	font-weight: normal;
	font-style: normal;
	font-size: 90%;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin: 3em 3em 3em 3em;
	padding:0em;
	}
p.copyright1
	{
	font-family: Arial;
	font-weight: normal;
	font-style: normal;
	font-size: 90%;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin: 0em 0em 0em 0em;
	padding:0.1em;
	}
/* body-text */
p.body-text-2
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin: 0.26em 0em;
	margin-top:.5em;}
p.body-text-noindent
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin-top: 0.5em;
	margin-bottom: 0.5em;
	}
p.body-text-right
	{
	font-weight: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: right;
	color: #000000;
	margin: 0.26em 0em;
	}
p.frontmatter
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	padding: 2%;
	margin-top: 0.8em;
	}
p.body-noindent
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin: 0em 0em;
	}
p.body-text-b
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin-top: 0.5em;
	margin-bottom: 0.5em;
	}
div.back-center
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin-top: 25%;
	margin-bottom: 0.5em;
	padding: 1%;
	}
p.body-text-li
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin-top: 1em;
	margin-bottom: 1em;
	vertical-align: middle;
	}
p.body-li
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin-top: 1em;
	margin-bottom: 1em;
	vertical-align: middle;
	}
p.body-text-li1
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1em;
	line-height: 1em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin-top: 0.5em;
	margin-bottom: 0em;
	}
p.body-text-li3
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin-top: 0em;
	margin-bottom: 0em;
	}
p.body-text-li2
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin-top: 1.5em;
	margin-bottom: 1.5em;
	}
p.body-text-col
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 0.26em 0em;
	min-width:45%;
	padding-right:2.5%;
	float:left;
	}
p.keywords
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: small-caps;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin: 0em 0em 0em 0em;
	}
p.objective
	{
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 0.83em;
	line-height: 1.40em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -1em;
	text-align: left;
	color: #000000;
	margin: 0em 0em 0em 2em;
	padding:0em;
	}
p.body-text
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 0.26em 0em;
	}
p.body-text-1
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin-top: 1em;
	margin-bottom: 1em;
	}
p.body-text-3
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin-top: 1em;
	margin-bottom: 1em;
	}
p.lesson-title
	{
	font-family: western, fantasy;
	font-weight: normal;
	font-style: normal;
	font-size: 2.17em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin: 0em 0em 0em 30%;
	}
p.no-paragraph-style
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 0em;
	}
p.indent-0
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -1.3em;
	text-align: left;
	color: #000000;
	margin: 1em 0em 0.26em 1.3em;
	}
p.indent-1
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -1.5em;
	text-align: left;
	color: #000000;
	margin: 1em 0em 0.26em 3em;
	}
p.indent-gp
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -1.25em;
	text-align: left;
	color: #000000;
	margin: 1em 0em 0.26em 1em;
	}
p.indent-2
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 0.26em 0em 0.26em 4em;
	}
p.indent-3
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 1em;
	text-align: left;
	color: #000000;
	margin: 0.26em 0em 0.26em 0em;
	}
p.bullet
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -0.8em;
	text-align: left;
	color: #000000;
	margin-top: 0.26em;
	margin-left: 1em;
	margin-right: 0;
	}
p.space-2
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: normal;
	font-style: normal;
	font-size: 0.83em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 2.40em;
	text-align: left;
	color: #000000;
	margin: 0em;
	}
p.icons
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.33em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: small-caps;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 2.25em 0em 1.12em 0.68em;
	}
p.example
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: bold;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 0.5em 0em 0.51em 0em;
	padding: 5px 10px 5px 10px;
	background-color: gray;
	display:inline-block;
	}
span.example
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: bold;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 0.5em 0em 0.51em 0em;
	padding: 5px 10px 5px 10px;
	background-color: gray;
	display:inline-block;
	}
p.example-text
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 1.93em;
	text-align: left;
	color: #000000;
	margin: 0.26em 1.29em;
	}
p.space-1
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.54em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 2em;
	text-align: left;
	color: #000000;
	margin: 0em;
	}
p.space-3
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: normal;
	font-style: normal;
	font-size: 0.67em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 3em;
	text-align: left;
	color: #000000;
	margin: 0em;
	}
p.concept-skills
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: bold;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 1em 0em 0em 0em;
	}
p.instructions1
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 1em 0em 0.26em 0em;
	}
p.instructions
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 2.5em 0em 0.26em 0em;
	}
p.dec-tab
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -3.21em;
	text-align: left;
	color: #000000;
	margin: 0.26em 0em 0.26em 3.21em;
	}
p.dec-tab-3
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -3.21em;
	text-align: left;
	color: #000000;
	margin: 0.26em 0em 0.26em 3.21em;
	}
p.materials
	{
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	}
p.for-illus
	{
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: center;
	color: #ff0000;
	margin: 0em;
	}
p.arial-font-14
	{
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -1.77em;
	text-align: left;
	color: #000000;
	margin: 0.26em 0em 0.26em 3.21em;
	}
p.trivia
	{
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 0.83em;
	line-height: 1.40em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 0em;
	}
p.ex-step
	{
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -4.50em;
	text-align: left;
	color: #000000;
	margin: 0.51em 1.29em 0.26em 5.79em;
	}
p.lets-play
	{
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 0em 0em 0em 0em;
	}
p.lets-play-indent
	{
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -1em;
	text-align: left;
	color: #000000;
	margin: 2em 0em 0.26em 1em;
	}
p.caption
	{
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 0.83em;
	line-height: 1.40em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 0em;
	}
p.icons-2
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: bold;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 2.06em 0em 1.54em 0em;
	}
p.gp-chap-number
	{
	font-family: ""Souvenir Lt BT"";
	font-weight: normal;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: center;
	color: #ffffff;
	margin: 0em 0em 0.26em 0em;
	}
p.gp-title
	{
	font-family: western, fantasy;
	font-weight: normal;
	font-style: normal;
	font-size: 1.50em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: small-caps;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin: 0em;
	}
p.center
	{
	page-break-inside: avoid;
	text-align: center;
	margin-top:0.3em;
	}
p.center1
	{
	page-break-inside: avoid;
	text-align: center;
	margin-top:0.8em;
	}
/* Updated */
p.special
	{
	border-top:1px black solid;
	border-bottom: 1px black solid;
	vertical-align:top;
	padding: 1%;
	text-align:center;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	text-decoration: none;
	margin-left: 5em;
	margin-right: 5em;
	font-variant: normal;
	}
.box-empty
	{
	border: 1.5px  solid black;
	background-color: white;
	margin-left: -2em;
	padding-top: 0.0001em;
	padding-bottom: 0.00001em;
	padding-right: 0.1em;
	padding-left: 1%;
	}
a
	{
	text-decoration: none;
	color: #000000;
	}
.halftitle
	{
	margin-top: 15%;
	text-align: center;
	}
.author
	{
	text-align: center;
	margin-top: 1em;
	margin-bottom: 1em;
	color: #808285;
	font-size: 150%;
	}
.title-page
	{
	text-align: center;
	margin-top: 2.5em;
	margin-bottom: 0em;
	font-size: 120%;
	}
.h2
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: bold;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 2em 0em 2em 0em;
	vertical-align: middle;
	padding: 0px 0px 0px 0px;
	color: #808285;
	}
.h4
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: bold;
	font-style: normal;
	font-size: 1.15em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 1.5em 0em 1.5em 0em;
	vertical-align: middle;
	padding: 0px 0px 0px 0px;
	color: #808285;
	}
span.imgleft
	{
	float:left;
	text-indent:0;
	margin-top: 5px;
	margin-right:5px;
	margin-bottom:1em;
	font-size: 200%;
	color: #6D6E71;
	max-width:50%;
	}
p.h3
	{
	font-family: roman, 'times new roman', times, serif;
	font-weight: bold;
	font-style: normal;
	font-size: 1.17em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #6D6E71;
	margin: 1em 0em 0.5em 0em;
	}
p.h5
	{
	font-family: roman, 'times new roman', times, serif;
	font-style: normal;
	font-size: 1.02em;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: left;
	color: #000000;
	margin: 1em 0em 0.5em 0em;
	}
.block
	{
	margin-left: 2em;
	text-indent: 0em;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	text-align: justify;
	}
textarea.text-area
	{
	padding:3px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin-top: 1.5em;
	margin-bottom: 1.5em;
	margin-left: 0em;
	margin-right: 2.5em;
	width: 90%;
	height: 80px;
	border: 1px dotted black;
	}
	
	textarea.text-area9
	{
	padding:3px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin-top: 0.5em;
	margin-bottom: 0.5em;
	margin-left: 0em;
	margin-right: 2.5em;
	width: 80%;
	height: 50px;
	border: 1px dotted black;
	}
	
		textarea.text-area9a
	{
	padding:3px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin-top: 0.5em;
	margin-bottom: 0.5em;
	margin-left: 0em;
	margin-right: 2.5em;
	width: 80%;
	height: 80px;
	border: 1px dotted black;
	}
	
	textarea.text-area8
	{
	padding:3px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: .9em;
	line-height: 1.20em;
	margin-top: 0.5em;
	margin-bottom: 0.5em;
	margin-left: 0em;
	margin-right: 0em;
	width: 50%;
	height: 50px;
	border: 1px dotted black;
	}
	
textarea.text-area1
	{
	padding:3px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin-top: 1.5em;
	margin-bottom: 1.5em;
	margin-left: 0em;
	margin-right: 2.5em;
	width: 90%;
	height: 40px;
	border: 1px dotted black;
	}
	textarea.text-area1a
	{
	padding:3px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin-top: 1.5em;
	margin-bottom: 1.5em;
	margin-left: 0em;
	margin-right: 2.5em;
	width: 90%;
	height: 120px;
	border: 1px dotted black;
	}
	
textarea.text-area2
	{
	padding:3px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin-top: 1.5em;
	margin-bottom: 1.5em;
	margin-left: 0em;
	margin-right: 2.5em;
	width: 90%;
	height: 180px;
	border: 1px dotted black;
	}
.topspace1{margin-top: 2em;}
.topspace2{margin-top: 4em;}
.topspace3{margin-top: 6em;}
.topspace{margin-top: 1em;}
p.bib
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -4em;
	text-align: justify;
	color: #000000;
	margin-left: 4em;
	margin-top: 1em;
	margin-bottom: 0;
	}
p.bib1
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: -1em;
	text-align: justify;
	color: #000000;
	margin-left: 1em;
	margin-top: 0.26em;
	margin-bottom: 0;
	}
td.bib1
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin-left: 0em;
	margin-top: 0.26em;
	margin-bottom: 0;
	}
p.bib2
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin-left: 0em;
	margin-top: 0.26em;
	margin-bottom: 0;
	}
.color{color: #808285; font-weight: bold;}
.dropcap
	{
	font-size: 200%;
	color: #808285;
	}
div.exercice-note
	{
	font-family: ""Balzano"";
	line-height: 1.20em;
	font-size: 1.00em;
	margin-bottom: 1.00em;
	margin-top: 1.00em;
	text-indent: 0.00em;
	margin-right: 0.00em;
	margin-left: 0.00em;
	font-weight: normal;
	font-style: normal;
	color: rgb(0,0,0);
	text-align:left;
	}
div.exercice-note1
	{
	font-family: ""Balzano"";
	margin-top: 8em;
	margin-bottom: 0em;
	text-indent: 0.00em;
	margin-right: 0.00em;
	margin-left: 0.00em;
	text-align:left;
	}

	.noindent-td
	{
	margin-left: 0.5em;
	text-indent: +0;
	text-align: left;
	}
.noindent-td1
	{
	margin-left: 2em;
	text-indent: +0;
	text-align: left;
	}
td.dec-tab-hd16
	{
	border-left:1.3px solid #808285;
	border-right:1.3px solid #808285;
	border-bottom:1.3px solid #808285;
	font-weight: normal;
	font-style: normal;
	font-size: 1.1em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	color: #000000;
	}
td.dec-tab-hd16a
	{
	border-left:1.3px solid #808285;
	border-right:1.3px solid #808285;
	border-bottom:1.3px solid #808285;
	font-weight: normal;
	font-style: normal;
	padding-bottom: 3em;
	font-size: 1.1em;
	line-height: 1.21em;
	text-decoration: none;
	font-variant: normal;
	color: #000000;
	}
.float
{
float:left;		
padding:0;
margin-left:0em;
margin-top:.5em;
margin-bottom:0em;
width:13%;
margin-right:2%;
}
.float-left
{
float:left;		
padding:0;
margin-left:0em;
margin-top:.5em;
margin-bottom:0em;
width:35%;
vertical-align:top;
margin-right:2%;
}
.float-right
{
float:right;		
padding:0;
margin-left:2%;
margin-top:.5em;
margin-bottom:0em;
width:50%;
margin-right:0;
}
p.footline
{
text-indent:0px;
border-bottom:1px solid black;
width:35%;
margin-top:3em;
margin-bottom:0.5em;
}
p.body-text-foot
	{
	font-weight: normal;
	font-style: normal;
	font-size: 90%;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin: 0.26em 0em;
	}
.note {text-indent: 1em;
font-size: 90%;
}

.imgright{
float:right;
text-indent:0;
margin-top: 0px;
margin-right:0px;
margin-bottom:0px;
max-width:0%;
}
a.a1{color: #ffffff;}

hr.hr1
	{
	border-top: 1px solid black;
	width: 20%;
	text-align: left;
	margin-top: 3em;
	margin-bottom: 1em;
	}
span.xsmall
	{
	font-size: 0.5em;
	color: #808285;
	}
td textarea.text-area-td
	{
	padding:3px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin-top: 1.5em;
	margin-bottom: 1.5em;
	margin-left: 0em;
	margin-right: 2.5em;
	width: 50%;
	height: 250px;
	border: 1px dotted black;
	}

.responses{
  
    margin-right: 0em;
    color: black;

    /*
    border:1px solid #d6e8f0;
    background: -webkit-gradient(linear, left top, left bottom, color-stop(0, #f5f9fb), color-stop(40%, #ebf4f8), color-stop(100%, #d6e8f0));
    background: -moz-linear-gradient(top, #f5f9fb, #d6e8f0);

    -moz-border-radius: 3px;
    -webkit-border-radius: 3px;
    
    */
    padding-left: .4em;
    padding-bottom: .4em;
    display:block;

}
.float-l {float:left; margin-left:-2em}




		div.figure-left {
	float: left;
}
div.figure-right {
	float: right;
}

.forty-eight{width:48%}

div.group {
	width: 100%;
	display:inline-block;
	margin: 1em 0 1em 0;
}



p.back-center1
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin-top: 5%;
	margin-bottom: 1em;
	font-size:170%;
	-webkit-hyphens: none;}
	
p.back-center1a
	{
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin-top: 3%;
	margin-bottom: 1em;
	font-size:170%;
	-webkit-hyphens: none;}

		p.copyright2aa
	{
	font-family: Arial;
	font-weight: normal;
	font-style: normal;
	font-size: 90%;
	line-height: 1.20em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: center;
	color: #000000;
	margin-top: 15em;
	padding:0em;
	}
	
	.topspace2a{margin-top: 15em;}
	
	p.test:after { content:'' }
	
	textarea.text-area3
	{
	padding:3px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin-top: 1.5em;
	margin-bottom: 1.5em;
	margin-left: 0em;
	margin-right: 2.5em;
	width: 90%;
	height: 200px;
	border: 1px dotted black;
	}
	
	textarea.text-area3a
	{
	padding:3px;
	font-family: arial, helvetica, sans-serif;
	font-weight: normal;
	font-style: normal;
	font-size: 1.2em;
	line-height: 1.20em;
	margin-top: 1.5em;
	margin-bottom: 1.5em;
	margin-left: 0em;
	margin-right: 2.5em;
	width: 90%;
	height: 180px;
	border: 1px dotted black;
	}
	
	span.box1

	{
	padding:4px;
	border: thin solid purple;
-webkit-border-radius: 10px;
font-size: 95%;
}


p.charcount
	{
	font-weight: normal;
	font-style: normal;
	font-size: 85%;
	line-height: 1.2em;
	text-decoration: none;
	font-variant: normal;
	text-indent: 0em;
	text-align: justify;
	color: #000000;
	margin: 0.26em 0em;
	
	}

.text {
border:thin solid gray;
-webkit-border-radius:5px;
padding:2px 5px;
background-color: white;
font-size: .9em;
display:inline-block;
}

#exercise {
margin: 20px
}

td {padding:4px; vertical-align:middle; text-align:left; }

table {width:100%;}


button {width:150px;

    font-size:1em;
    border-radius:4px;
}

button {
   border-top: 1px solid #2c3e4a;
   background: #0f1417;
   padding: 5px 10px;
   -webkit-border-radius: 8px;
   border-radius: 8px;
   -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
   box-shadow: rgba(0,0,0,1) 0 1px 0;
   text-shadow: rgba(0,0,0,.4) 0 1px 0;
   color: white;
   font-size: 14px;
   font-family: Georgia, serif;
   text-decoration: none;
   vertical-align: middle;
   background-image: -webkit-gradient(linear, left top, left bottom, from(#e1e8ed), to(#0f1417));
   background-image: -webkit-linear-gradient(top, #e1e8ed, #0f1417);
   
}


button.blue-button {
   border-top: 1px solid #2c3e4a;
   background: #0f1417;
   padding: 5px 10px;
   -webkit-border-radius: 8px;
   border-radius: 8px;
   -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
   box-shadow: rgba(0,0,0,1) 0 1px 0;
   text-shadow: rgba(0,0,0,.4) 0 1px 0;
   color: white;
   font-size: 14px;
   font-family: Georgia, serif;
   text-decoration: none;
   vertical-align: middle;
background-image: -webkit-gradient(linear, left top, left bottom, from(#3e779d), to(#65a9d7));
 background-image: -webkit-linear-gradient(top, #3e779d, #65a9d7);
   }";
#endregion cssString
        }

        protected override void Because_of()
        {
            CSSParser parser = new CSSParser();
            _document = parser.ParseText(_inputCss);
            _outputCss = _document.ToString();
        }

        [Fact]
        protected void Then_output_should_be_ok()
        {
            _document.RuleSets.Count.ShouldEqual(244);
            _outputCss.ShouldContain("content: ''");
        }
    }
}
