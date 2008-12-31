using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Facturanet.Entities
{
    
    public class Customer : Entity
    {
        //public virtual int Id { get; set; }

        
        public virtual string Name { get; set; }
    }
}
