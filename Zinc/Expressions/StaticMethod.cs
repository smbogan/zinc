using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public class StaticMethod
    {
        public string Name { get; private set; }
        public Class Class { get; private set; }
        public TypeParameters Parameters { get; private set; }
        public Class ReturnType { get; private set; }

        public StaticMethod(Class klass, string name, TypeParameters parameterTypes, Class returnType)
        {
            this.Name = name;
            this.Class = klass;
            this.Parameters = parameterTypes;
            this.ReturnType = returnType;
        }
    }
}
