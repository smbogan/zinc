using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zinc.Expressions;

namespace ZincTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression e = new AddExpression(new LocalVariable("a"), new LocalVariable("b"));
            Expression a = new AssignmentExpression(new LocalVariable("c"), e);
        }
    }
}
