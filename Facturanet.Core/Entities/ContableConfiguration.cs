using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Facturanet.Entities
{
    public class ContableConfiguration : Entity
    {
        public virtual string Code { get; set; }
        public virtual bool Active { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual AccountTree AccountTree { get; set; }
        public virtual ContableAccount DefaultAccount { get; set; }
    }
}
