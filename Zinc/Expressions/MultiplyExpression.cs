﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public class MultiplyExpression : BinaryExpression
    {
        public MultiplyExpression(Expression left, Expression right)
            : base(left, right)
        {

        }

        public override void Visit(IExpressionVisitor visitor)
        {
            visitor.Multiply(Left, Right);
        }
    }
}
