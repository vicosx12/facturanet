using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facturanet.Server;
using System.Runtime.Serialization;

namespace Facturanet.Business
{

    /// <summary>
    /// This request allow to delete AccountTrees, Insert new empties AccountTrees and update base data of AccountTrees
    /// </summary>
    [DataContract]
    public class UpdateAccountTreeListRequest : BaseRequest<EmptyResponse>
    {
        [DataMember]
        public Guid[] DeletedIds { get; set; }

        [DataMember]
        public UI.AccountTreesListItem[] UpdatedItems { get; set; }

        [DataMember]
        public UI.AccountTreesListItem[] CreatedItems { get; set; }

        public UpdateAccountTreeListRequest(UI.AccountTreesListItem[] createdItems, UI.AccountTreesListItem[] updatedItems, Guid[] deletedIds)
        {
            CreatedItems = createdItems;
            UpdatedItems = updatedItems;
            DeletedIds = deletedIds;
        }

        public UpdateAccountTreeListRequest(
            IEnumerable<UI.AccountTreesListItem> createdItems, 
            IEnumerable<UI.AccountTreesListItem> updatedItems, 
            IEnumerable<UI.AccountTreesListItem> deletedItems)
            : this(
                createdItems.ToArray(),
                updatedItems.ToArray(),
                (from item in deletedItems 
                 select item.Id).ToArray())
        {

        }
        
        public override Facturanet.Validation.ValidationResult GetValidationResult(Facturanet.Validation.Level exceptionOverLevel)
        {
            var result = base.GetValidationResult(exceptionOverLevel);
            for (int i = 0; i < CreatedItems.Count(); i++)
                result.Add(exceptionOverLevel, "CreatedItems", i, CreatedItems[i].GetValidationResult(exceptionOverLevel));
            for (int i = 0; i < UpdatedItems.Count(); i++)
                result.Add(exceptionOverLevel, "UpdatedItems", i, UpdatedItems[i].GetValidationResult(exceptionOverLevel));
            return result;
        }
        
    }
}
