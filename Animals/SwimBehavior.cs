using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    [Serializable]
    public class SwimBehavior : IMoveBehavior
    {
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

            MoveHelper.MoveVertically(animal, animal.MoveDistance);
        }
    }
}
