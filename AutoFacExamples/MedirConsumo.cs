using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIoC
{
    [AttributeUsage( AttributeTargets.Method )]
    public class TraceResponseSize : Attribute
    {
    }
}
