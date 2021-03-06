using System;
using System.Collections.ObjectModel;

namespace BoneSoft.CSS
{
    public class Parser
    {
        const int _EOF = 0;
        const int _ident = 1;
        const int _newline = 2;
        const int _digit = 3;
        const int _whitespace = 4;
        const int maxT = 49;

        const bool T = true;
        const bool x = false;
        const int minErrDist = 2;

        public Scanner scanner;
        public Errors errors;

        public Token lastRecognizedToken;    // last recognized token
        public Token nextToken;   // lookahead token
        int errDist = minErrDist;

        public CSSDocument CSSDoc;

        bool PartOfHex(string value)
        {
            if (value.Length == 7) { return false; }
            if (value.Length + nextToken.val.Length > 7) { return false; }
            System.Collections.Generic.List<string> hexes = new System.Collections.Generic.List<string>(new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "a", "b", "c", "d", "e", "f" });
            foreach (char c in nextToken.val)
            {
                if (!hexes.Contains(c.ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        bool IsUnit()
        {
            if (nextToken.kind != 1) { return false; }
            System.Collections.Generic.List<string> units = new System.Collections.Generic.List<string>(new string[] { "em", "ex", "px", "gd", "rem", "vw", "vh", "vm", "ch", "mm", "cm", "in", "pt", "pc", "deg", "grad", "rad", "turn", "ms", "s", "hz", "khz" });
            return units.Contains(nextToken.val.ToLower());
        }

        bool IsNumber()
        {
            if (nextToken.val.Length > 0)
            {
                return char.IsDigit(nextToken.val[0]);
            }
            return false;
        }

        /*------------------------------------------------------------------------*
         *----- SCANNER DESCRIPTION ----------------------------------------------*
         *------------------------------------------------------------------------*/

        public Parser(Scanner scanner)
        {
            this.scanner = scanner;
            errors = new Errors();
        }

        void SynErr(int n)
        {
            if (errDist >= minErrDist) errors.SynErr(nextToken.line, nextToken.col, n);
            errDist = 0;
        }

        public void SemErr(string msg)
        {
            if (errDist >= minErrDist) errors.SemErr(lastRecognizedToken.line, lastRecognizedToken.col, msg);
            errDist = 0;
        }

        void GetNextToken()
        {
            for (; ; )
            {
                lastRecognizedToken = nextToken;
                nextToken = scanner.Scan();
                if (nextToken.kind <= maxT)
                {
                    ++errDist;
                    break;
                }

                nextToken = lastRecognizedToken;
            }
        }

        void Expect(int n)
        {
            if (nextToken.kind == n) GetNextToken(); else { SynErr(n); }
        }

        bool StartOf(int s)
        {
            return set[s, nextToken.kind];
        }

        void ExpectWeak(int n, int follow)
        {
            if (nextToken.kind == n) GetNextToken();
            else
            {
                SynErr(n);
                while (!StartOf(follow)) GetNextToken();
            }
        }

        bool WeakSeparator(int n, int syFol, int repFol)
        {
            int kind = nextToken.kind;
            if (kind == n) { GetNextToken(); return true; }
            else if (StartOf(repFol)) { return false; }
            else
            {
                SynErr(n);
                while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind]))
                {
                    GetNextToken();
                    kind = nextToken.kind;
                }
                return StartOf(syFol);
            }
        }

        void CSS3()
        {
            CSSDoc = new CSSDocument();
            string cset = null;
            RuleSet rset = null;
            Directive dir = null;

            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            while (nextToken.kind == 5 || nextToken.kind == 6)
            {
                if (nextToken.kind == 5)
                {
                    GetNextToken();
                }
                else
                {
                    GetNextToken();
                }
            }
            while (StartOf(1))
            {
                if (StartOf(2))
                {
                    ruleset(out rset);
                    CSSDoc.RuleSets.Add(rset);
                }
                else
                {
                    directive(out dir);
                    CSSDoc.Directives.Add(dir);
                }
                while (nextToken.kind == 5 || nextToken.kind == 6)
                {
                    if (nextToken.kind == 5)
                    {
                        GetNextToken();
                    }
                    else
                    {
                        GetNextToken();
                    }
                }
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
            }
        }

