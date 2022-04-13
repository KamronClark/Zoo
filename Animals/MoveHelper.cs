using CagedItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    public static class MoveHelper
    {
        public static void MoveHorizontally(Animal animal, int moveDistance)
        {
            switch(animal.HungerState)
            {
                case HungerState.Hungry:
                    moveDistance = moveDistance / 4;
                    break;

                case HungerState.Starving:
                    moveDistance = 0;
                    break;

                case HungerState.Unconscious:
                    moveDistance = 0;
                    break;
            }

            // If the animal is moving to the right...
            if (animal.XDirection == HorizontalDirection.Right)
            {
                // Check the distance, if it's more than the max...
                if (animal.XPosition + moveDistance > animal.XPositionMax)
                {
                    // Move the animal to the maximum distance.
                    animal.XPosition = animal.XPositionMax;

                    // The animal can't go any further to the right, turn around.
                    animal.XDirection = HorizontalDirection.Left;
                }
                else
                {
                    // Move the animal to specified distance.
                    animal.XPosition += moveDistance;
                }
            }
            else
            {
                if (animal.XPosition - moveDistance < 0)
                {
                    animal.XPosition = 0;
                    animal.XDirection = HorizontalDirection.Right;
                }
                else
                {
                    animal.XPosition -= moveDistance;
                }
            }
        }

        public static void MoveVertically(Animal animal, int moveDistance)
        {
            switch (animal.HungerState)
            {
                case HungerState.Hungry:
                    moveDistance = moveDistance / 4;
                    break;

                case HungerState.Starving:
                    moveDistance = 0;
                    break;

                case HungerState.Unconscious:
                    moveDistance = 0;
                    break;
            }

            // If the animal is swimming up.
            if (animal.YDirection == VerticalDirection.Up)
            {
                // Check the distance
                if (animal.YPosition + moveDistance > animal.YPositionMax)
                {
                    // If the distance traveled is greater than max, move the animal to the max.
                    animal.YPosition = animal.YPositionMax;

                    // The animal can't go any further up, so turn around.
                    animal.YDirection = VerticalDirection.Down;
                }
                else
                {
                    // Move the animal to specified distance.
                    animal.YPosition += moveDistance;
                }
            }
            else
            {
                if (animal.YPosition - moveDistance < 0)
                {
                    animal.YPosition = 0;
                    animal.YDirection = VerticalDirection.Up;
                }
                else
                {
                    animal.YPosition -= moveDistance;
                }
            }
        }
    }
}
