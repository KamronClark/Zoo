using System;
using Foods;
using MoneyCollectors;

namespace VendingMachines
{
    /// <summary>
    /// The class which is used to represent a vending machine.
    /// </summary>
    [Serializable]
    public class VendingMachine
    {
        /// <summary>
        /// The size of a bag of food used to refill the vending machine.
        /// </summary>
        private readonly double bagSize = 65.0;

        /// <summary>
        /// The amount of food that the vending machine can hold (in pounds).
        /// </summary>
        private readonly double maxFoodStock = 250.0;

        /// <summary>
        /// The price of food (per pound).
        /// </summary>
        private decimal foodPricePerPound;

        /// <summary>
        /// The amount of food currently in stock (in pounds).
        /// </summary>
        private double foodStock;

        /// <summary>
        /// The machine's money box.
        /// </summary>
        private IMoneyCollector moneyBox;

        /// <summary>
        /// Initializes a new instance of the VendingMachine class.
        /// </summary>
        /// <param name="foodPrice">The price of food (per pound).</param>
        /// <param name="moneyBox">The box of money.</param>
        public VendingMachine(decimal foodPrice, IMoneyCollector moneyBox)
        {
            this.foodPricePerPound = foodPrice;
            this.moneyBox = moneyBox;

            // Fill with an initial load of food.
            while (!this.IsFull())
            {
                this.AddFoodBag();
            }
        }

        /// <summary>
        /// Gets the money balance of the machine's money box.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBox.MoneyBalance;
            }
        }

        /// <summary>
        /// Adds money to the money balance.
        /// </summary>
        /// <param name="amount">Amount to be added.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBox.AddMoney(amount);
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
            if (this.moneyBox.MoneyBalance >= amount)
            {
                // Return the requested amount.
                amountRemoved = amount;
            }
            else
            {
                // Otherwise return all the money that is left.
                amountRemoved = this.moneyBox.MoneyBalance;
            }

            // Subtract the amount removed from the wallet's money balance.
            this.moneyBox.RemoveMoney(amountRemoved);

            return amountRemoved;
        }

        /// <summary>
        /// Buys food from the vending machine.
        /// </summary>
        /// <param name="payment">The payment for the food.</param>
        /// <returns>The purchased food.</returns>
        public Food BuyFood(decimal payment)
        {
            // Add money to vending machine.
            this.AddMoney(payment);

            // Determine food weight.
            double weight = (double)(payment / this.foodPricePerPound);

            // Reduce stock level.
            this.foodStock -= weight;

            // Create and return food.
            return new Food(weight);
        }

        /// <summary>
        /// Determines the price of food for an animal of a specified weight.
        /// </summary>
        /// <param name="animalWeight">The weight of the animal for which to determine food price.</param>
        /// <returns>The price for the amount of food required to sufficiently feed an animal of the specified weight.</returns>
        public decimal DetermineFoodPrice(double animalWeight)
        {
            // Determine food weight as two percent of the animal weight.
            double foodWeight = animalWeight * 0.02;

            // Determine food price by multiplying food weight by price per pound.
            decimal foodPrice = (decimal)foodWeight * this.foodPricePerPound;

            // Round the price to two decimal places.
            foodPrice = Math.Round(foodPrice, 2);

            return foodPrice;
        }

        /// <summary>
        /// Add a bag of food to the vending machine.
        /// </summary>
        private void AddFoodBag()
        {
            this.foodStock = Math.Min(this.foodStock + this.bagSize, this.maxFoodStock);
        }

        /// <summary>
        /// Returns a value indicating whether or not the vending machine is full.
        /// </summary>
        /// <returns>A value indicating whether or not the vending machine is full.</returns>
        private bool IsFull()
        {
            return this.foodStock >= this.maxFoodStock;
        }
    }
}