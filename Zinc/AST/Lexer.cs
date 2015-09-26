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
        public TokenStream Tokens { get; private set; }

        private LexerState State { get; set; }

        private Dictionary<LexerState, LexerOptions> StateTable;

        public Lexer()
        {
            Tokens = new TokenStream();

            //Setup state table
            StateTable = new Dictionary<LexerState, LexerOptions>()
            {
                {LexerState.Start, 
                    new LexerOptions(
                        new LexerOption(IsWhitespace, ConsumeWhitespace),
                        new LexerOption(IsIdentifier, ConsumeIdentifier),
                        new LexerOption(IsOperator, ConsumeOperator),
                        new LexerOption(IsEndOfStream, NullOperation)
                        )}
            };
        }



        public void Lex(Stream s, Encoding encoding)
        {
            StreamWindow window = new StreamWindow(s, encoding);

            while(State != LexerState.End && State != LexerState.Error)
            {
                StateTable[State].Do(window);
            }
        }

        private bool IsWhitespace(StreamWindow window)
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

        private void ConsumeWhitespace(StreamWindow window)
        {
            int whitespaceLength = window.CountWhile(CharacterClasses.IsWhitespace);

            if (whitespaceLength > 0)
            {
                Token token = new Token(TokenType.WS, window, whitespaceLength);
                Tokens.Add(token);
            }
        }

        private bool IsIdentifier(StreamWindow window)
        {
            char c;
            if (window.Peek(0, out c))
            {
                return CharacterClasses.IsIdentifier(c);
            }
            else
            {
                State = LexerState.End;
            }

            return false;
        }

        private void ConsumeIdentifier(StreamWindow window)
        {
            int identifierLength = window.CountWhile(CharacterClasses.IsIdentifier);

            if (identifierLength > 0)
            {
                Token token = new Token(TokenType.Identifier, window, identifierLength);
                Tokens.Add(token);
            }
        }

        private void NullOperation(StreamWindow window)
        {
            //Do nothing
        }

        private bool IsEndOfStream(StreamWindow window)
        {
            char c;
            if (window.Peek(0, out c))
            {
                return false;
            }
            else
            {
                State = LexerState.End;
                return true;
            }
        }


        private bool IsOperator(StreamWindow window)
        {
            char c;
            if (window.Peek(0, out c))
            {
                return CharacterClasses.IsOperator(c);
            }
            else
            {
                State = LexerState.End;
                return false;
            }
        }


        private void ConsumeOperator(StreamWindow window)
        {
            int operatorLength = window.CountWhile(CharacterClasses.IsOperator);

            if (operatorLength > 0)
            {
                Token token = new Token(TokenType.Operator, window, operatorLength);
                Tokens.Add(token);
            }
        }


    }
}
