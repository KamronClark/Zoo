using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    [Serializable]
    public class ClimbBehavior : IMoveBehavior
    {
        private static Random random = new Random(DateTime.Now.Millisecond);

        private int maxHeight;

        private ClimbProcess process;

        public void Move(Animal animal)
        {
            switch (this.process)
            {
                // if the current process is climbing
                case ClimbProcess.Climbing:

                    // ensure animal is moving up > // If the animal is climbing, ensure the vertical direction is set to up
                    animal.YDirection = VerticalDirection.Up;

                    // move vertically > // Move the animal vertically
                    MoveHelper.MoveVertically(animal, animal.MoveDistance);

                    // if the animal's next step will take it above the max height > // If the animal has hit or will hit the top of the cage
                    if (animal.YPosition - animal.MoveDistance <= this.maxHeight)
                    {
                        // make animal move down > // Change the animal's vertical direction to down
                        animal.YDirection = VerticalDirection.Down;

                        // switch its horizontal direction (if moving right, make it move left and vice versa) > // Switch the way the animal is facing
                        animal.XDirection = animal.XDirection == HorizontalDirection.Left ? HorizontalDirection.Right : HorizontalDirection.Left;

                        // switch to next process > // And switch the animal to the next process
                        this.NextProcess(animal);
                    }

                    break;
                // if current process is falling
                case ClimbProcess.Falling:
                    // move horizontally > // Otherwise if the animal is floating, have it move diagonally
                    MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

                    // move vertically at twice the distance
                    MoveHelper.MoveVertically(animal, animal.MoveDistance * 2);

                    // if the animal's next move will take it past the bottom, switch to the next process > // If the animal has hit or will hit the bottom of the cage
                    if (animal.YPosition + animal.MoveDistance >= animal.YPositionMax)
                    {
                        // Switch to the next process
                        this.NextProcess(animal);
                    }

                    break;
                // if the current process is scurrying
                case ClimbProcess.Scurrying:
                    // move horizontally > // Move the animal horizontally
                    MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

                    // if the animal will hit a vertical wall (either right or left), set the animal to the edge and switch to the next process > // If the animal has hit or will hit a vertical wall
                    if (animal.XPosition - animal.MoveDistance <= 0 || animal.XPosition + animal.MoveDistance >= animal.XPositionMax)
                    {
                        // Move the animal to the appropriate edge of the cage
                        if (animal.XPosition + animal.MoveDistance >= animal.XPositionMax)
                        {
                            animal.XPosition = animal.XPositionMax;
                        }
                        else
                        {
                            animal.XPosition = 0;
                        }

                        // Switch to the next process
                        this.NextProcess(animal);
                    }

                    break;
            }
        }

        private void NextProcess(Animal animal)
        {
            // [6 (E)] Use the following code and code comments to write the NextProcess method:

            switch (this.process)
            {
                // if the current process is climbing, switch to falling
                case ClimbProcess.Climbing:
                    this.process = ClimbProcess.Falling;

                    break;
                // if the current process is falling, switch to scurrying
                case ClimbProcess.Falling:
                    this.process = ClimbProcess.Scurrying;

                    break;
                // if the current process is scurrying
                case ClimbProcess.Scurrying:
                    // Set the maximum height to a random value between 15 and 85 percent of the height of the animal's maximum y position > code given
                    int lowerMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.15));
                    int higherMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.85));

                    // set max height to a random value between the lowest max and the highest max
                    this.maxHeight = ClimbBehavior.random.Next(lowerMax, higherMax + 1);

                    // switch to climbing
                    this.process = ClimbProcess.Climbing;

                    break;
            }
        }
    }
}
