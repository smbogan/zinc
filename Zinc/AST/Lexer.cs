using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Zinc.AST
{
    public class Lexer
    {
        TokenStream Tokens { get; private set; }

        LexerState State { get; private set; }

        public Dictionary<LexerState, LexerOptions> StateTable = new Dictionary<LexerState, LexerOptions>()
        {
            {LexerState.Start, 
                new LexerOptions(
                    new LexerOption()
                    )}
        };

        public Lexer()
        {
            Tokens = new TokenStream();
        }

        public void Lex(Stream s, Encoding encoding)
        {
            StreamWindow window = new StreamWindow(new StreamReader(s, encoding, false, 16384, true));

            while(State != LexerState.End && State != LexerState.Error)
            {
                StateTable[State].Do(window);
            }
        }

        public bool IsWhitespace(StreamWindow window)
        {
            char c;
            if(window.Peek(0, out c))
            {
                return CharacterClasses.IsWhitespace(c);
            }
            else
            {
                State = LexerState.End;
            }

            return false;
        }
    }
}
