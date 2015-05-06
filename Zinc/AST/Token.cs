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

        public ulong Start { get; private set; }

        public ulong End { get; private set; }

        public ulong Line { get; private set; }

        public ulong Offset { get; private set; }

        public ulong Length
        {
            get
            {
                return End - Start;
            }
        }

        public Stream Source { get; private set; }

        public Token(TokenType type, ulong start, ulong end, ulong line, ulong offset, Stream stream)
        {
            LexerTokenType = type;
            Start = start;
            End = end;
        }
    }
}
