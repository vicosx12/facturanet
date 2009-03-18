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

        public override Validation.ValidationResult GetValidationResult(Validation.Level exceptionOverLevel)
        {
            var result = base.GetValidationResult(exceptionOverLevel);

            if (Code == null || Code == string.Empty)
                result.Add(exceptionOverLevel, "Code", Validation.Level.Error, "NULLEMPTY", "This field cant't be null or empty");
            if (Name == null || Name == string.Empty)
                result.Add(exceptionOverLevel, "Name", Validation.Level.Error, "NULLEMPTY", "This field cant't be null or empty");
            if (Description == null || Description == string.Empty)
                result.Add(exceptionOverLevel, "Description", Validation.Level.Error, "NULLEMPTY", "This field cant't be null or empty");
            
            return result;
        }
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
