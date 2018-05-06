using System;

namespace TestIoC
{
    public class Man : IHuman
    {
        public string Name { get; set; }

        public void Eat()
        {
            Console.WriteLine("The man eats");
        }

        [MedirConsumo]
        public void Breathe()
        {
            Console.WriteLine($"The man breathes {Name}");
        }

    }
}