        void ruleset(out RuleSet rset)
        {
            rset = new RuleSet();
            Selector sel = null;
            Declaration dec = null;

            selector(out sel);
            rset.Selectors.Add(sel);
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            while (nextToken.kind == 25)
            {
                GetNextToken();
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
                selector(out sel);

                if (!string.IsNullOrEmpty(sel.ToString()))
                    rset.Selectors.Add(sel);

                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
            }
            Expect(26);
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            if (StartOf(3))
            {
                declaration(out dec);

                if (IsValideDeclaration(dec))
                    rset.Declarations.Add(dec);
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
                while (nextToken.kind == 27)
                {
                    GetNextToken();
                    while (nextToken.kind == 4)
                    {
                        GetNextToken();
                    }
                    if (nextToken.val.Equals("}")) { GetNextToken(); return; }

                    declaration(out dec);

                    if (IsValideDeclaration(dec))
                        rset.Declarations.Add(dec);
                    while (nextToken.kind == 4)
                    {
                        GetNextToken();
                    }
                }
                if (nextToken.kind == 27)
                {
                    GetNextToken();
                    while (nextToken.kind == 4)
                    {
                        GetNextToken();
                    }
                }
            }
            Expect(28);
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
        }

        bool IsValideDeclaration(Declaration dec)
        {
            bool hasEmptyName = string.IsNullOrEmpty(dec.Name);
            bool hasExpression = dec.Expression != null;
            bool hasOnlyEmptyTerms = true;

            if (!hasEmptyName)
            {
                if (hasExpression)
                {
                    var termsToRemove = new Collection<Term>();

                    foreach (var term in dec.Expression.Terms)
                    {
                        if ((term.Type != TermType.Function) && string.IsNullOrEmpty(term.Value))
                        {
                            termsToRemove.Add(term);
                            continue;
                        }

                        hasOnlyEmptyTerms = false;
                    }

                    foreach (var term in termsToRemove)
                    {
                        dec.Expression.Terms.Remove(term);
                    }
                }
            }

            return !hasEmptyName && hasExpression && !hasOnlyEmptyTerms;
        }

        void directive(out Directive dir)
        {
            dir = new Directive();
            Declaration dec = null;
            RuleSet rset = null;
            Expression exp = null;
            Directive dr = null;
            string ident = null;
            Medium m;

            Expect(23);
            dir.Name = "@";
            if (nextToken.kind == 24)
            {
                GetNextToken();
                dir.Name += "-";
            }
            identity(out ident);
            dir.Name += ident;
            switch (dir.Name.ToLower())
            {
                case "@media": dir.Type = DirectiveType.Media; break;
                case "@import": dir.Type = DirectiveType.Import; break;
                case "@charset": dir.Type = DirectiveType.Charset; break;
                case "@page": dir.Type = DirectiveType.Page; break;
                case "@font-face": dir.Type = DirectiveType.FontFace; break;
                case "@namespace": dir.Type = DirectiveType.Namespace; break;
                default: dir.Type = DirectiveType.Other; break;
            }

            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            if (StartOf(4))
            {
                if (StartOf(5))
                {
                    medium(out m);
                    dir.Mediums.Add(m);
                    while (nextToken.kind == 4)
                    {
                        GetNextToken();
                    }
                    while (nextToken.kind == 25)
                    {
                        GetNextToken();
                        while (nextToken.kind == 4)
                        {
                            GetNextToken();
                        }
                        medium(out m);
                        dir.Mediums.Add(m);
                        while (nextToken.kind == 4)
                        {
                            GetNextToken();
                        }
                    }
                }
                else
                {
                    expr(out exp);
                    dir.Expression = exp;
                    while (nextToken.kind == 4)
                    {
                        GetNextToken();
                    }
                }
            }
            if (nextToken.kind == 26)
            {
                GetNextToken();
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
                if (StartOf(6))
                {
                    while (StartOf(1))
                    {
                        if (dir.Type == DirectiveType.Page || dir.Type == DirectiveType.FontFace)
                        {
                            declaration(out dec);

                            if (IsValideDeclaration(dec))
                                dir.Declarations.Add(dec);
                            while (nextToken.kind == 4)
                            {
                                GetNextToken();
                            }
                            while (nextToken.kind == 27)
                            {
                                GetNextToken();
                                while (nextToken.kind == 4)
                                {
                                    GetNextToken();
                                }
                                if (nextToken.val.Equals("}")) { GetNextToken(); return; }
                                declaration(out dec);
                                if (IsValideDeclaration(dec))
                                    dir.Declarations.Add(dec);
                                while (nextToken.kind == 4)
                                {
                                    GetNextToken();
                                }
                            }
                            if (nextToken.kind == 27)
                            {
                                GetNextToken();
                                while (nextToken.kind == 4)
                                {
                                    GetNextToken();
                                }
                            }
                        }
                        else if (StartOf(2))
                        {
                            ruleset(out rset);
                            dir.RuleSets.Add(rset);
                            while (nextToken.kind == 4)
                            {
                                GetNextToken();
                            }
                        }
                        else
                        {
                            directive(out dr);
                            dir.Directives.Add(dr);
                            while (nextToken.kind == 4)
                            {
                                GetNextToken();
                            }
                        }
                    }
                }
                Expect(28);
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
            }
            else if (nextToken.kind == 27)
            {
                GetNextToken();
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
            }
            else SynErr(50);
        }

