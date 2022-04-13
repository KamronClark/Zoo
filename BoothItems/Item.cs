using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// The class which represents an item.
    /// </summary>
    [Serializable]
    public abstract class Item
    {
        /// <summary>
        /// Weight of the item.
        /// </summary>
        private double weight;

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="weight">Weight of the item.</param>
        public Item(double weight)
        {
            this.weight = weight;
        }

        /// <summary>
        /// Gets the weight of the item.
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }
        }
    }
}
