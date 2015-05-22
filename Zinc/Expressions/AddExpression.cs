using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public class AddExpression : BinaryExpression
    {
        public AddExpression(Expression left, Expression right)
            : base(left, right)
        {

        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Add(Left, Right);
        }
    }
}
