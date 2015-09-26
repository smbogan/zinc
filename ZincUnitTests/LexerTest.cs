using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Zinc.AST;

namespace ZincUnitTests
{
    [TestClass]
    public class LexerTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string code = @"
                /* block comment
                stuff here
                end of comment */
                variable = 55; //Comment
                wallaby = 44;
                zinc = variable + wallaby;
            ";

            using (MemoryStream ms = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(code)))
            {
                Lexer lex = new Lexer();
                lex.Lex(ms, System.Text.Encoding.Unicode);
                String s = lex.Tokens[1].ToString();
            }
        }
    }
}
