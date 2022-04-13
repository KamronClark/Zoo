using Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    [Serializable]
    public class BuryAndEatBoneBehavior : IEatBehavior
    {
        public void Eat(IEater eater, Food food)
        {
            this.BuryBone(food);

            this.DigUpAndEatBone();

            // Increase animal's weight as a result of eating food.
            eater.Weight += food.Weight * (eater.WeightGainPercentage / 100);
            
            this.Bark();
        }

        private void Bark()
        {
            // Bark in satisfaction.
        }

        private void BuryBone(Food bone)
        {
            // Bury the bone.
        }

        private void DigUpAndEatBone()
        {
            // Dig up and eat the bone.
        }
    }
}
