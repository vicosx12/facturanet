using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Entities;

namespace Facturanet.DTOs
{
    public class InvoicesListItem : Entities.Base.InvoiceBase, DTOs.IDTO
    {
        public string EnterpriseCode { get; private set; }
        public string CustomerCode { get; private set; }
        public string CustomerName { get; private set; }
        public double Total { get; private set; }

        public Entities.Base.InvoiceBase CopyFromInvoice
        {
            set
            {
                Date = value.Date;
                FiscalType = value.FiscalType;
                Id = value.Id;
                Number = value.Number;

                //EnterpriseCode = enterpriseCode;
                //CustomerCode = customerCode;
                //CustomerName = customerName;
                //Total = total;
            }
        }
    }
}
