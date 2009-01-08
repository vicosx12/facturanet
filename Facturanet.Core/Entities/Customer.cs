using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Facturanet.Entities
{
    
    public class Customer : Entity
    {
        public virtual string FiscalId { get; set; }
        public virtual string FiscalType { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
    }
}
