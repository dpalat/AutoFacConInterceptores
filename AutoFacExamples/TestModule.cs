using Autofac;

namespace TestIoC
{
    public class TestModule : Module
    {
        protected override void Load(ContainerBuilder creadorDeContenedor)
        {
            base.Load(creadorDeContenedor);
            creadorDeContenedor.RegisterType<Dog>().As<IAnimal>();
            var planeta = new PlanetWorld(creadorDeContenedor);

            creadorDeContenedor.Register(e => new PlanetWorld()).As<IWorld>().Named<IWorld>("PlanetaPorLambdaYRecibeBuilder");
            creadorDeContenedor.Register(e => new PlanetWorld()).As<IWorld>().Named<IWorld>("PlanetaPorLambda");
            //Siempre la segunda interfaz o tipo concreto es la que se va a utilizar cuando no se especifica el Named.
            creadorDeContenedor.Register(e => planeta).As<IWorld>().Named<IWorld>("PlanetaPorReferencia");
        }
    }
}