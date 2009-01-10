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
    internal class ListProductsProcessor : PersistenceProcessor<ListProductsRequest, ListProductsResponse>
    {
        protected override ListProductsResponse RunInContext(ListProductsRequest request, PersistenceContext context)
        {
            IList<Product> list = context.Session.CreateCriteria(typeof(Product)).List<Product>();
            return new ListProductsResponse() { Products = list };
        }

    }
}
