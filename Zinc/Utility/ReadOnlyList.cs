using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zinc.Utility
{
    public class ReadOnlyList<T>
    {
        List<T> items;

        public T this[int index]
        {
            get
            {
                return items[index];
            }
        }

        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        public ReadOnlyList(IEnumerable<T> list)
        {
            items = new List<T>(list);
        }
    }
}
