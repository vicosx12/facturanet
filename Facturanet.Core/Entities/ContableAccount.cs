using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Facturanet.Entities.Base
{
    public abstract class ContableAccountBase : Entity
    {
        public virtual string Code { get; set; }
        public virtual bool Active { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Imputable { get; set; }
    }
}

namespace Facturanet.Entities
{
    public class ContableAccount : Base.ContableAccountBase
    {
        public virtual ContableAccount ParentAccount { get; set; }
        public virtual AccountTree AccountTree { get; set; }
    }
}
