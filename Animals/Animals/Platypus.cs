using Foods;
using Reproducers;
using System;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a platypus.
    /// </summary>
    [Serializable]
    public sealed class Platypus : Mammal
    {
        /// <summary>
        /// Initializes a new instance of the Platypus class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">Gender of the animal.</param>
        public Platypus(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 12.0;
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Swim);

            this.EatBehavior = new ShowAffectionBehavior();

            this.BirthBehavior = new LayEggBehavior();
        }

        /// <summary>
        /// Gets the display size of the platypus.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                return this.Age == 0 ? 0.8 : 1.1;
            }
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public override void Eat(Food food)
        {
            this.StashInPouch(food);

            base.Eat(food);
        }

        /// <summary>
        /// Stashes food in its cheek pouches.
        /// </summary>
        /// <param name="food">The food to be stashed.</param>
        private void StashInPouch(Food food)
        {
            // Stash food to eat later.
        }
    }
}