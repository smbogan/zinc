using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.AST
{
    public class TokenStream
    {
        List<Token> Tokens { get; set; }

        public int Count
        {
            get
            {
                return Tokens.Count;
            }
        }

        public Token this[int i]
        {
            get
            {
                return Tokens[i];
            }
        }

        public TokenStream()
        {
            Tokens = new List<Token>();
        }

        public void Add(Token token)
        {
            Tokens.Add(token);
        }

        public void Remove(int index)
        {
            Tokens.RemoveAt(index);
        }

        
    }
}
