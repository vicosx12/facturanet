using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace Facturanet.Entities
{

    
    public class Invoice : Entity
    {
        //public virtual int Id { get; set; }

        
        public virtual string Number { get; set; }

        
        public virtual Customer Customer { get; set; }

        private IList<InvoiceItem> items = new List<InvoiceItem>();

        
        public virtual IList<InvoiceItem> Items
        {
            get { return items; }
            set { items = value; }
        }
    }
}




