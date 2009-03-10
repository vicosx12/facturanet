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

            response.Items = context.Session
                .CreateQuery(@"
select 
    accountTree.Active, 
    accountTree.Code, 
    accountTree.Description, 
    accountTree.Id, 
    accountTree.Name 
from 
    AccountTree accountTree")
                //.ToDTOEnumerable<UI.AccountTreesListItem>("Active, Code, Description, Id, Name")
                .ToDTOEnumerable<UI.AccountTreesListItem>(tuple => 
                    new UI.AccountTreesListItem() 
                    {
                        Active = (bool)tuple[0],
                        Code = (string)tuple[1],
                        Description = (string)tuple[2],
                        Id = (Guid)tuple[3],
                        Name = (string)tuple[4]
                    })
                .ToList();
            return response;
        }
    }
}
