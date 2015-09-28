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

        public LexerError Error { get; private set; }

        public int ErrorPosition { get; private set; }

        private Dictionary<LexerState, LexerOptions> StateTable;

        public Lexer()
        {
            Tokens = new TokenStream();

            //Setup state table
            StateTable = new Dictionary<LexerState, LexerOptions>()
            {
                {LexerState.Start, 
                    new LexerOptions(
                        new LexerOption(IsNumber, ConsumeNumber),
                        new LexerOption(IsSemicolon, ConsumeSemicolon),
                        new LexerOption(IsBlockComment, ConsumeBlockComment),
                        new LexerOption(IsHashComment, ConsumeHashComment),
                        new LexerOption(IsLineComment, ConsumeLineComment),
                        new LexerOption(IsWhitespace, ConsumeWhitespace),
                        new LexerOption(IsIdentifier, ConsumeIdentifier),
                        new LexerOption(IsOperator, ConsumeOperator),
                        new LexerOption(IsEndOfStream, NullOperation)
                        )}
            };
        }

        private bool IsNumber(StreamWindow window)
        {
            char c;

            if(window.Peek(0, out c))
            {
                if(CharacterClasses.IsDigit(c))
                {
                    return true;
                }
            }
            else
            {
                State = LexerState.End;
            }

            return false;
        }

        private void ConsumeNumber(StreamWindow window)
        {
            int count = window.CountWhile(CharacterClasses.IsDigit);

            if (count > 0)
            {
                Token token = new Token(TokenType.Integer, window, count);
                Tokens.Add(token);
            }
        }

        private bool IsSemicolon(StreamWindow window)
        {
            char c;

            if(window.Peek(0, out c))
            {
                if(c == ';')
                {
                    return true;
                }
            }
            else
            {
                State = LexerState.End;
            }

            return false;
        }

        private void ConsumeSemicolon(StreamWindow window)
        {
            Token token = new Token(TokenType.Semicolon, window, 1);
            Tokens.Add(token);
        }

        private bool IsHashComment(StreamWindow window)
        {
            char c;

            if(window.Peek(0, out c))
            {
                if(c == '#')
                {
                    return true;
                }
            }
            else
            {
                State = LexerState.End;
            }

            return false;
        }

        private void ConsumeHashComment(StreamWindow window)
        {
            int lineCommentLength = window.CountWhileNot(CharacterClasses.IsNewLineCharacter);

            if (lineCommentLength > 0)
            {
                Token token = new Token(TokenType.HashLineComment, window, lineCommentLength);
                Tokens.Add(token);
            }
        }

        private bool IsBlockComment(StreamWindow window)
        {
            char c, d;

            bool notEndOfStream;

            if ((notEndOfStream = window.Peek(0, out c)) && window.Peek(1, out d))
            {
                if (c == '/' && d == '*')
                {
                    return true;
                }
            }

            if (!notEndOfStream)
            {
                State = LexerState.End;
            }

            return false;
        }

        private void ConsumeBlockComment(StreamWindow window)
        {
            int blockCommentLength = window.CountWhileNot((c, d) => c == '*' && d == '/');

            if (blockCommentLength > 0)
            {
                Token token = new Token(TokenType.LineComment, window, blockCommentLength);
                Tokens.Add(token);
            }
            else
            {
                State = LexerState.Error;
                Error = LexerError.BlockCommentWithNoEnd;
            }
        }

        private bool IsLineComment(StreamWindow window)
        {
            char c, d;

            bool notEndOfStream;

            if((notEndOfStream = window.Peek(0, out c)) && window.Peek(1, out d))
            {
                if(c == '/' && d == '/')
                {
                    return true;
                }
            }

            if(!notEndOfStream)
            {
                State = LexerState.End;
            }

            return false;
        }

        private void ConsumeLineComment(StreamWindow window)
        {
            int lineCommentLength = window.CountWhileNot(CharacterClasses.IsNewLineCharacter);

            if (lineCommentLength > 0)
            {
                Token token = new Token(TokenType.LineComment, window, lineCommentLength);
                Tokens.Add(token);
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

        public void Lex(Stream s, Encoding encoding)
        {
            State = LexerState.Start;
            Error = LexerError.None;

            StreamWindow window = new StreamWindow(s, encoding);

            while (State != LexerState.End && State != LexerState.Error)
            {
                StateTable[State].Do(window);
            }
        }
    }
}
