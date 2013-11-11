using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core;

namespace prueba
{
    public class Hombre : IHumano
    {

        [MedirConsumo]
        //[Interceptor("MiInterceptor")]
        public void Respirar()
        {
            Console.WriteLine( "El Hombre respira..." );
        }

        
        public void Comer()
        {
            Console.WriteLine( "El Hombre respira..." );
        }
    }
}
