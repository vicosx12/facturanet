using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Server;
using Facturanet.Business;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Facturanet.NHUtil;


namespace Facturanet.Business
{
    internal class ListAccountTreesProcessor : PersistenceProcessor<ListAccountTreesRequest, ListAccountTreesResponse>
    {
        protected override ListAccountTreesResponse RunInContext(ListAccountTreesRequest request, PersistenceContext context)
        {
            var response = new ListAccountTreesResponse();
            response.Items = new List<Facturanet.UI.AccountTreesListItem>(context.Session
                .CreateQuery("select accountTree from AccountTree accountTree")
                .ToDTOEnumerable<UI.AccountTreesListItem>("CopyFromAccountTree"));

            return response;
        }
    }
}
