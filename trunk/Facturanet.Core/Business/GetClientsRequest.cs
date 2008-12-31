﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Server;
using System.Runtime.Serialization;

namespace Facturanet.Business
{
    [DataContract]
    public class GetClientsRequest : BaseRequest<GetClientsResponse>
    {
        [DataMember]
        public string Filter { get; set; }
    }
}
