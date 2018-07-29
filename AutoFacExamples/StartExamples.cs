using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TestIoC
{
    public class StartExamples
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Start IoC Test");
            Test_IoC_With_AutoFac_Simple();
            Test_IoC_With_AutoFac_Named();
            Test_IoC_With_AutoFac_Interceptor();
            Test_IoC_With_CastleWinsor();
            Test_DI();
            Console.ReadLine();
        }

        private static void Log(string log)
        {
            Console.WriteLine($"");
            Console.WriteLine($"++++++++{log}++++++++");
        }

        private static void Test_DI()
        {
            Log(GetCurrentMethod());
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Size>().As<ISize>();
            containerBuilder.RegisterType<Man>().As<IHuman>();

            var container = containerBuilder.Build();
            var human1 = container.Resolve<IHuman>();
            human1.Breathe();
        }

        private static void Test_IoC_With_AutoFac_Simple()
        {
            Log(GetCurrentMethod());
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Man>().As<IHuman>();
            containerBuilder.RegisterType<Woman>().As<IHuman>();

            var container = containerBuilder.Build();
            var human1 = container.Resolve<IHuman>();
            human1.Breathe();

            var human2 = container.Resolve<IHuman>();
            human2.Breathe();
        }

        private static void Test_IoC_With_AutoFac_Named()
        {
            Log(GetCurrentMethod());
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Size>().As<ISize>();
            containerBuilder.RegisterType<Man>().As<IHuman>().Named<IHuman>("ManNamed1");
            containerBuilder.Register(c => new Man(null) {Name = "Jhon"}).As<IHuman>().Named<IHuman>("ManNamed2");
            containerBuilder.RegisterType<Woman>().As<IHuman>().Named<IHuman>("Woman");

            var interceptor = new Interceptor();
            containerBuilder.Register(c => interceptor);

            var container = containerBuilder.Build();

            var human1 = container.ResolveNamed<IHuman>("ManNamed1");
            human1.Breathe();

            var human2 = container.ResolveNamed<IHuman>("ManNamed2");
            human2.Breathe();

            var human3 = container.ResolveNamed<IHuman>("Woman");
            human3.Breathe();

            var human4 = container.Resolve<IHuman>(); // Give the last type registered for this interface
            human4.Breathe();
        }

        private static void Test_IoC_With_AutoFac_Interceptor()
        {
            Log(GetCurrentMethod());
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Size>().As<ISize>();

            containerBuilder.Register(c =>new Man(new Size())).EnableInterfaceInterceptors().InterceptedBy(typeof(Interceptor)).Named<IHuman>("ManWithIntercepAndSelectConstructor");

            containerBuilder.RegisterType<Man>().As<IHuman>().EnableInterfaceInterceptors().InterceptedBy(typeof(Interceptor)).Named<IHuman>("ManWithIntercep");

            containerBuilder.RegisterType<Man>().As<IHuman>().Named<IHuman>("ManNamed");
            containerBuilder.RegisterType<Woman>().As<IHuman>().Named<IHuman>("Woman");

            var interceptor = new Interceptor();
            containerBuilder.Register(c => interceptor);

            var container = containerBuilder.Build();

            var humano = container.ResolveNamed<IHuman>("ManNamed");
            humano.Breathe();

            var humano2 = container.Resolve<IHuman>();
            humano2.Breathe();

            var humano3 = container.ResolveNamed<IHuman>("ManWithIntercepAndSelectConstructor");
            humano3.Breathe();

            var humano4 = container.ResolveNamed<IHuman>("ManWithIntercep");
            humano3.Breathe();
        }

        private static void Test_IoC_With_CastleWinsor()
        {
            Log(GetCurrentMethod());
            var container = new WindsorContainer();
            
            container.Register(Component.For<IInterceptor>().ImplementedBy<Interceptor>().Named("MyInterceptor"));
            container.Register(Component.For<ISize>().ImplementedBy<Size>());

            container.Register(Component.For<IHuman>().ImplementedBy<Man>().Named("TheMan"));
            container.Register(Component.For<IHuman>().ImplementedBy<Woman>().Named("TheWoman").LifestyleTransient().Interceptors("MyInterceptor"));

            container.Resolve<IHuman>().Breathe();
            container.Resolve<IHuman>("TheWoman").Breathe();
            container.Resolve<IHuman>("TheMan").Breathe();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            return sf.GetMethod().Name;
        }
    }
}