using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Entities;
using System.Runtime.Serialization;

namespace Facturanet.Tdo
{
    public class AccountTreesListItem : Entities.Base.AccountTreeBase, Tdo.ITdo
    {
        public AccountTreesListItem()
        {
        }

        public AccountTreesListItem(Entities.AccountTree accountTree)
        {
            this.Active = accountTree.Active;
            this.Code = accountTree.Code;
            this.Description = accountTree.Description;
            this.Id = accountTree.Id;
            this.Name = accountTree.Name;
        }
    }
}
