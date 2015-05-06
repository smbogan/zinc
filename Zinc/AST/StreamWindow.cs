using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Zinc.AST
{
    public class StreamWindow : IDisposable
    {
        public ulong Position { get; private set; }

        public ulong Line { get; private set; }

        public ulong Offset { get; private set; }

        char[] buffer;
        int bufferPosition;
        int bufferCount;

        TextReader reader;
        List<char> temp;

        public StreamWindow(TextReader reader)
        {
            this.reader = reader;
            temp = new List<char>();
            buffer = new char[16384];
            Line = 1;
            Offset = 0;
        }

        private bool LoadIntoBuffer()
        {
            bufferCount = reader.Read(buffer, 0, buffer.Length);
            bufferPosition = 0;

            return bufferCount > 0;
        }

        private bool Load(int p)
        {
            for(int j = 0; j < p; j++)
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

            temp.RemoveRange(0, count);
        }
    }
}
