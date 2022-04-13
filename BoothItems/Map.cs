using System;

namespace BoothItems
{
    /// <summary>
    /// The class which represents a map.
    /// </summary>
    [Serializable]
    public class Map : Item
    {
        /// <summary>
        /// Date the map was issued.
        /// </summary>
        private DateTime dateIssued;

        /// <summary>
        /// Initializes a new instance of the Map class.
        /// </summary>
        /// <param name="weight">Date the map was issued.</param>
        /// <param name="dateIssued">Weight of the map.</param>
        public Map(double weight, DateTime dateIssued)
            : base(weight)
        {
            this.dateIssued = dateIssued;
        }

        /// <summary>
        /// Gets the date the map was issued on.
        /// </summary>
        public DateTime DateIssued
        {
            get
            {
                return this.dateIssued;
            }
        }
    }
}
