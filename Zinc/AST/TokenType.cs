using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.AST
{
    public enum TokenType : uint
    {
        WS,
        LineComment,
        BlockComment,
        StringDoubleQuotesLiteral,
        StringSingleQuotesLiteral,
        Identifier,
        ArrayLiteral,
        IntegerLiteral,
        FloatLiteral,
        Operator,

        //Keywords

    }
}
