using System;
using System.Collections.Generic;
using People;
using Reproducers;

namespace BirthingRooms
{
    /// <summary>
    /// The class which is used to represent a birthing room.
    /// </summary>
    [Serializable]
    public class BirthingRoom
    {       
        /// <summary>
        /// The minimum allowable temperature of the birthing room.
        /// </summary>
        public static readonly double MinTemperature = 35.0;

        /// <summary>
        /// The maximum allowable temperature of the birthing room.
        /// </summary>
        public static readonly double MaxTemperature = 95.0;

        /// <summary>
        /// The initial temperature of the birthing room.
        /// </summary>
        private readonly double initialTemperature = 77.0;

        /// <summary>
        /// The current temperature of the birthing room.
        /// </summary>
        private double temperature;

        /// <summary>
        /// The employee currently assigned to be the vet of the birthing room.
        /// </summary>
        private Employee vet;

        /// <summary>
        /// Initializes a new instance of the BirthingRoom class.
        /// </summary>
        /// <param name="vet">The employee to be the birthing room's vet.</param>
        public BirthingRoom(Employee vet)
        {
            this.Temperature = this.initialTemperature;
            this.vet = vet;
            this.PregnantAnimals = new Queue<IReproducer>();
        }

        /// <summary>
        /// Gets or sets the birthing room's temperature.
        /// </summary>
        public double Temperature
        {
            get
            {
                return this.temperature;
            }

            set
            {
                double previousTemp = this.temperature;
                // If the value is in range...
                if (value < MinTemperature)
                {
                    throw new ArgumentOutOfRangeException("temperature", "The temperature must be above 35 degrees.");
                }
                else if (value > MaxTemperature)
                {
                    throw new ArgumentOutOfRangeException("temperature", "The temperature must be above 95 degrees.");
                }                
                else if (value >= MinTemperature && value <= MaxTemperature)
                {
                    this.temperature = value;

                    if (this.OnTemperatureChange != null)
                    {
                        this.OnTemperatureChange(previousTemp, this.temperature);
                    }
                }
                else
                {
                    throw new Exception("Please enter a valid temperature.");
                }
            }
        }

        public Action<double, double> OnTemperatureChange { get; set; }

        public Queue<IReproducer> PregnantAnimals { get; private set; }

        /// <summary>
        /// Births a reproducer.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer BirthAnimal()
        {
            IReproducer baby = null;

            baby = this.vet.DeliverAnimal(this.PregnantAnimals.Dequeue());

            // Increase the temperature due to the heat generated from birthing.
            this.Temperature += 0.5;

            return baby;
        }
    }
}