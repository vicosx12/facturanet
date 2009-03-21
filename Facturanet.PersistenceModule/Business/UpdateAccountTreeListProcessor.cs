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
    internal class UpdateAccountTreeListProcessor : PersistenceEmptyResponseProcessor<UpdateAccountTreeListRequest>
    {
        protected override void RunInContextEmptyResponse(UpdateAccountTreeListRequest request, PersistenceContext context)
        {
            foreach (Guid id in request.DeletedIds)
            {
                //AccountTree entity = context.Session.Load<AccountTree>(id);
                //context.Session.Delete(entity);
                AccountTree entity = context.Session.Load<AccountTree>(id);
                entity.MarkAsDelete();
            }

            context.Session.Flush();

            //Modifico los elementos modificados
            foreach (UI.AccountTreesListItem ui in request.UpdatedItems)
            {
                //TODO: si aca tuviera los valores originales podría verificar que los valores originales sean iguales o los actuales en la db.
                AccountTree entity = context.Session.Load<AccountTree>(ui.Id);

                //Esto no sirve porque nhibernate usa un numero de version obtenido en el load:
                //entity.Version = ui.Version; 
                /*
                //Esta es la forma de implementar el control de concurrencia 
                //optimista, pero es mas ineficiente. Así que por ahora no lo voy
                //a usar.
                 * 
                if (entity.Version != ui.Version)
                    throw new StaleObjectStateException("AccountTree", ui.Id);
                */
                entity.Active = ui.Active;
                entity.Code = ui.Code;
                entity.Description = ui.Description;
                entity.Name = ui.Name;
            }

            context.Session.Flush();

            //Creo los nuevos elementos
            foreach (UI.AccountTreesListItem ui in request.CreatedItems)
            {
                AccountTree entity = new AccountTree()
                {
                    Active = ui.Active,
                    Code = ui.Code,
                    Description = ui.Description,
                    Name = ui.Name
                };
                context.Session.Save(entity, ui.Id);
            }
            

            
            //TODO: cuando pueda ver las consultas que hace a la db me fijo 
            //si vale la pena optimizarlo.
        }
    }
}
