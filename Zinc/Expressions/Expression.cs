using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public abstract class Expression
    {
        public Expression()
        {

        }

        public abstract void Visit(IExpressionVisitor visitor);
    }
}
