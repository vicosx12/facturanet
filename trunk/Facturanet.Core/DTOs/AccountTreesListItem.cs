using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Facturanet.Entities;
using System.Runtime.Serialization;
using System.ComponentModel;


namespace Facturanet.DTOs
{
    public class AccountTreesListItem : Entities.Base.AccountTreeBase, IEditableDTO
    {
        public override bool Active
        {
            get { return editableData.GetData<bool>("Active"); }
            set { editableData.SetData("Active", value); }
        }

        public override string Description
        {
            get { return editableData.GetData<string>("Description"); }
            set { editableData.SetData("Description", value); }
        }

        public override string Code
        {
            get { return editableData.GetData<string>("Code"); }
            set { editableData.SetData("Code", value); }
        }

        public override Guid Id
        {
            get { return editableData.GetData<Guid>("Id"); }
            set { editableData.SetData("Id", value); }
        }

        public override string Name
        {
            get { return editableData.GetData<string>("Name"); }
            set { editableData.SetData("Name", value); }
        }

        #region PositionalToBeanResultTransformerGeneric Support

        public Entities.Base.AccountTreeBase CopyFromAccountTree
        {
            set
            {
                Active = value.Active;
                Code = value.Code;
                Description = value.Description;
                Id = value.Id;
                Name = value.Name;
            }
        }

        #endregion

        #region IEditableDTO Implementation

        private EditableSupporter editableData = new EditableSupporter();

        public event PropertyChangingEventHandler PropertyChanging
        {
            add { editableData.PropertyChanging += value; }
            remove { editableData.PropertyChanging -= value; }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { editableData.PropertyChanged += value; }
            remove { editableData.PropertyChanged -= value; }
        }

        [IgnoreDataMember]
        bool IDiscartableChanges.IDiscartableChangesActive
        {
            get { return editableData.IDiscartableChangesActive; }
            set { editableData.IDiscartableChangesActive = value; }
        }

        bool IDiscartableChanges.IsDirty
        {
            get { return editableData.IsDirty; }
        }

        void IDiscartableChanges.DiscardChanges()
        {
            editableData.DiscardChanges();
        }

        void IDiscartableChanges.AcceptChanges()
        {
            editableData.AcceptChanges();
        }

        void IEditableObject.BeginEdit()
        {
            editableData.BeginEdit();
        }

        void IEditableObject.CancelEdit()
        {
            editableData.CancelEdit();
        }

        void IEditableObject.EndEdit()
        {
            editableData.EndEdit();
        }

        #endregion
    }
}
