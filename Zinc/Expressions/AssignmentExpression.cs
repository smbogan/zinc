using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public class AssignmentExpression : Expression
    {
        public AssignableExpression Left { get; private set; }
        public Expression Right { get; private set; }

        public AssignmentExpression(AssignableExpression assignable, Expression expr)
        {
            Left = assignable;
            Right = expr;
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Assignment(Left, Right);
        }
    }
}
