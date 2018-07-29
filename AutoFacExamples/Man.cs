using System;

namespace TestIoC
{
    public class Man : IHuman
    {
        private readonly ISize _humanSize;

        public string Name { get; set; }

        public Man(ISize humanSize)
        {
            _humanSize = humanSize;
        }

        public void Eat()
        {
            Console.WriteLine("The man eats");
        }

        public void Breathe()
        {
            Console.WriteLine($"The man breathes {Name}");
        }

    }
}