using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The class which represents an item which is sold.
    /// </summary>
    [Serializable]
    public abstract class SoldItem : Item
    {
        /// <summary>
        /// Price of the item.
        /// </summary>
        private decimal price;

        /// <summary>
        /// Initializes a new instance of the SoldItem class.
        /// </summary>
        /// <param name="price">Price of the item.</param>
        /// <param name="weight">Weight of the item.</param>
        public SoldItem(decimal price, double weight)
            : base(weight)
        {
            this.price = price;
        }

        /// <summary>
        /// Gets the price of the item.
        /// </summary>
        public decimal Price
        {
            get
            {
                return this.price;
            }
        }
    }
}
