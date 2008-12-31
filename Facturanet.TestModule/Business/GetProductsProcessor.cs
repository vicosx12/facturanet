using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Entities;
using Facturanet.Server;
using Facturanet.Business;


namespace Facturanet.Business
{
    internal class GetProductsProcessor : Processor<GetProductsRequest,GetProductsResponse>
    {
        public override GetProductsResponse Run(GetProductsRequest request, IContext context)
        {
            return new GetProductsResponse()
            {
                Products = new List<Product>()
                {
                    new Product() { Id = 1, Taxes = 1, Name = "Articulo1"},
                    new Product() { Id = 2, Taxes = 2, Name = "Articulo2"},
                    new Product() { Id = 3, Taxes = 3, Name = "Articulo3"},
                    new Product() { Id = 4, Taxes = 4, Name = "Articulo4"}
                }
            };
        }
    }
}
