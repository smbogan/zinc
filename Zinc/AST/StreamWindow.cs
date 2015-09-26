using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Zinc.AST
{
    public class StreamWindow
    {
        public int Position { get; private set; }

        public int Line { get; private set; }

        public int Offset { get; private set; }

        public TextReader Reader { get; private set; }

        public Stream UnderlyingStream { get; private set; }

        public Encoding ReaderEncoding { get; private set; }

        char[] buffer;
        int bufferPosition;
        int bufferCount;

        List<char> temp;

        public StreamWindow(Stream stream, Encoding encoding)
        {
            this.ReaderEncoding = encoding;
            this.UnderlyingStream = stream;
            this.Reader = new StreamReader(stream, encoding, false, 16384, true);
            temp = new List<char>();
            buffer = new char[16384];
            Line = 1;
            Offset = 0;
            
        }

        private bool LoadIntoBuffer()
        {
            bufferCount = Reader.Read(buffer, 0, buffer.Length);
            bufferPosition = 0;

            return bufferCount > 0;
        }

        private bool Load(int p)
        {
            if(bufferCount == 0)
            {
                if (!LoadIntoBuffer())
                {
                    return false;
                }
            }

            for(int j = temp.Count - 1; j < p; j++)
            {
                if(bufferPosition >= bufferCount)
                {
                    if(!LoadIntoBuffer())
                    {
                        return false;
                    }
                }

                temp.Add(buffer[bufferPosition++]);
            }

            return true;
        }

        public bool Peek(int p, out char value)
        {
            if(temp.Count > p)
            {
                value = temp[p];
                return true;
            }
            else
            {
                if(!Load(p))
                {
                    value = '\0';  //Garbage
                    return false;
                }

                return Peek(p, out value);
            }
        }

        public int CountWhile(Predicate<char> test)
        {
            int i = -1;

            char c;

            do
            {
                i++;

                if(!Peek(i, out c))
                {
                    return i;
                }
            } while (test(c));

            return i;
        }

        public int CountWhile(Func<char, char, bool> test)
        {
            int i = -1;

            char c, d;

            do
            {
                i++;

                if (!Peek(i, out c))
                {
                    return i;
                }

                if(!Peek(i + 1, out d))
                {
                    return i + 1;
                }
            } while (test(c, d));

            return i + 2;
        }

        public int CountWhileNot(Predicate<char> test)
        {
            return CountWhile((c) => !test(c));
        }

        public int CountWhileNot(Func<char, char, bool> test)
        {
            return CountWhile((c, d) => !test(c, d));
        }

        public void Accept(int count)
        {
            if(temp.Count < count)
            {
                char unused;
                if(!Peek(count, out unused))
                {
                    throw new Exception("Internal error: Lexer accepted more characters than can be peeked.");
                }
            }

            for (int i = 0; i < count; i++)
            {
                switch(temp[i])
                {
                    case '\n':
                        Line++;
                        Offset = 0;
                        break;
                    default:
                        Offset++;
                        break;
                }
            }

            Position += count;

            temp.RemoveRange(0, count);
        }
    }
}
