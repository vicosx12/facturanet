using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Entities;
using Facturanet.Server;
using Facturanet.Business;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Facturanet.NHUtil;

namespace Facturanet.Business
{
    internal class ListInvoicesProcessor : PersistenceProcessor<ListInvoicesRequest, ListInvoicesResponse>
    {
        protected override ListInvoicesResponse RunInContext(ListInvoicesRequest request, PersistenceContext context)
        {
            List<DTOs.InvoicesListItem> list = context.Session
                .GetNamedQuery("InvoicesList")
                .DTOList<DTOs.InvoicesListItem>(new string[] 
                {
                    "CopyFromInvoice", 
                    "Total", 
                    "EnterpriseCode", 
                    "CustomerCode", 
                    "CustomerName"
                });
            return new ListInvoicesResponse() { Items = list };
        }

    }
}
