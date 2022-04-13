using Reproducers;
using System;

namespace Animals
{
    /// <summary>
    /// The class which represents a squirrel.
    /// </summary>
    [Serializable]
    public class Squirrel : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the Squirrel class.
        /// </summary>
        /// <param name="name">Name of the Squirrel.</param>
        /// <param name="age">Age of the Squirrel.</param>
        /// <param name="weight">Weight of the Squirrel.</param>
        /// <param name="gender">Gender of the animal.</param>
        public Squirrel(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 17;
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Climb);
        }
    }
}
