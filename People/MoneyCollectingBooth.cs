using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;
using BoothItems;
using MoneyCollectors;
using Reproducers;

namespace People
{
    /// <summary>
    /// The class which represents a booth that sells items.
    /// </summary>
    [Serializable]
    public class MoneyCollectingBooth : Booth
    {
        /// <summary>
        /// The price of a ticket.
        /// </summary>
        private decimal ticketPrice;

        /// <summary>
        /// Price of the booth's water bottles.
        /// </summary>
        private decimal waterBottlePrice;

        /// <summary>
        /// The money box in the booth. (presumably a cash register).
        /// </summary>
        private IMoneyCollector moneyBox;

        private Stack<Ticket> ticketStack;

        /// <summary>
        /// Initializes a new instance of the MoneyCollectingBooth class.
        /// </summary>
        /// <param name="attendant">Booth attendant.</param>
        /// <param name="ticketPrice">Ticket price.</param>
        /// <param name="waterBottlePrice">Water bottle price.</param>
        /// <param name="moneyBox">The booth's box of money.</param>
        public MoneyCollectingBooth(Employee attendant, decimal ticketPrice, decimal waterBottlePrice, IMoneyCollector moneyBox)
            : base(attendant)
        {
            this.ticketPrice = ticketPrice;
            this.waterBottlePrice = waterBottlePrice;
            this.moneyBox = moneyBox;
            this.ticketStack = new Stack<Ticket>();

            for (int t = 0; t < 5; t++)
            {
                Ticket ticket = new Ticket(this.TicketPrice, t, 0.01);
                this.ticketStack.Push(ticket);
            }

            for (int w = 0; w < 5; w++)
            {
                WaterBottle waterBottle = new WaterBottle(this.WaterBottlePrice, w, 1);
                this.Items.Add(waterBottle);
            }
        }

        /// <summary>
        /// Gets the ticket price.
        /// </summary>
        public decimal TicketPrice
        {
            get
            {
                return this.ticketPrice;
            }
        }

        /// <summary>
        /// Gets the water bottle price.
        /// </summary>
        public decimal WaterBottlePrice
        {
            get
            {
                return this.waterBottlePrice;
            }
        }

        /// <summary>
        /// Gets the balance of the booth's money box.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBox.MoneyBalance;
            }
        }

        /// <summary>
        /// Adds money to the booth.
        /// </summary>
        /// <param name="amount">Amount to be added.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBox.AddMoney(amount);
        }

        /// <summary>
        /// Removes money from the booth.
        /// </summary>
        /// <param name="amount">Amount to be removed.</param>
        /// <returns>Returns amount to be removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.moneyBox.RemoveMoney(amount);
        }

        /// <summary>
        /// Sells a ticket to the guest.
        /// </summary>
        /// <param name="payment">Guest's payment.</param>
        /// <returns>Returns a ticket.</returns>
        public Ticket SellTicket(decimal payment)
        {
            Ticket ticket = null;

            try
            {
                if (payment >= this.TicketPrice)
                {
                    ticket = this.ticketStack.Pop();

                    if (ticket != null)
                    {
                        this.AddMoney(payment);
                    }
                }
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Ticket not found.", ex);
            }
            
            return ticket;
        }

        /// <summary>
        /// Sells a water bottle to the guest.
        /// </summary>
        /// <param name="payment">Guest's payment.</param>
        /// <returns>Returns a water bottle.</returns>
        public WaterBottle SellWaterBottle(decimal payment)
        {
            WaterBottle waterBottle = null;

            try
            {
                if (payment >= this.waterBottlePrice)
                {
                    waterBottle = this.Attendant.FindItem(this.Items, typeof(WaterBottle)) as WaterBottle;

                    if (waterBottle != null)
                    {
                        this.AddMoney(payment);
                    }
                }
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Water bottle not found.", ex);
            }

            return waterBottle;
        }
    }
}
