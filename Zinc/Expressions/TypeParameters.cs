using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zinc.Utility;

namespace Zinc.Expressions
{
    public class TypeParameters
    {
        public ReadOnlyList<Class> Types { get; private set; }

        public TypeParameters(params Class[] types)
        {
            Types = new ReadOnlyList<Class>(types);
        }
    }
}
