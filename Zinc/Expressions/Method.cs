using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public class Method
    {
        public Class Class { get; private set; }
        public TypeParameters Parameters { get; private set; }
        public Class ReturnType { get; private set; }

        public Method(Class klass, TypeParameters parameterTypes, Class returnType)
        {
            this.Class = klass;
            this.Parameters = parameterTypes;
            this.ReturnType = returnType;
        }
    }
}
