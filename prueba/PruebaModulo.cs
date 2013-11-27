using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba
{
    public class PruebaModulo : Module
    {
        protected override void Load( ContainerBuilder creadorDeContenedor )
        {
            base.Load( creadorDeContenedor );
            creadorDeContenedor.RegisterType<Perro>().As<IAnimal>();
            var planeta = new PlanetaTiera( creadorDeContenedor );

            creadorDeContenedor.Register( e => new PlanetaTiera( ) ).As<IPlaneta>().Named<IPlaneta>( "PlanetaPorLambdaYRecibeBuilder" );
            creadorDeContenedor.Register( e => new PlanetaTiera() ).As<IPlaneta>().Named<IPlaneta>( "PlanetaPorLambda" );
            //Siempre la segunda interfaz o tipo concreto es la que se va a utilizar cuando no se especifica el Named.
            creadorDeContenedor.Register( e => planeta ).As<IPlaneta>().Named<IPlaneta>( "PlanetaPorReferencia" );
        }
    }
}

//public class TimeManagementModule 
//{
//    private const string TimeManagement = "zNubeTimeManagementEntities";

//    protected override void Load( ContainerBuilder builder )
//    {
        

//        var connStr = ConfigurationManager.ConnectionStrings[ TimeManagement ].ConnectionString;

//        builder.Register<ZNube.Services.TimeManagement.Service>( c =>
//                                                                new ZNube.Services.TimeManagement.Service(
//                                                                        c.Resolve<ZNubeContext>(),
//                                                                        c.Resolve<ITimeManagementRepository>()
//                                                                                                         )
//                                                               )
//            .As<ZNube.Services.TimeManagement.IService>()
//            .EnableInterfaceInterceptors()
//            .InterceptSecurity();

//        builder.Register<SqlTimeManagementRepository>( c => new SqlTimeManagementRepository( connStr, new ZNubeEntities.ZNubeEntityProvider() ) )
//            .As<ITimeManagementRepository>();
//    }
//}
