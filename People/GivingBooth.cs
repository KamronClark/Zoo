using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoothItems;

namespace People
{
    /// <summary>
    /// The class which represents a booth that gives free items.
    /// </summary>
    [Serializable]
    public class GivingBooth : Booth
    {
        /// <summary>
        /// Initializes a new instance of the GivingBooth class.
        /// </summary>
        /// <param name="attendant">Booth attendant.</param>
        public GivingBooth(Employee attendant)
            : base(attendant)
        {
            for (int c = 0; c < 5; c++)
            {
                CouponBook couponBook = new CouponBook(DateTime.Now, DateTime.Now.AddYears(1), 0.8);
                this.Items.Add(couponBook);
            }

            for (int m = 0; m < 10; m++)
            {
                Map map = new Map(.5, DateTime.Now);
                this.Items.Add(map);
            }
        }

        /// <summary>
        /// Gives a guest a free coupon book.
        /// </summary>
        /// <returns>Returns a coupon book.</returns>
        public CouponBook GiveFreeCouponBook()
        {
            try
            {
                return this.Attendant.FindItem(this.Items, typeof(CouponBook)) as CouponBook;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Coupon book not found.", ex);
            }
        }

        /// <summary>
        /// Gives a guest a free map.
        /// </summary>
        /// <returns>Returns a map.</returns>
        public Map GiveFreeMap()
        {
            try
            {
                return this.Attendant.FindItem(this.Items, typeof(Map)) as Map;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Ticket not found.", ex);
            }
        }
    }
}
