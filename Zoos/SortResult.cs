using Animals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoos
{
    [Serializable]
    public class SortResult
    {
        public List<object> Objects { get; set; }

        public int CompareCount { get; set; }

        public int SwapCount { get; set; }

        public double ElapsedMilliseconds { get; set; }
    }
}
