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

        static private Type[] knownTypesCache = null;
        static Type[] GetKnownTypes()
        {
            if (knownTypesCache == null)
                knownTypesCache = FacturanetProcessorFactory.GetKnownTypesOf(typeof(Entity));
            return knownTypesCache;
        }

        public Entity()
        {
            Id = Util.IdentifierHelper.GenerateComb();
        }
    }
}
