using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    /// <summary>
    /// The class which represents a money box.
    /// </summary>
    [Serializable]
    public class MoneyBox : MoneyCollector
    {
        /// <summary>
        /// Removes money.
        /// </summary>
        /// <param name="amount">Money to be removed.</param>
        /// <returns>Money removed.</returns>
        public override decimal RemoveMoney(decimal amount)
        {
            this.Unlock();
            decimal amountRemoved = base.RemoveMoney(amount);
            this.Lock();

            return amountRemoved;
        }

        /// <summary>
        /// Locks the money box.
        /// </summary>
        private void Lock()
        {
        }

        /// <summary>
        /// Unlocks the money box.
        /// </summary>
        private void Unlock()
        {
        }
    }
}
