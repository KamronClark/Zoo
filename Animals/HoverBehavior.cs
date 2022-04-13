using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    [Serializable]
    public class HoverBehavior : IMoveBehavior
    {
        private static Random random = new Random(DateTime.Now.Millisecond);

        private int stepCount;

        private HoverProcess process;

        public void Move(Animal animal)
        {
            // if there are no more steps to take (step count is at 0), switch to the next process
            if (this.stepCount == 0)
            {
                this.NextProcess(animal);
            }

            // decrement the step count
            this.stepCount--;

            // define a move distance variable
            int moveDistance;

            // if the current process is hovering
            if (this.process == HoverProcess.Hovering)
            {
                // the animal moves at a normal pace, so set the move distance variable to the animal's move distance
                moveDistance = animal.MoveDistance;

                // the animal moves randomly on each step, so give the animal a random horizontal and vertical direction
                animal.XDirection = random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
                animal.YDirection = random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;
            }
            else     // otherwise
            {
                // the animal moves at a quadruple pace, so set the move distance variable to 4 times the animal's move distance
                moveDistance = animal.MoveDistance * 4;
            }

            // move horizontally and vertically using the move distance variable
            MoveHelper.MoveHorizontally(animal, moveDistance);
            MoveHelper.MoveVertically(animal, moveDistance);
        }

        private void NextProcess(Animal animal)
        {
            // if the current process is hovering
            if (this.process == HoverProcess.Hovering)
            {
                // switch to zooming
                this.process = HoverProcess.Zooming;

                // set the step count to a random number between 5 and 8, inclusive
                this.stepCount = random.Next(5, 9);

                // set the animal's horizontal and vertical directions to a random direction
                animal.XDirection = random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
                animal.YDirection = random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;
            }
            else    // otherwise
            {
                // switch to hovering
                this.process = HoverProcess.Hovering;

                // set the step count to a random number between 7 and 10, inclusive
                this.stepCount = random.Next(7, 11);
            }
        }
    }
}
