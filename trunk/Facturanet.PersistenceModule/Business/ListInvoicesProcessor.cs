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

namespace Facturanet.Business
{
    internal class ListInvoicesProcessor : PersistenceProcessor<ListInvoicesRequest, ListInvoicesResponse>
    {
        protected override ListInvoicesResponse RunInContext(ListInvoicesRequest request, PersistenceContext context)
        {
            IList<Lines.ILineInvoice> list = context.Session.GetNamedQuery("InvoicesList").List<Lines.ILineInvoice>();
            //IList<Invoice> list = context.Session.GetNamedQuery("Invoices_CompleteGraph").List<Invoice>();
            //////TODO: Tengo que hacer algo para poder enviar los objetos con referencias recursivas
            ////foreach (var invoice in list)
            ////    foreach (var item in invoice.Items)
            ////        item.Invoice = null;

            return new ListInvoicesResponse() { List = list };
        }

    }
}
