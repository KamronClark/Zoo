using Reproducers;
using System;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a hummingbird.
    /// </summary>
    [Serializable]
    public class Hummingbird : Bird
    {
        /// <summary>
        /// Initializes a new instance of the Hummingbird class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">Gender of the animal.</param>
        public Hummingbird(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 17.5;
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Hover);
        }

        /// <summary>
        /// Gets the display size of the Hummingbird.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 0.4 : 0.6;
            }
        }
    }
}