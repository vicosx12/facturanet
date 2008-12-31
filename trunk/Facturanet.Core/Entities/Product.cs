using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Facturanet.Entities
{
    
    public class Product : Entity
    {
        //public virtual int Id { get; set; }

        
        public virtual string Name { get; set; }

        
        public virtual decimal Taxes { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
