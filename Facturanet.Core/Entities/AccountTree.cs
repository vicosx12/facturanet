using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Facturanet.Entities
{
    public class AccountTree : Entity, Lines.ILineAccountTree
    {
        public virtual string Code { get; set; }
        public virtual bool Active { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual Iesi.Collections.Generic.ISet<ContableAccount> Accounts { get; set; }
        
        public virtual void AddAccount(ContableAccount account)
        {
            account.AccountTree = this;
            if (!Accounts.Contains(account))
                Accounts.Add(account);
        }
    }
}
