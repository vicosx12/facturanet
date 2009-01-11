using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Entities;
using Facturanet.Server;
using Facturanet.Business;
using Facturanet.Tdo;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Facturanet.Business
{
    internal class ListInvoicesProcessor : PersistenceProcessor<ListInvoicesRequest, ListInvoicesResponse>
    {
        protected override ListInvoicesResponse RunInContext(ListInvoicesRequest request, PersistenceContext context)
        {
            IList<Invoice> list = context.Session.GetNamedQuery("ListInvoices_CompleteGraph").List<Invoice>();

            //TODO: en realidad aca solo tengo que devolver una lista mas simple que esto

            //TODO: Tengo que hacer algo para serializar los recursivos
            foreach (var invoice in list)
                foreach (var item in invoice.Items)
                    item.Invoice = null;

            return new ListInvoicesResponse() { List = list };
        }

    }
}
