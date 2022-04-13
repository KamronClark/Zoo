using System;

namespace BoothItems
{
    /// <summary>
    /// The class which represents a water bottle.
    /// </summary>
    [Serializable]
    public class WaterBottle : SoldItem
    {
        /// <summary>
        /// Serial number of the water bottle.
        /// </summary>
        private int serialNumber;

        /// <summary>
        /// Initializes a new instance of the WaterBottle class.
        /// </summary>
        /// <param name="price">Price of the water bottle.</param>
        /// <param name="serialNumber">Serial number of the water bottle.</param>
        /// <param name="weight">Weight of the water bottle.</param>
        public WaterBottle(decimal price, int serialNumber, double weight)
            : base(price, weight)
        {
            this.serialNumber = serialNumber;
        }

        /// <summary>
        /// Gets the serial number of the water bottle.
        /// </summary>
        public int SerialNumber
        {
            get
            {
                return this.serialNumber;
            }
        }
    }
}
