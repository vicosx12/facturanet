using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Facturanet.Entities.Base
{
    public abstract class AccountTreeBase : Entity
    {
        public virtual string Code { get; set; }
        public virtual bool Active { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}

namespace Facturanet.Entities
{
    public class AccountTree : Base.AccountTreeBase
    {
        public virtual Iesi.Collections.Generic.ISet<ContableAccount> Accounts { get; set; }
        
        public virtual void AddAccount(ContableAccount account)
        {
            account.AccountTree = this;
            if (!Accounts.Contains(account))
                Accounts.Add(account);
        }
    }
}
