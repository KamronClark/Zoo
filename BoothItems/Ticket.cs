using System;

namespace BoothItems
{
    /// <summary>
    /// The class which represents a ticket.
    /// </summary>
    [Serializable]
    public class Ticket : SoldItem
    {
        /// <summary>
        /// Indicates if the ticket is redeemed.
        /// </summary>
        private bool isRedeemed;

        /// <summary>
        /// Serial number of the ticket.
        /// </summary>
        private int serialNumber;

        /// <summary>
        /// Initializes a new instance of the Ticket class.
        /// </summary>
        /// <param name="price">Price of the ticket.</param>
        /// <param name="serialNumber">Serial number of the ticket.</param>
        /// <param name="weight">Weight of the ticket.</param>
        public Ticket(decimal price, int serialNumber, double weight)
            : base(price, weight)
        {
            this.serialNumber = serialNumber;
        }

        /// <summary>
        /// Gets a value indicating whether or not the ticket is redeemed.
        /// </summary>
        public bool IsRedeemed
        {
            get
            {
                return this.isRedeemed;
            }
        }

        /// <summary>
        /// Gets the serial number of the ticket.
        /// </summary>
        public int SerialNumber
        {
            get
            {
                return this.serialNumber;
            }
        }

        /// <summary>
        /// Redeems the ticket.
        /// </summary>
        public void Redeem()
        {
            this.isRedeemed = true;
        }
    }
}
