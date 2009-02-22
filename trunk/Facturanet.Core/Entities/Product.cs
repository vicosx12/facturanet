using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Facturanet.Entities
{
    public class Product : Entity, Lines.ILineProduct
    {
        public virtual string Code { get; set; }
        public virtual bool Active { get; set; }
        public virtual string Name { get; set; }
        public virtual double Taxes { get; set; }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
