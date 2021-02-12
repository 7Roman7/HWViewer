using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace HWViewer.Tools
{
    static class CodeFormatter
    {
        private const string fontColorDefault = "scbFontDefault";
        private const string fontColorSpecial = "scbFontSpecial";
        private const string fontColorClass = "scbFontClass";
        private const string fontColorComment = "scbFontComment";
        private const string fontColorStrings = "scbFontStrings";


        static string DeleteTabsFromStart(string s)
        {
            var minEmptySymbols = s.Length;
            var lines = s.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var l in lines)
            {
                var emptySymbols = 0;
                for (var i = 0; i < l.Length; i++)
                    if (l[i] == '\t' || l[i] == ' ')
                        emptySymbols++;
                    else
                        break;

                if (emptySymbols < minEmptySymbols)
                    minEmptySymbols = emptySymbols;
            }

            StringBuilder sbResult = new StringBuilder();

            foreach (var l in lines)
                sbResult.Append(l.Substring(minEmptySymbols) + Environment.NewLine);

            return sbResult.ToString();
        }

        static string[] blueWords = {
            "static","const","using","namespace",
            "class","partial","sealed",
            "void","return", "ref", "out", "params",
            "for","foreach","while","do",
            "break","continue",
            "if", "else",
            "as", "is", "in",
            "public","private","internal","protected",
            "object","bool","byte","int","long","float","double","decimal","char","string",
            "true", "false",
            "new","var",
            "try","catch","finally","throw",
        };

        static string[] greenWords = {
            "Console",
            "Convert",
            "Exception",
            "Assert","TestMethod","TestClass",
            "Environment","Application",
            "Thread","Task",
            "Random",
            "String", "StringBuilder",
            "List","Dictionary",
            "StreamReader","StreamWriter",
            "RoutedEventArgs","EventArgs",
            "MessageBox",
            "Registry", "RegistryKey"

        };

        static char[] delimeters = { ' ','.',',',':',';','!','?',
            '(',')','[',']','{','}','<','>',
            '\r','\n','\t'
        };

        public static void Fill(FlowDocument fDocument, string code)
        {
            code = DeleteTabsFromStart(code);
            fDocument.Blocks.Clear();
            StringBuilder word = new StringBuilder();
            var paragraph = new Paragraph();

            code = code.Replace("\r\n", "\n");

            for (int i = 0; i < code.Length; i++)
            {
                char workingSymbol = code[i];

                //Comments
                if (workingSymbol == '/' && (code.Length > code[i + 1]) && (code[i].ToString() + code[i + 1] == "//"))
                {
                    var posEnd = code.IndexOf("\n", i);
                    if (posEnd == -1)
                        posEnd = code.Length -1;//до конца

                    var subString = code.Substring(i, posEnd - i);


                    var runForComment = new Run(subString);
                    runForComment.SetResourceReference(TextElement.ForegroundProperty, fontColorComment);
                    paragraph.Inlines.Add(runForComment);
                    i = posEnd - 1;
                    continue;
                }

                //RedString
                if (workingSymbol == '"')
                {
                    var posNext = code.IndexOf("\"", i + 1);
                    if (posNext != -1)
                    {
                        var subString = code.Substring(i, posNext - i + 1);
                        var runForString = new Run(subString);
                        runForString.SetResourceReference(TextElement.ForegroundProperty, fontColorStrings);
                        paragraph.Inlines.Add(runForString);
                        i = posNext;
                        continue;
                    }
                }

                if (!delimeters.Contains(workingSymbol))
                {
                    word.Append(workingSymbol);
                    if (i != code.Length)
                        continue;
                }
                if (word.Length > 0)
                {
                    var run = new Run(word.ToString());
                    if (blueWords.Contains(word.ToString()))
                        run.SetResourceReference(TextElement.ForegroundProperty, fontColorSpecial);
                    else if (greenWords.Contains(word.ToString()))
                        run.SetResourceReference(TextElement.ForegroundProperty, fontColorClass);
                    else
                        run.SetResourceReference(TextElement.ForegroundProperty, fontColorDefault);

                    paragraph.Inlines.Add(run);
                    word.Clear();
                }

                if (workingSymbol == '\n')
                {
                    paragraph.Inlines.Add(new LineBreak());
                }
                else
                {
                    var runForDelimeter = new Run(workingSymbol.ToString());
                    runForDelimeter.SetResourceReference(TextElement.ForegroundProperty, fontColorDefault);
                    paragraph.Inlines.Add(runForDelimeter);
                }
            }

            fDocument.Blocks.Add(paragraph);
        }
    }
}
