using Reproducers;
using System;

namespace Animals
{
    /// <summary>
    /// The class which represents a shark.
    /// </summary>
    [Serializable]
    public class Shark : Fish
    {
        /// <summary>
        /// Initializes a new instance of the Shark class.
        /// </summary>
        /// <param name="name">Name of the Shark.</param>
        /// <param name="age">Age of the Shark.</param>
        /// <param name="weight">Weight of the Shark.</param>
        /// <param name="gender">Gender of the animal.</param>
        public Shark(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 18;
        }

        /// <summary>
        /// Gets the display size of the shark.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 1 : 1.5;
            }
        }
    }
}
