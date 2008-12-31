using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Facturanet.Server
{
    [DataContract]
    [KnownType("GetKnownTypes")]
    public abstract class Response
    {
        static Type[] GetKnownTypes()
        {
            return FacturanetProcessorFactory.GetKnownTypesOf(typeof(Response));
        }
    }
}
