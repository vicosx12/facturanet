using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Facturanet.Entities.Base
{
    public abstract class InvoiceBase : Entity
    {
        public virtual string FiscalType { get; set; }
        public virtual string Number { get; set; }
        public virtual DateTime Date { get; set; }
        
        /*
         * Esto no debería ir, si lo necesito lo hago, ¿para que tengo las cosas?
        public abstract string EnterpriseCode { get; }
        public abstract string CustomerCode { get; }
        public abstract string CustomerName { get; }
        public abstract double Total { get; }
         * */
    }
}

namespace Facturanet.Entities
{
    public class Invoice : Base.InvoiceBase
    {
        public virtual Enterprise Enterprise { get; set; }
        public virtual Customer Customer { get; set; }

        private IList<InvoiceItem> items = new List<InvoiceItem>();
        public virtual IList<InvoiceItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        /*
        public override string EnterpriseCode 
        {
            get { return Enterprise.Code; }
        }
        public override string CustomerCode 
        {
            get { return Customer.Code; }
        }

        public override string CustomerName 
        {
            get { return Customer.Name; }
        }

        public override double Total 
        {
            get { return Items.Sum(i => i.Total); }
        }
         * */
    }
}




