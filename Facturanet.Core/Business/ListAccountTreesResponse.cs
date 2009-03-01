﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Server;
using System.Runtime.Serialization;

namespace Facturanet.Business
{
    [DataContract]
    [KnownType(typeof(Entities.AccountTree))]
    [KnownType(typeof(DTOs.AccountTreesListItem))]
    public class ListAccountTreesResponse : ListResponse<DTOs.AccountTreesListItem>
    {
    }
}