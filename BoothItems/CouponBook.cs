using System;

namespace BoothItems
{
    /// <summary>
    /// The class which represents a coupon book.
    /// </summary>
    [Serializable]
    public class CouponBook : Item
    {
        /// <summary>
        /// Date the coupon book was issued.
        /// </summary>
        private DateTime dateMade;

        /// <summary>
        /// Date the coupon book expires.
        /// </summary>
        private DateTime dateExpired;

        /// <summary>
        /// Initializes a new instance of the CouponBook class.
        /// </summary>
        /// <param name="dateMade">Date the coupon book was issued.</param>
        /// <param name="dateExpired">Date the coupon book expires.</param>
        /// <param name="weight">Weight of the coupon book.</param>
        public CouponBook(DateTime dateMade, DateTime dateExpired, double weight)
            : base(weight)
        {
            this.dateMade = dateMade;
            this.dateExpired = dateExpired;
        }

        /// <summary>
        /// Gets the date the coupon book was issued.
        /// </summary>
        public DateTime DateMade
        {
            get
            {
                return this.dateMade;
            }
        }

        /// <summary>
        /// Gets the date the coupon book expires.
        /// </summary>
        public DateTime DateExpired
        {
            get
            {
                return this.dateExpired;
            }
        }
    }
}
