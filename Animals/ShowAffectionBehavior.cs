using Foods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    [Serializable]
    public class ShowAffectionBehavior : IEatBehavior
    {
        public void Eat(IEater eater, Food food)
        {
            // Increase animal's weight as a result of eating food.
            eater.Weight += food.Weight * (eater.WeightGainPercentage / 100);

            this.ShowAffection();
        }

        private void ShowAffection()
        {
            // Give a hug.
        }
    }
}
