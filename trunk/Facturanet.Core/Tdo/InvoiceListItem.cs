using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Entities;

namespace Facturanet.Tdo
{
    public class InvoiceListItem
    {
        public virtual string EnterpriseName { get; set; }
        public virtual string EnterpriseCode { get; set; }
        public virtual string FiscalType { get; set; }
        public virtual string Number { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual string CustomerCode { get; set; }
        public decimal Total { get; set; }

        public InvoiceListItem()
        {
        }

        public InvoiceListItem(
            string enterpriseName,
            string enterpriseCode,
            string fiscalType,
            string number,
            DateTime date,
            string customerName,
            string customerCode,
            decimal total)
        {
            EnterpriseName = enterpriseName;
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
