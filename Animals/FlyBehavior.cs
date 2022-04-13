using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    [Serializable]
    public class FlyBehavior : IMoveBehavior
    {
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

            // Code for flying.
            if (animal.YDirection == VerticalDirection.Up)
            {
                animal.YPosition += 10;
                animal.YDirection = VerticalDirection.Down;
            }
            else
            {
                animal.YPosition -= 10;
                animal.YDirection = VerticalDirection.Up;
            }
        }
    }
}
