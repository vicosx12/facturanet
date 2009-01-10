using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Server;
using Facturanet.Entities;
using System.Runtime.Serialization;

namespace Facturanet.Business
{
    [DataContract]
    public class ListProductsResponse : Response
    {
        [DataMember]
        public IList<Product> Products { get; set; }
    }
}
