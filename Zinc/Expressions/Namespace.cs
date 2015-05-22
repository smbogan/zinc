using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public class Namespace
    {
        public string Path { get; private set; }

        public Namespace(string path)
        {
            Path = path;
        }
    }
}
