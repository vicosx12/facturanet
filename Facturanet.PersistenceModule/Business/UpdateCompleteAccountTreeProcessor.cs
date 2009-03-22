using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Server;
using Facturanet.Business;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Facturanet.Entities;

namespace Facturanet.Business
{
    internal class UpdateCompleteAccountTreeProcessor : PersistenceEmptyResponseProcessor<UpdateCompleteAccountTreeRequest>
    {
        protected override void RunInContextEmptyResponse(UpdateCompleteAccountTreeRequest request, PersistenceContext context)
        {
            AccountTree tree = context.Session.Load<AccountTree>(request.AccountTreeId);

            if (request.AccountTreeHeader != null)
            {
                request.AccountTreeHeader.CopyTo(tree);
            }

            foreach (UI.ContableAccount ui in request.UpdatedAccounts)
            {
                ContableAccount entity = context.Session.Load<ContableAccount>(ui.Id);
                ui.CopyTo(entity); //las modificaciones sin cambio de padres
            }

            foreach (UI.ContableAccount ui in request.CreatedAccounts)
            {
                ContableAccount entity = new ContableAccount(ui.Id);
                ui.CopyTo(entity);
                entity.AccountTree = tree;
                context.Session.Save(entity); //los grabo sin padre
            }

            //para cada una de las modificadas o creadas les pongo el padre
            //no se si anda bien por el tema de la bidireccionalidad
            foreach (UI.ContableAccount ui in request.UpdatedAccounts.Union(request.CreatedAccounts))
            {
                ContableAccount entity = context.Session.Load<ContableAccount>(ui.Id);
                if (ui.ParentAccountId.HasValue)
                    entity.ParentAccount = context.Session.Load<ContableAccount>(ui.ParentAccountId.Value);
                else
                    entity.ParentAccount = null;
            }
                
/*
            //TODO: No estoy seguro de que esto esté funcionando bien
            foreach (UI.TreeOrderPair pair in request.ReorderData)
            {
                ContableAccount child = context.Session.Load<ContableAccount>(pair.ChildId);
                if (pair.ParentId.HasValue)
                {
                    ContableAccount parent = context.Session.Load<ContableAccount>(pair.ParentId.Value);
                    //parent.AddSubaccount(child);
                    child.ParentAccount = parent;
                }
                else
                {
                    child.ParentAccount = null;
                }
            }
 */ 
        }
    }
}
