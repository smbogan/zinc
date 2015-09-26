using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public class ForLoop : Expression
    {
        public Expression Initial { get; private set; }
        public Expression Final { get; private set; }
        public Expression Body { get; private set; }

        public ForLoop(Expression initial, Expression final, Expression body)
        {
            Initial = initial;
            Final = final;
            Body = body;
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.ForLoop(Initial, Final, Body);
        }
    }
}
