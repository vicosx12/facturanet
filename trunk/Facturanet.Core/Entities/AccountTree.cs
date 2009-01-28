using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;


namespace Facturanet.Entities
{
    public class AccountTree : Entity
    {
        public virtual string Code { get; set; }
        public virtual bool Active { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual IList<ContableAccount> Accounts { get; set; }

        public void AddAccount(ContableAccount account)
        {
            account.AccountTree = this;
            if (!Accounts.Contains(account))
                Accounts.Add(account);
        }

    }
}
