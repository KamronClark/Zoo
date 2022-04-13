using Reproducers;
using System;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which represents an ostrich.
    /// </summary>
    [Serializable]
    public sealed class Ostrich : Bird
    {
        /// <summary>
        /// Initializes a new instance of the Ostrich class.
        /// </summary>
        /// <param name="name">Name of the Ostrich.</param>
        /// <param name="age">Age of the Ostrich.</param>
        /// <param name="weight">Weight of the Ostrich.</param>
        /// <param name="gender">Gender of the animal.</param>
        public Ostrich(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 30;
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Pace);
        }

        /// <summary>
        /// Gets the display size of the Ostrich.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 0.4 : 0.8;
            }
        }
    }
}
