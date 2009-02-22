using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Facturanet.Entities
{
    // TODO: Sacar la herencia de Entity
    public class InvoiceItem : Entity
    {
        // public virtual Invoice Invoice { get; set; }
        public virtual int InvoiceLine { get; set; }
        public virtual Product Product { get; set; }
        public virtual double Quantity { get; set; }
        public virtual double Price { get; set; }
    }
}
