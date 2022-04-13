using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class ListUtil
    {
        public static string Flatten(this IEnumerable<string> list, string separator)
        {
            string result = null;

            list.ToList().ForEach(s => result += result == null ? s : separator + s);

            return result;
        }
    }
}
