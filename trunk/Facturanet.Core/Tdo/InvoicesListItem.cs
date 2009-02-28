using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Entities;

namespace Facturanet.Tdo
{
    public class InvoicesListItem : Facturanet.Entities.Entity, Lines.ILineInvoice
    {
        public string FiscalType { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }
        public string EnterpriseCode { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }

        public InvoicesListItem() : base()
        {
        }
      
        public InvoicesListItem(
            Guid id,
            string enterpriseCode,
            string fiscalType,
            string number,
            DateTime date,
            string customerCode,
            string customerName,
            double total)
        {
            Id = id;
            EnterpriseCode = enterpriseCode;
            FiscalType = fiscalType;
            Number = number;
            Date = date;
            CustomerName = customerName;
            CustomerCode = customerCode;
            Total = total;
        }
    }
}
