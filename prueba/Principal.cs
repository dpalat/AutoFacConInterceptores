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
            Prueba01ContenedorWinsor();

            Console.ReadLine();
        }

        private static void Prueba01ContenedorWinsor()
        {
            // arrange
            var container = new WindsorContainer();
            container.Register(Component.For<IInterceptor>().ImplementedBy<Interceptor>().Named("MiInterceptor"));
            container.Register(Component.For<IHumano>().ImplementedBy<Hombre>().Named("ElMacho") );
            container.Register(Component.For<IHumano>().ImplementedBy<Mujer>().Named("LaHembra").LifestyleTransient().Interceptors("MiInterceptor"));
            //
            
            container.Resolve<IHumano>().Respirar();
            container.Resolve<IHumano>("LaHembra").Respirar();

            
        }

        private static void Prueba01ContenedorAutoFac()
        {
            var contenedor = new ContainerBuilder();

            // Se declaran como se resuelven las intancias y que interceptor intercepta a cada instancia.
            contenedor.Register(c => new Hombre()).EnableInterfaceInterceptors().InterceptedBy(typeof(Interceptor)).Named<IHumano>("ElPreInstanciado");
            contenedor.RegisterType<Hombre>().As<IHumano>().Named<IHumano>("ElMacho");
            contenedor.RegisterType<Mujer>().As<IHumano>().Named<IHumano>("LaHembra");

            // Se registra el Interceptor.
            var interceptor = new Interceptor();
            contenedor.Register(c => interceptor);


            // Se crea el contenedor.
            var container = contenedor.Build();


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
