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

        public Encoding StreamEncoding { get; private set; }

        public int Length
        {
            get
            {
                return End - Start;
            }
        }

        public Stream Source { get; private set; }

        public Token(TokenType type, int start, int end, int line, int offset, Stream stream, Encoding encoding)
        {
            LexerTokenType = type;
            Start = start;
            End = end;
            Offset = offset;
            Line = line;
            Source = stream;
            StreamEncoding = encoding;
        }

        public Token(TokenType type, StreamWindow window, int length)
            : this(type, window.Position, window.Position + length, window.Line, window.Offset, window.UnderlyingStream, window.ReaderEncoding)
        {
            window.Accept(length);
        }

        public override string ToString()
        {
            if (LexerTokenType == TokenType.WS)
                return Enum.GetName(typeof(TokenType), LexerTokenType);

            long pos = Source.Position;
            Source.Seek(0, SeekOrigin.Begin);

            char[] data = new char[End - Start];

            int count = 0;

            using (System.IO.StreamReader sr = new StreamReader(Source, StreamEncoding, false, 2000, true))
            {
                while (count < Start)
                {
                    sr.Read();
                    count++;
                }

                sr.ReadBlock(data, 0, End - Start);
            }

            Source.Seek(pos, SeekOrigin.Begin);

            return new String(data) + " [" + Enum.GetName(typeof(TokenType), LexerTokenType) + "]";
        }
    }
}
