using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.AST
{
    public class LexerOptions
    {
        public LexerOption[] Options { get; private set; }

        public LexerOptions(params LexerOption[] options)
        {
            Options = options;
        }

        public void Do(StreamWindow window)
        {
            foreach(LexerOption option in Options)
            {
                if(option.Test(window))
                {
                    option.Do(window);
                    return;
                }
            }

            throw new Exception("Internal error: No alternative matched in the lexer.");
        }
    }
}
