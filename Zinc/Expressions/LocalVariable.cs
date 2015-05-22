using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public class LocalVariable : AssignableExpression
    {
        public string Name { get; private set; }

        public LocalVariable(string name)
        {
            Name = name;
        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.LocalVariable(Name);
        }
    }
}
