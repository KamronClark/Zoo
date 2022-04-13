using MoneyCollectors;
using System;

namespace People
{
    /// <summary>
    /// The class which is used to represent a wallet.
    /// </summary>
    [Serializable]
    public class Wallet : IMoneyCollector
    {
        /// <summary>
        /// Color of the wallet.
        /// </summary>
        private WalletColor color;
        
        /// <summary>
        /// The wallet's money pocket.
        /// </summary>
        private IMoneyCollector moneyPocket;

        /// <summary>
        /// Initializes a new instance of the Wallet class.
        /// </summary>
        /// <param name="color">The color of the wallet.</param>
        public Wallet(WalletColor color)
        {
            this.color = color;
            this.moneyPocket = new MoneyPocket();

            this.moneyPocket.OnBalanceChange = () =>
            {
                if (this.OnBalanceChange != null)
                {
                    this.OnBalanceChange();
                }                
            };
        }

        /// <summary>
        /// Gets The wallet's money balance.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyPocket.MoneyBalance;
            }
        }

        /// <summary>
        /// Gets or sets the wallet color.
        /// </summary>
        public WalletColor WalletColor
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
            }
        }

        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// Adds money to the money balance.
        /// </summary>
        /// <param name="amount">Amount to be added.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyPocket.AddMoney(amount);      
        }

        /// <summary>
        /// Removes money from the money balance.
        /// </summary>
        /// <param name="amount">Amount to be removed.</param>
        /// <returns>Returns the amount removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved;

            // If there is enough money in the wallet...
            if (this.moneyPocket.MoneyBalance >= amount)
            {
                // Return the requested amount.
                amountRemoved = amount;
            }
            else
            {
                // Otherwise return all the money that is left.
                amountRemoved = this.moneyPocket.MoneyBalance;
            }

            // Subtract the amount removed from the wallet's money balance.
            this.moneyPocket.RemoveMoney(amountRemoved);

            return amountRemoved;
        }
    }
}