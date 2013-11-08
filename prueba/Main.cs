using Autofac;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy2;

namespace prueba
{
    public class Main
    {
        static void Main( string[] args )
        {
            Console.WriteLine( "Iniciar" );
            var contenedor = new ContainerBuilder();

            contenedor.Register( c => new Hombre() ).EnableInterfaceInterceptors().InterceptedBy( typeof( Interceptor ) ).Named<IHumano>( "ElPreInstanciado" );
            contenedor.RegisterType<Hombre>().As<IHumano>().Named<IHumano>( "ElMacho" );
            contenedor.RegisterType<Mujer>().As<IHumano>().Named<IHumano>( "LaHembra" );

            var interceptor = new Interceptor();
            contenedor.Register( c => interceptor );

            var container = contenedor.Build();

            var humano = container.ResolveNamed<IHumano>( "ElMacho" );
            humano.Respirar();

            var humano2 = container.Resolve<IHumano>();
            humano2.Respirar();

            var humano3 = container.ResolveNamed<IHumano>( "ElPreInstanciado" );
            humano3.Respirar();

            Console.ReadLine();
        }


        [AttributeUsage( AttributeTargets.Method )]
        public class MedirConsumo : Attribute
        {
        }

        //public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptAccounting<TLimit, TActivatorData, TStyle>( this IRegistrationBuilder<TLimit, TActivatorData, TStyle> i )
        //{
        //    return i.InterceptedBy;
        //}

    }
}
