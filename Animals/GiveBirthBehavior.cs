using Foods;
using Reproducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    [Serializable]
    public class GiveBirthBehavior : IBirthBehavior
    {
        /// <summary>
        /// The random used to randomly set a gender to male or female.
        /// [11 (E)] ...Use a ternary operator and the animal's random to set the gender randomly to either male or female.
        /// </summary>
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// Has an animal reproduce.
        /// [11 (C)] Reproduce behavior implementation > inherited method
        /// </summary>
        /// <param name="mother">The pregnant mother animal.</param>
        /// <returns>The baby animal.</returns>
        public IReproducer Reproduce(Animal mother)
        {
            // [11 (C)] Reproduce behavior implementation > Move code from Animal.Reproduce and modify as necessary

            // Define and initialize a result variable > changes the Type of baby from the original Animal to IProducer
            IReproducer baby = null;

            // Set the baby animal weight based upon its type.
            double weight = mother.Weight * (mother.BabyWeightPercentage / 100); // this was pulled from the inline calculation on the CreateInstance method call

            // [11 (E)] pass in a gender in the CreateInstance method as the last parameter. Use a ternary operator and the animal's random to set the gender randomly to either male or female.
            // Create a baby animal of the same type as the mother.
            Gender gender = GiveBirthBehavior.random.Next(0, 2) == 0 ? Gender.Female : Gender.Male;
            baby = (IReproducer)Activator.CreateInstance(mother.GetType(), "Baby", 0, weight, gender);

            // Reduce the mother's weight by 1.5 times the baby's weight.
            mother.Weight -= weight * 1.5;

            // [11 (C)] Reproduce behavior implementation > implement the code from Mammal
            if (mother is Mammal && mother is IEater)
            {
                this.FeedNewborn(mother as Mammal, baby as Mammal);
            }

            // Return result.
            return baby;
        }

        /// <summary>
        /// Feeds the newborn baby.
        /// [11 (B.C)] Move each behavior's associated methods - (FeedNewborn) for giving birth, LayEgg and HatchEgg for laying eggs - into the appropriate behavior class. > from Mammal.Reproduce().
        /// </summary>
        /// <param name="mother">The newborn's mother.</param>
        /// <param name="baby">The newborn baby animal.</param>
        private void FeedNewborn(Mammal mother, Mammal baby)
        {
            // Determine milk weight.
            double milkWeight = mother.Weight * 0.005;

            // Generate milk.
            Food milk = new Food(milkWeight);

            // Feed baby.
            baby.Eat(milk);

            // Reduce parent's weight.
            mother.Weight -= milkWeight;
        }
    }
}
