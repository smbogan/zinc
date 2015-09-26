using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zinc.Utility;

namespace Zinc.Expressions
{
    public class Block : Expression
    {
        public ReadOnlyList<Expression> Expressions { get; private set; }

        public Block(params Expression[] expressions)
        {
            Expressions = new ReadOnlyList<Expression>(expressions);
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Block(Expressions);
        }
    }
}
