using Autofac;

namespace TestIoC
{
    public class PruebaModulo : Module
    {
        protected override void Load( ContainerBuilder creadorDeContenedor )
        {
            base.Load( creadorDeContenedor );
            creadorDeContenedor.RegisterType<Perro>().As<IAnimal>();
            var planeta = new PlanetaTiera(creadorDeContenedor);

            creadorDeContenedor.Register(e => new PlanetaTiera()).As<IPlaneta>().Named<IPlaneta>("PlanetaPorLambdaYRecibeBuilder");
            creadorDeContenedor.Register(e => new PlanetaTiera()).As<IPlaneta>().Named<IPlaneta>("PlanetaPorLambda");
            //Siempre la segunda interfaz o tipo concreto es la que se va a utilizar cuando no se especifica el Named.
            creadorDeContenedor.Register(e => planeta).As<IPlaneta>().Named<IPlaneta>("PlanetaPorReferencia");
        }
    }
}