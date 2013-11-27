using Autofac;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy2;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.DynamicProxy;
using Castle.Core;

namespace prueba
{
    public class Principal
    {
        static void Main( string[] args )
        {
            Console.WriteLine( "Iniciar" );
            //Prueba01ContenedorAutoFac();
            //Prueba01ContenedorWinsor();
            Prueba01ParaVerSiLaExpresionLambdaSeEjecutaSiempre();



            Console.ReadLine();
        }

        private static void Prueba01ParaVerSiLaExpresionLambdaSeEjecutaSiempre()
        {
            var creadorDeContenedor = new ContainerBuilder();
            creadorDeContenedor.RegisterType<Hombre>().As<IHumano>();
            creadorDeContenedor.RegisterModule<PruebaModulo>();
            var container = creadorDeContenedor.Build();
            
            var humano = container.Resolve<IHumano>();
            humano.Respirar();

            var animal = container.Resolve<IAnimal>();
            animal.Dormir();

            var planeta1 = container.ResolveNamed<IPlaneta>( "PlanetaPorReferencia" );
            var planeta2 = container.Resolve<IPlaneta>();
            var planeta3 = container.Resolve<IPlaneta>();
            Console.WriteLine( "Segunda etapa, planetas > a 3." );
            var planeta4 = container.ResolveNamed<IPlaneta>( "PlanetaPorLambda" );
            var planeta5 = container.ResolveNamed<IPlaneta>( "PlanetaPorLambda" );
            var planeta6 = container.ResolveNamed<IPlaneta>( "PlanetaPorLambda" );

        }

        private static void Prueba01ContenedorWinsor()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IInterceptor>().ImplementedBy<Interceptor>().Named("MiInterceptor"));
            container.Register(Component.For<IHumano>().ImplementedBy<Hombre>().Named("ElMacho") );
            container.Register(Component.For<IHumano>().ImplementedBy<Mujer>().Named("LaHembra").LifestyleTransient().Interceptors("MiInterceptor"));
            
            container.Resolve<IHumano>().Respirar();
            container.Resolve<IHumano>( "LaHembra").Respirar();
            container.Resolve<IHumano>( "LaHembra" ).Respirar();

            
        }

        private static void Prueba01ContenedorAutoFac()
        {
            var creadorDeContenedor = new ContainerBuilder();

            // Se declaran como se resuelven las intancias y que interceptor intercepta a cada instancia.
            creadorDeContenedor.Register(c => new Hombre()).EnableInterfaceInterceptors().InterceptedBy(typeof(Interceptor)).Named<IHumano>("ElPreInstanciado");
            creadorDeContenedor.RegisterType<Hombre>().As<IHumano>().Named<IHumano>("ElMacho");
            creadorDeContenedor.RegisterType<Mujer>().As<IHumano>().Named<IHumano>("LaHembra");

            // Se registra el Interceptor.
            var interceptor = new Interceptor();
            creadorDeContenedor.Register(c => interceptor);


            // Se crea el contenedor.
            var container = creadorDeContenedor.Build();


            //Se prueba
            var humano = container.ResolveNamed<IHumano>("ElMacho");
            humano.Respirar();

            var humano2 = container.Resolve<IHumano>();
            humano2.Respirar();

            var humano3 = container.ResolveNamed<IHumano>("ElPreInstanciado");
            humano3.Respirar();
        }


        [AttributeUsage( AttributeTargets.Method )]
        public class MedirConsumo : Attribute
        {
        }
    }
}

