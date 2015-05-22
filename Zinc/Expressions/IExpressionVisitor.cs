using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public interface IExpressionVisitor
    {
        void Add(Expression left, Expression right);

        void Subtract(Expression Left, Expression Right);

        void LocalVariable(string Name);

        void Assignment(AssignableExpression Left, Expression Right);

        void Call(Method Method, Utility.ReadOnlyList<Expression> Parameters);

        void Multiply(Expression Left, Expression Right);

        void Divide(Expression Left, Expression Right);

        void Mod(Expression Left, Expression Right);
    }
}
