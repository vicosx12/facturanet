using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Server;
using Facturanet.Business;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Facturanet.Business
{
    internal class ListAccountTreesProcessor : PersistenceProcessor<ListAccountTreesRequest, ListAccountTreesResponse>
    {
        protected override ListAccountTreesResponse RunInContext(ListAccountTreesRequest request, PersistenceContext context)
        {
            IList<Lines.ILineAccountTree> list = context.Session
                .CreateQuery("select new AccountTreesListItem(tree) from AccountTree tree")
                .SetReadOnly(true)
                .List<Lines.ILineAccountTree>();
            return new ListAccountTreesResponse() { List = list };
        }
    }
}
