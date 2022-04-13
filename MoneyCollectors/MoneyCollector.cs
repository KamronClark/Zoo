using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    /// <summary>
    /// The class which represents something that can collect money.
    /// </summary>
    [Serializable]
    public abstract class MoneyCollector : IMoneyCollector
    {
        /// <summary>
        /// The money collector's money balance.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets the money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBalance;
            }
        }

        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// Adds money to the money balance.
        /// </summary>
        /// <param name="amount">Amount to be added.</param>
        public void AddMoney(decimal amount)
        {           
            this.moneyBalance += amount;

            if (this.OnBalanceChange != null)
            {
                this.OnBalanceChange();
            }
        }

        /// <summary>
        /// Removes money from the money balance.
        /// </summary>
        /// <param name="amount">Amount to be removed.</param>
        /// <returns>Returns the amount removed.</returns>
        public virtual decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved;

            // If there is enough money in the wallet...
            if (this.moneyBalance >= amount)
            {
                // Return the requested amount.
                amountRemoved = amount;
            }
            else
            {
                // Otherwise return all the money that is left.
                amountRemoved = this.moneyBalance;
            }

            // Subtract the amount removed from the wallet's money balance.
            this.moneyBalance -= amountRemoved;

            if (this.OnBalanceChange != null)
            {
                this.OnBalanceChange();
            }

            return amountRemoved;
        }
    }
}
