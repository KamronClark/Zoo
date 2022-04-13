using Reproducers;
using System;

namespace Animals
{
    /// <summary>
    /// The class which represents a kangaroo.
    /// </summary>
    [Serializable]
    public class Kangaroo : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the Kangaroo class.
        /// </summary>
        /// <param name="name">Name of the Kangaroo.</param>
        /// <param name="age">Age of the Kangaroo.</param>
        /// <param name="weight">Weight of the Kangaroo.</param>
        /// <param name="gender">Gender of the animal.</param>
        public Kangaroo(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 13;
        }
    }
}
