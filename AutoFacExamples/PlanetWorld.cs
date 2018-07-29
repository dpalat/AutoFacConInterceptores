using Autofac;
using System;

namespace TestIoC
{
    public class PlanetWorld : IWorld
    {
        private IContainer _container;
        public string Name { get; set; }

        public PlanetWorld()
        {
            Console.WriteLine( "Welcome to planet Earth" );
        }

        public PlanetWorld( ContainerBuilder creadorDeContenedor )
        {
            _container = creadorDeContenedor.Build();
            var humano = _container.Resolve<IHuman>();
            humano.Breathe();
        }
    }
}

