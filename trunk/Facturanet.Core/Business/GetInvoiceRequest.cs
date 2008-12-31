using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Server;
using System.Runtime.Serialization;

namespace Facturanet.Business
{
    [DataContract]
    public class GetInvoiceRequest : BaseRequest<GetInvoiceResponse>
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Number { get; set; }
    }
}