        void QuotedString(out string qs)
        {
            qs = "";
            char quote = '\n';
            if (nextToken.kind == 7)
            {
                GetNextToken();
                quote = '\'';
                qs += quote;

                if (nextToken.kind == 7)
                {
                    qs += quote;
                }
                else
                {
                    while (StartOf(7))
                    {
                        GetNextToken();
                        qs += lastRecognizedToken.val;
                        if (nextToken.val.Equals("'") && !lastRecognizedToken.val.Equals("\\")) { qs += quote; break; }
                    }
                }

                Expect(7);
            }
            else if (nextToken.kind == 8)
            {
                GetNextToken();
                quote = '"';
                qs += quote;

                if (nextToken.kind == 8)
                {
                    qs += quote;
                }
                else
                {
                    while (StartOf(8))
                    {
                        GetNextToken();
                        qs += lastRecognizedToken.val;
                        if (nextToken.val.Equals("\"") && !lastRecognizedToken.val.Equals("\\")) { qs += quote; break; }
                    }
                }
                Expect(8);
            }
            else SynErr(51);
        }

        void URI(out string url)
        {
            url = "";
            Expect(9);
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            if (nextToken.kind == 10)
            {
                GetNextToken();
            }
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            if (nextToken.kind == 7 || nextToken.kind == 8)
            {
                QuotedString(out url);
            }
            else if (StartOf(9))
            {
                while (StartOf(10))
                {
                    GetNextToken();
                    url += lastRecognizedToken.val;
                    if (nextToken.val.Equals(")")) { break; }
                }
            }
            else SynErr(52);
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            if (nextToken.kind == 11)
            {
                GetNextToken();
            }
        }

        void medium(out Medium m)
        {
            m = Medium.all;
            switch (nextToken.kind)
            {
                case 12:
                    {
                        GetNextToken();
                        m = Medium.all;
                        break;
                    }
                case 13:
                    {
                        GetNextToken();
                        m = Medium.aural;
                        break;
                    }
                case 14:
                    {
                        GetNextToken();
                        m = Medium.braille;
                        break;
                    }
                case 15:
                    {
                        GetNextToken();
                        m = Medium.embossed;
                        break;
                    }
                case 16:
                    {
                        GetNextToken();
                        m = Medium.handheld;
                        break;
                    }
                case 17:
                    {
                        GetNextToken();
                        m = Medium.print;
                        break;
                    }
                case 18:
                    {
                        GetNextToken();
                        m = Medium.projection;
                        break;
                    }
                case 19:
                    {
                        GetNextToken();
                        m = Medium.screen;
                        break;
                    }
                case 20:
                    {
                        GetNextToken();
                        m = Medium.tty;
                        break;
                    }
                case 21:
                    {
                        GetNextToken();
                        m = Medium.tv;
                        break;
                    }
                default: SynErr(53); break;
            }
        }

