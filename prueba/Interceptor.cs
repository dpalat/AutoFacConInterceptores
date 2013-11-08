﻿using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace prueba
{
    public class Interceptor : IInterceptor
    {
        public void Intercept( IInvocation invocation )
        {
            Console.WriteLine( "--- INICIO INTERCEPTOR ---" );
            var timer = new Stopwatch();
            timer.Start();
            invocation.Proceed();
            var peso = this.ObtenerPeso( invocation.ReturnValue );
            timer.Stop();
            var atributos = invocation.Method.GetCustomAttributes( typeof( MedirConsumo ), true );
            if ( atributos.Any() )
            {
                Console.WriteLine( "Se realizo la metrica de consumos, el tiempo fue de: " + timer.Elapsed.Milliseconds.ToString() + " mls, peso: " + peso.ToString() );
            }
            Console.WriteLine( "--- FIN INTERCEPTOR ---" );
        }


        private long ObtenerPeso( object obj )
        {
            long size = 0;
            if ( obj != null )
            {
                using ( Stream s = new MemoryStream() )
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize( s, obj );
                    size = s.Length;
                }
            }
            return size;
        }
    }
}