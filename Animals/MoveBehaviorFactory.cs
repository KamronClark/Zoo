using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public static class MoveBehaviorFactory
    {
        public static IMoveBehavior CreateMoveBehavior(MoveBehaviorType type)
        {
            IMoveBehavior behavior = null;

            switch (type)
            {
                case MoveBehaviorType.Fly:
                    behavior = new FlyBehavior();
                    break;
                case MoveBehaviorType.Pace:
                    behavior = new PaceBehavior();
                    break;
                case MoveBehaviorType.Swim:
                    behavior = new SwimBehavior();
                    break;
                case MoveBehaviorType.NoMove:
                    behavior = new NoMoveBehavior();
                    break;
                case MoveBehaviorType.Hover:
                    behavior = new HoverBehavior();
                    break;

                case MoveBehaviorType.Climb:
                    behavior = new ClimbBehavior();
                    break;

                default:
                    // I'm pretty sure this can't ever be reached, since we're using an enum for this. Unless more behaviors are added and this isn't updated for some reason.
                    break;
            }

            return behavior;
        }
    }
}