        void identity(out string ident)
        {
            ident = "";
            switch (nextToken.kind)
            {
                case 1:
                    {
                        GetNextToken();

                        if (nextToken.kind == 49)
                        {
                            ident = lastRecognizedToken.val + nextToken.val;

                            GetNextToken();

                            ident += nextToken.val;

                            GetNextToken();
                        }

                        break;
                    }
                case 22:
                    {
                        GetNextToken();
                        break;
                    }
                case 9:
                    {
                        GetNextToken();
                        break;
                    }
                case 12:
                    {
                        GetNextToken();
                        break;
                    }
                case 13:
                    {
                        GetNextToken();
                        break;
                    }
                case 14:
                    {
                        GetNextToken();
                        break;
                    }
                case 15:
                    {
                        GetNextToken();
                        break;
                    }
                case 16:
                    {
                        GetNextToken();
                        break;
                    }
                case 17:
                    {
                        GetNextToken();
                        break;
                    }
                case 18:
                    {
                        GetNextToken();
                        break;
                    }
                case 19:
                    {
                        GetNextToken();
                        break;
                    }
                case 20:
                    {
                        GetNextToken();
                        break;
                    }
                case 21:
                    {
                        GetNextToken();
                        break;
                    }
                default: SynErr(54); break;
            }

            if (string.IsNullOrEmpty(ident))
                ident += lastRecognizedToken.val;
        }

        void expr(out Expression exp)
        {
            exp = new Expression();
            char? sep = null;
            Term trm = null;

            term(out trm);
            exp.Terms.Add(trm);
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            while (StartOf(11))
            {
                if (nextToken.kind == 25 || nextToken.kind == 46)
                {
                    if (nextToken.kind == 46)
                    {
                        GetNextToken();
                        sep = '/';
                    }
                    else
                    {
                        GetNextToken();
                        sep = ',';
                    }
                    while (nextToken.kind == 4)
                    {
                        GetNextToken();
                    }
                }
                term(out trm);
                if (sep.HasValue) { trm.Seperator = sep.Value; }
                exp.Terms.Add(trm);
                sep = null;

                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
            }
        }

        void declaration(out Declaration dec)
        {
            dec = new Declaration();
            Expression exp = null;
            string ident = "";

            if (nextToken.kind == 24)
            {
                GetNextToken();
                dec.Name += "-";
            }
            identity(out ident);
            dec.Name += ident;
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            Expect(43);
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            expr(out exp);
            dec.Expression = exp;
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            if (nextToken.kind == 44)
            {
                GetNextToken();
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
                Expect(45);
                dec.Important = true;
                if (lastRecognizedToken.val == "!" && nextToken.val == "Important")
                    GetNextToken();

                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
            }
        }

        void selector(out Selector sel)
        {
            sel = new Selector();
            SimpleSelector ss = null;
            Combinator? cb = null;

            simpleselector(out ss);
            sel.SimpleSelectors.Add(ss);
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            while (StartOf(12))
            {
                if (nextToken.kind == 29 || nextToken.kind == 30 || nextToken.kind == 31)
                {
                    if (nextToken.kind == 29)
                    {
                        GetNextToken();
                        cb = Combinator.PrecededImmediatelyBy;
                    }
                    else if (nextToken.kind == 30)
                    {
                        GetNextToken();
                        cb = Combinator.ChildOf;
                    }
                    else
                    {
                        GetNextToken();
                        cb = Combinator.PrecededBy;
                    }
                }
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
                simpleselector(out ss);
                if (cb.HasValue) { ss.Combinator = cb.Value; }
                sel.SimpleSelectors.Add(ss);

                cb = null;
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
            }
        }

        void simpleselector(out SimpleSelector ss)
        {
            ss = new SimpleSelector();
            ss.ElementName = "";
            string psd = null;
            BoneSoft.CSS.Attribute atb = null;
            SimpleSelector parent = ss;
            string ident = null;

            if (StartOf(3))
            {
                if (nextToken.kind == 24)
                {
                    GetNextToken();
                    ss.ElementName += "-";
                }
                identity(out ident);
                ss.ElementName += ident;
            }
            else if (nextToken.kind == 32)
            {
                GetNextToken();
                ss.ElementName = "*";
            }
            else if (StartOf(13))
            {
                if (nextToken.kind == 33)
                {
                    GetNextToken();
                    if (nextToken.kind == 24)
                    {
                        GetNextToken();
                        ss.ID = "-";
                    }
                    identity(out ident);
                    if (ss.ID == null) { ss.ID = ident; } else { ss.ID += ident; }
                }
                else if (nextToken.kind == 34)
                {
                    GetNextToken();
                    ss.Class = "";
                    if (nextToken.kind == 24)
                    {
                        GetNextToken();
                        ss.Class += "-";
                    }
                    identity(out ident);
                    ss.Class += ident;
                }
                else if (nextToken.kind == 35)
                {
                    attrib(out atb);
                    ss.Attribute = atb;
                }
                else
                {
                    pseudo(out psd);
                    ss.Pseudo = psd;
                }
            }
            else SynErr(55);
            while (StartOf(13))
            {
                SimpleSelector child = new SimpleSelector();
                if (nextToken.kind == 33)
                {
                    GetNextToken();
                    if (nextToken.kind == 24)
                    {
                        GetNextToken();
                        child.ID = "-";
                    }
                    identity(out ident);
                    if (child.ID == null) { child.ID = ident; } else { child.ID += "-"; }
                }
                else if (nextToken.kind == 34)
                {
                    GetNextToken();

                    if (nextToken.kind == 4 || nextToken.kind == 26)
                    {
                        return;
                    }

                    child.Class = "";
                    if (nextToken.kind == 24)
                    {
                        GetNextToken();
                        child.Class += "-";
                    }
                    identity(out ident);
                    child.Class += ident;
                }
                else if (nextToken.kind == 35)
                {
                    attrib(out atb);
                    child.Attribute = atb;
                }
                else
                {
                    pseudo(out psd);
                    child.Pseudo = psd;
                }
                parent.Child = child;
                parent = child;
            }
        }

