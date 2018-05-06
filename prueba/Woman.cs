using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIoC
{
    public class Woman : IHuman
    {
        public string Name { get; set; }

        public void Breathe()
        {
            Console.WriteLine("The woman breathes.");
        }
    }
}
