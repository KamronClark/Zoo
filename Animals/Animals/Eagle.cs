using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which represents an eagle.
    /// </summary>
    [Serializable]
    public class Eagle : Bird
    {
        /// <summary>
        /// Initializes a new instance of the Eagle class.
        /// </summary>
        /// <param name="name">Name of the Eagle.</param>
        /// <param name="age">Age of the Eagle.</param>
        /// <param name="weight">Weight of the Eagle.</param>
        /// <param name="gender">Gender of the animal.</param>
        public Eagle(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 25;
        }
    }
}
