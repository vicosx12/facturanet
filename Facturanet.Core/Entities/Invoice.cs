using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;


namespace Facturanet.Entities
{

    public class Invoice : Entity, Lines.ILineInvoice
    {
        public virtual Enterprise Enterprise { get; set; }
        public virtual string FiscalType { get; set; }
        public virtual string Number { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Customer Customer { get; set; }

        private IList<InvoiceItem> items = new List<InvoiceItem>();
        public virtual IList<InvoiceItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        string Lines.ILineInvoice.EnterpriseCode
        {
            get { return Enterprise.Code; }
        }

        string Lines.ILineInvoice.CustomerCode
        {
            get { return Customer.Code; }
        }

        string Lines.ILineInvoice.CustomerName
        {
            get { return Customer.Name; }
        }

        public virtual double Total
        {
            get { return Items.Sum(item => item.Quantity * item.Price); }
        }
    }
}




