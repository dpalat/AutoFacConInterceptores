using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIoC
{
    public class Dog : IAnimal
    {
        public void Sleep()
        {
            Console.WriteLine( "The Dog is sleeping!" );
        }
    }
}
