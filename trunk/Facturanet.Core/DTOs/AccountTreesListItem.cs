using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Entities;
using System.Runtime.Serialization;

namespace Facturanet.DTOs
{
    public class AccountTreesListItem : Entities.Base.AccountTreeBase, DTOs.IDTO
    {
        public Entities.Base.AccountTreeBase CopyFromAccountTree
        {
            set
            {
                this.Active = value.Active;
                this.Code = value.Code;
                this.Description = value.Description;
                this.Id = value.Id;
                this.Name = value.Name;
            }
        }
    }
}
