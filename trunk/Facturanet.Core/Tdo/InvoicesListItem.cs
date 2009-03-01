using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Entities;

namespace Facturanet.Tdo
{
    public class InvoicesListItem : Entities.Base.InvoiceBase, Tdo.ITdo
    {
        public string EnterpriseCode { get; private set; }
        public string CustomerCode { get; private set; }
        public string CustomerName { get; private set; }
        public double Total { get; private set; }

        public InvoicesListItem(Invoice invoice, string enterpriseCode, string customerCode, string customerName, double total)
        {
            Date = invoice.Date;
            FiscalType = invoice.FiscalType;
            Id = invoice.Id;
            Number = invoice.Number;

            EnterpriseCode = enterpriseCode;
            CustomerCode = customerCode;
            CustomerName = customerName;
            Total = total;
        }
    }
}
