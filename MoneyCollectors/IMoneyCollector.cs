using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyCollectors
{
    /// <summary>
    /// The interface which represents something that can collect money.
    /// </summary>
    public interface IMoneyCollector
    {
        /// <summary>
        /// Gets the money balance.
        /// </summary>
        decimal MoneyBalance { get; }

        Action OnBalanceChange { get; set; }

        /// <summary>
        /// Adds money.
        /// </summary>
        /// <param name="amount">Money to be added.</param>
        void AddMoney(decimal amount);

        /// <summary>
        /// Removes money.
        /// </summary>
        /// <param name="amount">Money to be removed.</param>
        /// <returns>Money removed.</returns>
        decimal RemoveMoney(decimal amount);
    }
}
