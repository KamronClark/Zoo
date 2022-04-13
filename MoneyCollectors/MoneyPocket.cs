using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    /// <summary>
    /// The class which represents a money pocket.
    /// </summary>
    [Serializable]
    public class MoneyPocket : MoneyCollector
    {               
        /// <summary>
        /// Removes money.
        /// </summary>
        /// <param name="amount">Money to be removed.</param>
        /// <returns>Money removed.</returns>
        public override decimal RemoveMoney(decimal amount)
        {
            this.Unfold();
            decimal amountRemoved = base.RemoveMoney(amount);
            this.Fold();

            return amountRemoved;
        }

        /// <summary>
        /// Folds the money pocket.
        /// </summary>
        private void Fold()
        {
        }

        /// <summary>
        /// Unfolds the money pocket.
        /// </summary>
        private void Unfold()
        {
        }
    }
}
