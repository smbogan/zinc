using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Expressions
{
    public class Assembly
    {
        public string Path { get; private set; }

        public Assembly(string path)
        {
            Path = path;
        }
    }
}
