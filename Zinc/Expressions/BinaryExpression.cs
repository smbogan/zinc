using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public abstract class BinaryExpression : Expression
    {
        public Expression Left { get; private set; }
        public Expression Right { get; private set; }

        public BinaryExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }
    }
}
