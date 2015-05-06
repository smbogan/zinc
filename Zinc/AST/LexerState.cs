using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.AST
{
    public enum LexerState
    {
        Start,
        DoubleString,
        SingleString,
        LineComment,
        Identifier,
        Operator,
        End,
        Error,

    }
}
