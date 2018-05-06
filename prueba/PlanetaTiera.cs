using Autofac;
using System;

namespace TestIoC
{
    public class PlanetaTiera : IPlaneta
    {
        private IContainer _container;
        public string Nombre { get; set; }

        public PlanetaTiera()
        {
            Console.WriteLine( "Welcome to planet Earth" );
        }

        public PlanetaTiera( ContainerBuilder creadorDeContenedor )
        {
            _container = creadorDeContenedor.Build();
            var humano = _container.Resolve<IHuman>();
            humano.Breathe();
        }
    }
}

