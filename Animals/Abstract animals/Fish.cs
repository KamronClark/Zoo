using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which represents a fish.
    /// </summary>
    [Serializable]
    public abstract class Fish : Animal
    {
        /// <summary>
        /// Initializes a new instance of the Fish class.
        /// </summary>
        /// <param name="name">Name of the Fish.</param>
        /// <param name="age">Age of the Fish.</param>
        /// <param name="weight">Weight of the Fish.</param>
        /// <param name="gender">Gender of the animal.</param>
        public Fish(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Swim);

            this.EatBehavior = new ConsumeBehavior();

            this.BirthBehavior = new LayEggBehavior();
        }

        /// <summary>
        /// Gets the percentage of weight gained from eating.
        /// </summary>
        public override double WeightGainPercentage
        {
            get
            {
                return 5.0;
            }
        }
    }
}
