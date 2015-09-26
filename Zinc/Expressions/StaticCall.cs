using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zinc.Utility;

namespace Zinc.Expressions
{
    public class StaticCall : Expression
    {
        public StaticMethod Method { get; private set; }
        public ReadOnlyList<Expression> Parameters { get; private set; }

        public StaticCall(StaticMethod method, params Expression[] parameters)
        {
            this.Method = method;
            this.Parameters = new ReadOnlyList<Expression>(parameters);
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.StaticCall(Method, Parameters);
        }
    }
}