        void attrib(out BoneSoft.CSS.Attribute atb)
        {
            atb = new BoneSoft.CSS.Attribute();
            atb.Value = "";
            string quote = null;
            string ident = null;

            Expect(35);
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }

            identity(out ident);

            atb.Operand = ident;
            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            if (StartOf(14))
            {
                switch (nextToken.kind)
                {
                    case 36:
                        {
                            GetNextToken();
                            atb.Operator = AttributeOperator.Equals;
                            break;
                        }
                    case 37:
                        {
                            GetNextToken();
                            atb.Operator = AttributeOperator.InList;
                            break;
                        }
                    case 38:
                        {
                            GetNextToken();
                            atb.Operator = AttributeOperator.Hyphenated;
                            break;
                        }
                    case 39:
                        {
                            GetNextToken();
                            atb.Operator = AttributeOperator.EndsWith;
                            break;
                        }
                    case 40:
                        {
                            GetNextToken();
                            atb.Operator = AttributeOperator.BeginsWith;
                            break;
                        }
                    case 41:
                        {
                            GetNextToken();
                            atb.Operator = AttributeOperator.Contains;
                            break;
                        }
                }
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
                if (StartOf(3))
                {
                    if (nextToken.kind == 24)
                    {
                        GetNextToken();
                        atb.Value += "-";
                    }
                    identity(out ident);
                    atb.Value += ident;
                }
                else if (nextToken.kind == 7 || nextToken.kind == 8)
                {
                    QuotedString(out quote);
                    atb.Value = quote;
                }
                else SynErr(56);
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
            }
            Expect(42);
        }

