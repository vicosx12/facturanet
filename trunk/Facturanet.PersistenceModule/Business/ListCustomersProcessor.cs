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
    internal class ListCustomersProcessor : PersistenceProcessor<ListCustomersRequest, ListCustomersResponse>
    {
        protected override ListCustomersResponse RunInContext(ListCustomersRequest request, PersistenceContext context)
        {
            IList<Customer> list = context.Session.CreateCriteria(typeof(Customer)).List<Customer>();
            return new ListCustomersResponse() { List = list };
        }

    }
}
