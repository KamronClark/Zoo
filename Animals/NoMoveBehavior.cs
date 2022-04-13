using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    [Serializable]
    public class NoMoveBehavior : IMoveBehavior
    {
        public void Move(Animal animal)
        {
            // Animal stands completely still.
        }
    }
}