        void pseudo(out string pseudo)
        {
            pseudo = "";
            Expression exp = null;
            string ident = null;

            Expect(43);
            if (nextToken.kind == 43)
            {
                GetNextToken();
            }

            while (nextToken.kind == 4)
            {
                GetNextToken();
            }
            if (nextToken.kind == 24)
            {
                GetNextToken();
                pseudo += "-";
            }
            identity(out ident);
            pseudo += ident;
            if (nextToken.kind == 10)
            {
                GetNextToken();
                pseudo += lastRecognizedToken.val;
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }

                expr(out exp);
                pseudo += exp.ToString();

                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }
                Expect(11);
                pseudo += lastRecognizedToken.val;
            }
        }

        void term(out Term trm)
        {
            trm = new Term();
            string val = "";
            Expression exp = null;
            string ident = null;

            if (nextToken.kind == 7 || nextToken.kind == 8)
            {
                QuotedString(out val);
                trm.Value = val; trm.Type = TermType.String;
            }
            else if (nextToken.kind == 9)
            {
                URI(out val);
                trm.Value = val; trm.Type = TermType.Url;
            }
            else if (nextToken.kind == 47)
            {
                GetNextToken();
                identity(out ident);
                trm.Value = "U\\" + ident; trm.Type = TermType.Unicode;
            }
            else if (nextToken.kind == 33)
            {
                HexValue(out val);
                trm.Value = val; trm.Type = TermType.Hex;
            }
            else if (nextToken.kind == 43)
            {
                trm.Value += ":";
                GetNextToken();
                while (nextToken.kind == 4)
                {
                    GetNextToken();
                }

                trm.Value += nextToken.val;
                trm.Type = TermType.String;
                GetNextToken();
            }
            else if (StartOf(15))
            {
                bool minus = false;
                if (nextToken.kind == 24)
                {
                    GetNextToken();
                    minus = true;
                }
                if (StartOf(16))
                {
                    identity(out ident);
                    trm.Value = ident; trm.Type = TermType.String;
                    if (minus) { trm.Value = "-" + trm.Value; }
                    while (nextToken.kind == 4)
                    {
                        GetNextToken();
                    }
                    if (StartOf(17))
                    {
                        while ((nextToken.kind == 34 && lastRecognizedToken.kind != 4) || nextToken.kind == 36 || nextToken.kind == 43)
                        {
                            if (nextToken.kind == 43)
                            {
                                GetNextToken();
                                trm.Value += lastRecognizedToken.val;
                                if (StartOf(18))
                                {
                                    if (nextToken.kind == 43)
                                    {
                                        GetNextToken();
                                        trm.Value += lastRecognizedToken.val;
                                    }
                                    if (nextToken.kind == 24)
                                    {
                                        GetNextToken();
                                        trm.Value += lastRecognizedToken.val;
                                    }
                                    identity(out ident);
                                    trm.Value += ident;
                                }
                                else if (nextToken.kind == 33)
                                {
                                    HexValue(out val);
                                    trm.Value += val;
                                }
                                else if (StartOf(19))
                                {
                                    while (nextToken.kind == 3)
                                    {
                                        GetNextToken();
                                        trm.Value += lastRecognizedToken.val;
                                    }
                                    if (nextToken.kind == 34)
                                    {
                                        GetNextToken();
                                        trm.Value += ".";
                                        while (nextToken.kind == 3)
                                        {
                                            GetNextToken();
                                            trm.Value += lastRecognizedToken.val;
                                        }
                                    }
                                }
                                else SynErr(57);
                            }
                            else if (nextToken.kind == 34)
                            {
                                GetNextToken();
                                trm.Value += lastRecognizedToken.val;
                                if (nextToken.kind == 24)
                                {
                                    GetNextToken();
                                    trm.Value += lastRecognizedToken.val;
                                }
                                identity(out ident);
                                trm.Value += ident;
                            }
                            else
                            {
                                GetNextToken();
                                trm.Value += lastRecognizedToken.val;
                                while (nextToken.kind == 4)
                                {
                                    GetNextToken();
                                }
                                if (nextToken.kind == 24)
                                {
                                    GetNextToken();
                                    trm.Value += lastRecognizedToken.val;
                                }
                                if (StartOf(16))
                                {
                                    identity(out ident);
                                    trm.Value += ident;
                                }
                                else if (StartOf(19))
                                {
                                    while (nextToken.kind == 3)
                                    {
                                        GetNextToken();
                                        trm.Value += lastRecognizedToken.val;
                                    }
                                }
                                else SynErr(58);
                            }
                        }
                    }
                    if (nextToken.kind == 10)
                    {
                        GetNextToken();
                        while (nextToken.kind == 4)
                        {
                            GetNextToken();
                        }
                        expr(out exp);
                        Function func = new Function();
                        func.Name = trm.Value;
                        func.Expression = exp;
                        trm.Value = null;
                        trm.Function = func;
                        trm.Type = TermType.Function;

                        while (nextToken.kind == 4)
                        {
                            GetNextToken();
                        }
                        Expect(11);
                    }
                }
                else if (StartOf(15))
                {
                    if (nextToken.kind == 29)
                    {
                        GetNextToken();
                        trm.Sign = '+';
                    }
                    if (minus) { trm.Sign = '-'; }
                    while (nextToken.kind == 3)
                    {
                        GetNextToken();
                        val += lastRecognizedToken.val;
                    }
                    if (nextToken.kind == 34)
                    {
                        GetNextToken();
                        val += lastRecognizedToken.val;
                        while (nextToken.kind == 3)
                        {
                            GetNextToken();
                            val += lastRecognizedToken.val;
                        }
                    }
                    if (StartOf(20))
                    {
                        if (nextToken.val.ToLower().Equals("n"))
                        {
                            Expect(22);
                            val += lastRecognizedToken.val;
                            if (nextToken.kind == 24 || nextToken.kind == 29)
                            {
                                if (nextToken.kind == 29)
                                {
                                    GetNextToken();
                                    val += lastRecognizedToken.val;
                                }
                                else
                                {
                                    GetNextToken();
                                    val += lastRecognizedToken.val;
                                }
                                Expect(3);
                                val += lastRecognizedToken.val;
                                while (nextToken.kind == 3)
                                {
                                    GetNextToken();
                                    val += lastRecognizedToken.val;
                                }
                            }
                        }
                        else if (nextToken.kind == 48)
                        {
                            GetNextToken();
                            trm.Unit = Unit.Percent; // %
                        }
                        else
                        {
                            if (IsUnit())
                            {
                                identity(out ident);
                                try
                                {
                                    trm.Unit = (Unit)Enum.Parse(typeof(Unit), ident, true);
                                }
                                catch
                                {
                                    errors.SemErr(lastRecognizedToken.line, lastRecognizedToken.col, string.Format("Unrecognized unit '{0}'", ident));
                                }
                            }
                        }
                    }
                    trm.Value = val; trm.Type = TermType.Number;
                }
                else SynErr(59);
            }
            else SynErr(60);
        }

        void HexValue(out string val)
        {
            val = "";
            bool found = false;

            Expect(33);
            val += lastRecognizedToken.val;
            if (StartOf(19))
            {
                while (nextToken.kind == 3)
                {
                    GetNextToken();
                    val += lastRecognizedToken.val;
                }
            }
            else if (PartOfHex(val))
            {
                Expect(1);
                val += lastRecognizedToken.val; found = true;
            }
            else SynErr(61);
            if (!found && PartOfHex(val))
            {
                Expect(1);
                val += lastRecognizedToken.val;
            }
        }

        public void Parse()
        {
            nextToken = new Token();
            nextToken.val = "";
            GetNextToken();
            CSS3();

            Expect(0);
        }

        bool[,] set = {
		{T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x},
		{x,T,x,x, x,x,x,x, x,T,x,x, T,T,T,T, T,T,T,T, T,T,T,T, T,x,x,x, x,x,x,x, T,T,T,T, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x},
		{x,T,x,x, x,x,x,x, x,T,x,x, T,T,T,T, T,T,T,T, T,T,T,x, T,x,x,x, x,x,x,x, T,T,T,T, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x},
		{x,T,x,x, x,x,x,x, x,T,x,x, T,T,T,T, T,T,T,T, T,T,T,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x},
		{x,T,x,T, T,x,x,T, T,T,x,x, T,T,T,T, T,T,T,T, T,T,T,x, T,T,T,T, x,T,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,T,T, T,x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, T,T,T,T, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x},
		{x,T,x,x, x,x,x,x, x,T,x,x, T,T,T,T, T,T,T,T, T,T,T,T, T,x,x,x, T,x,x,x, T,T,T,T, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x},
		{x,T,T,T, T,T,T,x, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,x},
		{x,T,T,T, T,T,T,T, x,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,x},
		{x,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,x},
		{x,T,T,T, x,T,T,x, x,T,T,x, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,x},
		{x,T,x,T, T,x,x,T, T,T,x,x, T,T,T,T, T,T,T,T, T,T,T,x, T,T,x,x, x,T,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,T,T, T,x,x},
		{x,T,x,x, T,x,x,x, x,T,x,x, T,T,T,T, T,T,T,T, T,T,T,x, T,x,x,x, x,T,T,T, T,T,T,T, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,T,T, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,T,T, T,T,x,x, x,x,x,x, x,x,x},
		{x,T,x,T, T,x,x,T, T,T,x,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,x,x, T,T,T,T, x,x,x,x, x,x,x,T, T,x,T,T, T,x,x},
		{x,T,x,x, x,x,x,x, x,T,x,x, T,T,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x},
		{x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,x,x,x, x,x,x,T, x,x,x,x, x,x,x},
		{x,T,x,x, x,x,x,x, x,T,x,x, T,T,T,T, T,T,T,T, T,T,T,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x},
		{x,T,x,T, T,x,x,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,x,x, T,T,T,T, T,x,x,x, x,x,x,T, T,x,T,T, T,x,x},
		{x,T,x,x, x,x,x,x, x,T,x,x, T,T,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x}
	};
    } // end Parser

    public delegate void ParserMessage(string msg);

    public class Errors
    {
        public event ParserMessage OnError;

        public event ParserMessage OnWarning;

        public int count = 0;                                    // number of errors detected
        public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
        public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

        public void SynErr(int line, int col, int n)
        {
            string s;
            switch (n)
            {
                case 0: s = "EOF expected"; break;
                case 1: s = "ident expected"; break;
                case 2: s = "newline expected"; break;
                case 3: s = "digit expected"; break;
                case 4: s = "whitespace expected"; break;
                case 5: s = "\"<!--\" expected"; break;
                case 6: s = "\"-->\" expected"; break;
                case 7: s = "\"\'\" expected"; break;
                case 8: s = "\"\"\" expected"; break;
                case 9: s = "\"url\" expected"; break;
                case 10: s = "\"(\" expected"; break;
                case 11: s = "\")\" expected"; break;
                case 12: s = "\"all\" expected"; break;
                case 13: s = "\"aural\" expected"; break;
                case 14: s = "\"braille\" expected"; break;
                case 15: s = "\"embossed\" expected"; break;
                case 16: s = "\"handheld\" expected"; break;
                case 17: s = "\"print\" expected"; break;
                case 18: s = "\"projection\" expected"; break;
                case 19: s = "\"screen\" expected"; break;
                case 20: s = "\"tty\" expected"; break;
                case 21: s = "\"tv\" expected"; break;
                case 22: s = "\"n\" expected"; break;
                case 23: s = "\"@\" expected"; break;
                case 24: s = "\"-\" expected"; break;
                case 25: s = "\",\" expected"; break;
                case 26: s = "\"{\" expected"; break;
                case 27: s = "\";\" expected"; break;
                case 28: s = "\"}\" expected"; break;
                case 29: s = "\"+\" expected"; break;
                case 30: s = "\">\" expected"; break;
                case 31: s = "\"~\" expected"; break;
                case 32: s = "\"*\" expected"; break;
                case 33: s = "\"#\" expected"; break;
                case 34: s = "\".\" expected"; break;
                case 35: s = "\"[\" expected"; break;
                case 36: s = "\"=\" expected"; break;
                case 37: s = "\"~=\" expected"; break;
                case 38: s = "\"|=\" expected"; break;
                case 39: s = "\"$=\" expected"; break;
                case 40: s = "\"^=\" expected"; break;
                case 41: s = "\"*=\" expected"; break;
                case 42: s = "\"]\" expected"; break;
                case 43: s = "\":\" expected"; break;
                case 44: s = "\"!\" expected"; break;
                case 45: s = "\"important\" expected"; break;
                case 46: s = "\"/\" expected"; break;
                case 47: s = "\"U\\\\\" expected"; break;
                case 48: s = "\"%\" expected"; break;
                case 49: s = "??? expected"; break;
                case 50: s = "invalid directive"; break;
                case 51: s = "invalid QuotedString"; break;
                case 52: s = "invalid URI"; break;
                case 53: s = "invalid medium"; break;
                case 54: s = "invalid identity"; break;
                case 55: s = "invalid simpleselector"; break;
                case 56: s = "invalid attrib"; break;
                case 57: s = "invalid term"; break;
                case 58: s = "invalid term"; break;
                case 59: s = "invalid term"; break;
                case 60: s = "invalid term"; break;
                case 61: s = "invalid HexValue"; break;

                default: s = "error " + n; break;
            }
            errorStream.WriteLine(errMsgFormat, line, col, s);
            if (OnError != null)
            {
                OnError(string.Format(errMsgFormat, line, col, s));
            }
            count++;
        }

        public void SemErr(int line, int col, string s)
        {
            errorStream.WriteLine(errMsgFormat, line, col, s);
            if (OnError != null)
            {
                OnError(string.Format(errMsgFormat, line, col, s));
            }
            count++;
        }

        public void SemErr(string s)
        {
            errorStream.WriteLine(s);
            if (OnError != null)
            {
                OnError(s);
            }
            count++;
        }

        public void Warning(int line, int col, string s)
        {
            errorStream.WriteLine(errMsgFormat, line, col, s);
            if (OnWarning != null)
            {
                OnWarning(string.Format(errMsgFormat, line, col, s));
            }
        }

        public void Warning(string s)
        {
            errorStream.WriteLine(s);
            if (OnWarning != null)
            {
                OnWarning(s);
            }
        }
    } // Errors

    public class FatalError : Exception
    {
        public FatalError(string m)
            : base(m)
        {
        }
    }
}