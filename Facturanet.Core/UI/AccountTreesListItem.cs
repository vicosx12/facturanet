using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Facturanet.Entities;
using System.Runtime.Serialization;
using System.ComponentModel;


namespace Facturanet.UI
{
    public class AccountTreesListItem : 
        Entities.Base.AccountTreeBase, 
        IEditableUIObject,
        ICreableUIObject,
        IDeletableUIObject
    {
        public override bool Active
        {
            get { return EditableData.GetData<bool>("Active"); }
            set { EditableData.SetData("Active", value); }
        }

        public override string Description
        {
            get { return EditableData.GetData<string>("Description"); }
            set { EditableData.SetData("Description", value); }
        }

        public override string Code
        {
            get { return EditableData.GetData<string>("Code"); }
            set { EditableData.SetData("Code", value); }
        }

        public override Guid Id
        {
            get { return EditableData.GetData<Guid>("Id"); }
            set { EditableData.SetData("Id", value); }
        }

        public override string Name
        {
            get { return EditableData.GetData<string>("Name"); }
            set { EditableData.SetData("Name", value); }
        }

        public AccountTreesListItem()
        {
            IsNew = true;
            Active = true;
            IsDeleted = false;
        }

        #region IEditableUIObject Implementation

        public bool IsDeleted { set; get; }

        public bool IsNew { set; get; }

        private IEditableUIObjectSupporter _editableData;
        private IEditableUIObjectSupporter EditableData
        {
            get
            {
                if (_editableData == null)
                    _editableData = new EditableUIObjectSupporter(this);
                return _editableData;
            }
        }

        public event PropertyChangingEventHandler PropertyChanging
        {
            add { EditableData.PropertyChanging += value; }
            remove { EditableData.PropertyChanging -= value; }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { EditableData.PropertyChanged += value; }
            remove { EditableData.PropertyChanged -= value; }
        }

        [IgnoreDataMember]
        public bool DiscartableChangesControl
        {
            get { return EditableData.DiscartableChangesControl; }
            set { EditableData.DiscartableChangesControl = value; }
        }

        public bool IsDirty
        {
            get { return EditableData.IsDirty; }
        }

        public void DiscardChanges()
        {
            EditableData.DiscardChanges();
        }

        public void AcceptChanges()
        {
            EditableData.AcceptChanges();
        }

        public ValueChangedDescriptorCollection GetChanges()
        {
            return EditableData.GetChanges();
        }

        void IEditableObject.BeginEdit()
        {
            EditableData.BeginEdit();
        }

        void IEditableObject.CancelEdit()
        {
            EditableData.CancelEdit();
        }

        void IEditableObject.EndEdit()
        {
            EditableData.EndEdit();
        }

        #endregion
    }
}
