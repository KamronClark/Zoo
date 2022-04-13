using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyCollectors;

namespace Accounts
{
    /// <summary>
    /// The class which represents an account.
    /// </summary>
    [Serializable]
    public class Account : IMoneyCollector
    {
        /// <summary>
        /// Money balance of the account.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets the account's money balance.
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
        /// Adds money to the account.
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
        /// Removes money from the account.
        /// </summary>
        /// <param name="amount">Money to be removed.</param>
        /// <returns>Returns amount removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            this.moneyBalance -= amount;

            if (this.OnBalanceChange != null)
            {
                this.OnBalanceChange();
            }

            return amount;
        }
    }
}
