using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Facturanet.Server;

namespace Facturanet.Entities
{
    [DataContract]
    [KnownType("GetKnownTypes")]
    public abstract class Entity
    {
        [DataMember]
        public virtual Guid Id { get; set; }

        static Type[] GetKnownTypes()
        {
            return FacturanetProcessorFactory.GetKnownTypesOf(typeof(Entity));
        }
    }
}
