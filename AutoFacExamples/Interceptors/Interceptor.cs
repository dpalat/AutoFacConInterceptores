using Castle.DynamicProxy;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace TestIoC
{
    public class Interceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"    **** Start Interceptor: {invocation.TargetType.FullName} ****");
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            invocation.Proceed();

            var size = GetResponseSize(invocation.ReturnValue);
            stopWatch.Stop();

            var attributes = invocation.Method.GetCustomAttributes(typeof(TraceResponseSize), true);

            if (attributes.Any())
            {
                Console.WriteLine($"    **** Response time: {stopWatch.Elapsed.Milliseconds.ToString()}" +
                                  $" mls, Response Size: {size.ToString()}");
            }
            Console.WriteLine("    **** End Interceptor ****");
        }

        private long GetResponseSize(object obj)
        {
            long size = 0;
            if (obj != null)
            {
                using (Stream s = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(s, obj);
                    size = s.Length;
                }
            }
            return size;
        }
    }
}