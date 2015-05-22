using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.AST
{
    public class Token
    {
        public TokenType LexerTokenType { get; private set; }

        public int Start { get; private set; }

        public int End { get; private set; }

        public int Line { get; private set; }

        public int Offset { get; private set; }

        public int Length
        {
            get
            {
                return End - Start;
            }
        }

        public Stream Source { get; private set; }

        public Token(TokenType type, int start, int end, int line, int offset, Stream stream)
        {
            LexerTokenType = type;
            Start = start;
            End = end;
            Offset = offset;
            Line = line;
            Source = stream;
        }

        public Token(TokenType type, StreamWindow window, int length)
            : this(type, window.Position, window.Position + length, window.Line, window.Offset, window.UnderlyingStream)
        {
            window.Accept(length);
        }
    }
}
