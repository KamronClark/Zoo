using Animals;
using People;
using Reproducers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoos
{
    public static class ZooExtension
    {
        public static Animal FindAnimal(this Zoo zoo, Predicate<Animal> match)
        {
            return zoo.Animals.ToList().Find(match);
        }

        /// <summary>
        /// Finds a guest based on name.
        /// </summary>
        /// <param name="name">The name of the guest to find.</param>
        /// <returns>The first matching guest.</returns>
        public static Guest FindGuest(this Zoo zoo, Predicate<Guest> match)
        {
            return zoo.Guests.ToList().Find(match);
        }

        public static IEnumerable<object> GetYoungGuests(this Zoo zoo)
        {
            var returnValue = zoo.Guests
                              .Where(g => g.Age <= 10)
                              .Select(g => new { g.Name, g.Age });

            return returnValue;
        }

        public static IEnumerable<object> GetFemaleDingoes(this Zoo zoo)
        {
            var returnValue = zoo.Animals
                              .Where(a => a.GetType() == typeof(Dingo) && a.Gender == Gender.Female)
                              .Select(a => new { a.Name, a.Age, a.Weight, a.Gender});

            return returnValue;
        }

        public static IEnumerable<object> GetHeavyAnimals(this Zoo zoo)
        {
            var returnValue = zoo.Animals
                              .Where(a => a.Weight > 200)
                              .Select(a => new { Type = a.GetType(), a.Name, a.Age, a.Weight });

            return returnValue;
        }

        public static IEnumerable<object> GetGuestsByAge(this Zoo zoo)
        {
            var returnValue = zoo.Guests
                              .Where(g => g.GetType() == typeof(Guest))
                              .OrderBy(g => g.Age)
                              .Select(a => new { a.Name, a.Age, a.Gender});

            return returnValue;
        }

        public static IEnumerable<object> GetFlyingAnimals(this Zoo zoo)
        {
            var returnValue = zoo.Animals
                              .Where(a => a.MoveBehavior.GetType() == typeof(FlyBehavior))
                              .Select(a => new { Type = a.GetType(), a.Name } );

            return returnValue;
        }

        public static IEnumerable<object> GetAdoptedAnimals(this Zoo zoo)
        {
            var returnValue = zoo.Guests
                              .Where(g => g.AdoptedAnimal != null)
                              .Select(g => new { Adopter = g.Name, g.AdoptedAnimal.Name, Type = g.AdoptedAnimal.GetType()} );

            return returnValue;
        }

        public static IEnumerable<object> GetTotalBalanceByWalletColor(this Zoo zoo)
        {
            var returnValue = zoo.Guests
                         .GroupBy(g => g.Wallet.WalletColor)
                         .OrderBy(g => g.Key)
                         .Select(g => new { g.Key, TotalMoneyBalance = zoo.Guests.Where(w => w.Wallet.WalletColor == g.Key).Select(w => w.Wallet.MoneyBalance).Sum() });

            return returnValue;
        }

        public static IEnumerable<object> GetAverageWeightByAnimalType(this Zoo zoo)
        {
            var returnValue = zoo.Animals
                         .GroupBy(a => a.GetType())
                         //.OrderBy(a => a.Key) I couldn't get visual studio to understand how to order this without throwing an exception.
                         .Select(a => new { a.Key, AverageWeight = zoo.Animals.Where(w => w.GetType() == a.Key).Select(w => w.Weight).Average() });

            return returnValue;
        }
    }
}
