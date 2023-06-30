using System;
using System.Collections.Generic;

namespace VarmDrinkStation
{
    public interface IWarmDrink
    {
        void Consume();
    }

    internal class Water : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Warm water for you is served.");
        }
    }

    internal class Coffee : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Coffee for you is served.");
        }
    }

    internal class Cappuccino : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Cappuccino for you is served.");
        }
    }

    internal class HotChocolate : IWarmDrink
    {
        public void Consume()
        {
            Console.WriteLine("Hot chocolate for you is served.");
        }
    }

    public interface IWarmDrinkFactory
    {
        IWarmDrink Prepare(int total);
    }

    internal class HotWaterFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Pour {total} ml hot water in your glas");
            return new Water();
        }
    }

    internal class CoffeeFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Brew {total} ml of coffee");
            return new Coffee();
        }
    }

    internal class CappuccinoFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Prepare {total} ml of cappuccino");
            return new Cappuccino();
        }
    }

    internal class HotChocolateFactory : IWarmDrinkFactory
    {
        public IWarmDrink Prepare(int total)
        {
            Console.WriteLine($"Mix {total} ml of hot chocolate");
            return new HotChocolate();
        }
    }

    public class WarmDrinkMachine
    {
        private Dictionary<int, IWarmDrinkFactory> factories = new Dictionary<int, IWarmDrinkFactory>()
        {
            { 1, new HotWaterFactory() },
            { 2, new CoffeeFactory() },
            { 3, new CappuccinoFactory() },
            { 4, new HotChocolateFactory() }

        };

        public IWarmDrink MakeDrink()
        {
            Console.WriteLine("This is what we serve today:");
            foreach (var factory in factories)
            {
                Console.WriteLine($"{factory.Key}: {factory.Value.GetType().Name.Replace("Factory", string.Empty)}");
            }

            Console.WriteLine("Select a number to continue:");

            while (true)
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && factories.ContainsKey(choice))
                {
                    Console.Write("How much: ");
                    string amountInput = Console.ReadLine();
                    if (int.TryParse(amountInput, out int total) && total > 0)
                    {
                        return factories[choice].Prepare(total);
                    }
                }

                Console.WriteLine("Something went wrong with your input, try again.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new WarmDrinkMachine();
            IWarmDrink drink = machine.MakeDrink();
            drink.Consume();
        }
    }
}
