using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zinc.Utility;

namespace Zinc.Expressions
{
    public class Call : Expression
    {
        public Method Method { get; private set; }
        public ReadOnlyList<Expression> Parameters { get; private set; }

        public Call(Method method, params Expression[] parameters)
        {
            this.Method = method;
            this.Parameters = new ReadOnlyList<Expression>(parameters);
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Call(Method, Parameters);
        }
    }
}
