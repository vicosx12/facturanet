using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Server;
using System.Runtime.Serialization;

namespace Facturanet.Business
{

    /// <summary>
    /// This request allow to update AccountTree header data and add, remove, edit and reorder their ContableAccounts
    /// </summary>
    [DataContract]
    public class UpdateCompleteAccountTreeRequest : BaseRequest<EmptyResponse>
    {
        [DataMember]
        public Guid AccountTreeId { get; set; }

        /// <summary>
        /// Set this property only if AccountTreeHeader has changes.
        /// </summary>
        [DataMember]
        public UI.AccountTreeListItem AccountTreeHeader { get; set; }

        [DataMember]
        public UI.ContableAccount[] UpdatedAccounts { get; set; }

        [DataMember]
        public UI.ContableAccount[] CreatedAccounts { get; set; }

        /*
        [DataMember]
        public UI.TreeOrderPair[] ReorderData { get; set; }
        */

        public UpdateCompleteAccountTreeRequest(
            Guid accountTreeId)
        {
            AccountTreeId = accountTreeId;
        }
        
        public override Facturanet.Validation.ValidationResult GetValidationResult(Facturanet.Validation.Level exceptionOverLevel)
        {
            var result = base.GetValidationResult(exceptionOverLevel);

            if (
                AccountTreeHeader == null
                && UpdatedAccounts.Length == 0
                && CreatedAccounts.Length == 0
                /*&& ReorderData.Length == 0*/)
            {
                result.Add(exceptionOverLevel, "{GENERAL}", Validation.Level.Info, "NOACTION", "This request will not do any action");
            }
            else
            {
                if (AccountTreeId == null)
                    result.Add(exceptionOverLevel, "AccountTreeId", Validation.Level.Error, "REQUIERED_VALUE", "AccountTreeId is requiered to do the action");

                if (AccountTreeHeader != null)
                    result.Add(exceptionOverLevel, "AccountTreeHeader", AccountTreeHeader.GetValidationResult(exceptionOverLevel));

                for (int i = 0; i < UpdatedAccounts.Length; i++)
                    result.Add(exceptionOverLevel, "UpdatedAccounts", i, UpdatedAccounts[i].GetValidationResult(exceptionOverLevel));

                for (int i = 0; i < CreatedAccounts.Length; i++)
                    result.Add(exceptionOverLevel, "CreatedAccounts", i, CreatedAccounts[i].GetValidationResult(exceptionOverLevel));
            }
            return result;
        }
    }
}
