using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.AST
{
    public class LexerOption
    {
        public Func<StreamWindow, bool> Test { get; private set; }

        public Action<StreamWindow> Do { get; private set; }

        public LexerOption(Func<StreamWindow, bool> test, Action<StreamWindow> act)
        {
            Test = test;
            Do = act;
        }
    }
}
