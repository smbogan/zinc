using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public class Class
    {
        public Assembly Assembly { get; private set; }
        public Namespace Namespace { get; private set; }
        public string Name { get; private set; }

        public static readonly Class NullClass = new Class(new Assembly(""), new Namespace(""), "");
        
        public Class(Assembly assembly, Namespace ns, string name)
        {
            this.Assembly = assembly;
            this.Namespace = ns;
            this.Name = name;
        }
    }
}
