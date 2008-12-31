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
    internal class RemoveProductProcessor : PersistenceProcessor<RemoveProductRequest, RemoveProductResponse>
    {
        protected override RemoveProductResponse RunInContext(RemoveProductRequest request, PersistenceContext context)
        {
            //TODO: Terminar con los Selectores
            Product product = request.ProductIdentificator.GetEntity(context);

            return new RemoveProductResponse()
            {
                RespuestaPrueba = product == null
                ? "no encontrado"
                : product.Id.ToString() + "; " + product.Name + "; " + product.Taxes.ToString()
            };
        }
    }
}
