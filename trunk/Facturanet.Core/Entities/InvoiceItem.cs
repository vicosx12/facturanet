using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Facturanet.Entities
{
    //TODO: Sacar la herencia de Entity
    public class InvoiceItem : Entity
    {
        public virtual Invoice Invoice { get; set; }
        public virtual int InvoiceLine { get; set; }
        public virtual Product Product { get; set; }
        public virtual decimal Quantity { get; set; }
        public virtual decimal BasePrice { get; set; }
        
        //Esto me parece que está mal
        public virtual decimal FinalPrice
        {
            get 
            {
                return 0;
                /*
                if (Product == null)
                    return 0;
                else
                    return BasePrice * (1 + Product.Taxes); 
                */
            }
        }
    }
}
