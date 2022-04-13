using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCollectors;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// The class which represents a chimpanzee.
    /// </summary>
    [Serializable]
    public class Chimpanzee : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the Chimpanzee class.
        /// </summary>
        /// <param name="name">Name of the Chimpanzee.</param>
        /// <param name="age">Age of the Chimpanzee.</param>
        /// <param name="weight">Weight of the Chimpanzee.</param>
        /// <param name="gender">Gender of the animal.</param>
        public Chimpanzee(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 10;
        }

        /// <summary>
        /// Gets the money balance of the chimp.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return 0m;
            }
        }

        /// <summary>
        /// Adds money.
        /// </summary>
        /// <param name="amount">Amount to be added.</param>
        public void AddMoney(decimal amount)
        {
            // Buy some bananas.
        }

        /// <summary>
        /// Removes money.
        /// </summary>
        /// <param name="amount">Amount to be removed.</param>
        /// <returns>Amount removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return 0m;
        }
    }
}
