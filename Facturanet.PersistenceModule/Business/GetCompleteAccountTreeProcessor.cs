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
using NHibernate.Criterion;


namespace Facturanet.Business
{
    internal class GetCompleteAccountTreeProcessor : PersistenceProcessor<GetCompleteAccountTreeRequest, GetCompleteAccountTreeResponse>
    {
        #region si fuera una arbol de cuentas
        /*
        private List<UI.ContableAccount> EntityListToUIList(IEnumerable<Entities.ContableAccount> accounts)
        {
            var list = new List<UI.ContableAccount>();

            foreach (var sub in accounts)
            {
                UI.ContableAccount uiSub = new UI.ContableAccount(sub.Id)
                {
                    Active = sub.Active,
                    Code = sub.Code,
                    Description = sub.Description,
                    Imputable = sub.Imputable,
                    Name = sub.Name,
                    Version = sub.Version
                };
                uiSub.Subaccounts = EntityListToUIList(sub.Subaccounts);

                list.Add(uiSub);
            }

            return list;
        }
        */
        #endregion
        protected override GetCompleteAccountTreeResponse RunInContext(GetCompleteAccountTreeRequest request, PersistenceContext context)
        {
            
            var response = new GetCompleteAccountTreeResponse();
            
            Entities.AccountTree tree = context.Session
                .CreateCriteria(typeof(Entities.AccountTree), "tree")
                .Add(Expression.IdEq(request.AccountTreeId))
                .SetFetchMode("Accounts", FetchMode.Eager)
                .UniqueResult<Entities.AccountTree>();

            response.AccountTreeHeader =
                new UI.AccountTreesListItem(tree.Id)
                {
                    Active = tree.Active,
                    Code = tree.Code,
                    Description = tree.Description,
                    Name = tree.Name,
                    Version = tree.Version
                };

            response.Items = new List<Facturanet.UI.ContableAccount>();

            foreach (var entity in tree.Accounts)
            {
                response.Items.Add(
                    new UI.ContableAccount(entity.Id)
                    {
                        Active = entity.Active,
                        Code = entity.Code,
                        Description = entity.Description,
                        Imputable = entity.Imputable,
                        Name = entity.Name,
                        ParentAccountId = entity.ParentAccount == null 
                            ? null
                            : (Guid?)entity.ParentAccount.Id,
                        Version = entity.Version
                    });
            }

            #region si fuera un arbol de cuentas

            var rootAccounts =
                from account in tree.Accounts
                where account.ParentAccount == null
                select account;

            #region Otras formas
            //Opcion 1: Obtener el tree sin FetchMode:
            //Entities.AccountTree tree = context.Session.Load<Entities.AccountTree>(request.AccountTreeId);
            //Opcion 2: Hacer el select de los nulls, pero no se como traer todo
            /*
            var rootAccounts = context.Session
                .CreateQuery(@"
                    select 
                        account
                    from
                        AccountTree as tree
                        join tree.Accounts as account
                    where
                        account.ParentAccount is null
                        and tree.Id = :AccountTreeId
                    ")
                .SetGuid("AccountTreeId", request.AccountTreeId)
                .Enumerable<Entities.ContableAccount>();
            */
            #endregion
            /*
            response.Items = EntityListToUIList(rootAccounts);
            */
            #endregion

            return response;
        }
        
    }
}
