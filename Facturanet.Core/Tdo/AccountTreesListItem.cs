using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Entities;

namespace Facturanet.Tdo
{
    public class AccountTreesListItem : Facturanet.Entities.Entity, Lines.ILineAccountTree
    {
        public virtual string Code { get; set; }
        public virtual bool Active { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public AccountTreesListItem() : base()
        {
        }

        public AccountTreesListItem(
            Guid id,
            string code,
            bool active,
            string name,
            string description)
        {
            Id = id;
            Code = code;
            Active = active;
            Name = name;
            Description = description;
        }

        public AccountTreesListItem(Entities.AccountTree accountTree) : 
            this(accountTree.Id, accountTree.Code, accountTree.Active, accountTree.Name, accountTree.Description)
        {
        }
    }
}
