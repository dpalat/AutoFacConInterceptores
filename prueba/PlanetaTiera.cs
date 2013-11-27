using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba
{
    public class PlanetaTiera : IPlaneta
    {
        private string _nombre;
        private Autofac.IContainer container;
        public string Nombre
        {
            get
            {
                return this._nombre;
            }
            set
            {
                this._nombre = value;
            }
        }

        public PlanetaTiera()
        {
            Console.WriteLine( "BIENVENIDOS AL PLANETA TIERRA" );
        }

        public PlanetaTiera( ContainerBuilder creadorDeContenedor )
        {
            this.container = creadorDeContenedor.Build();
            var humano = this.container.Resolve<IHumano>();
            humano.Respirar();
        }
    }
}

