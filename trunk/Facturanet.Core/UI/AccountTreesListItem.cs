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
    public class AccountTreesListItem : Entities.Base.AccountTreeBase, IEditableUIObject
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


        #region IEditableUIObject Implementation

        private IEditableUIObjectSupporter editableData = new EditableUIObjectSupporter();

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
        public bool DiscartableChangesControl
        {
            get { return editableData.DiscartableChangesControl; }
            set { editableData.DiscartableChangesControl = value; }
        }

        public bool IsDirty
        {
            get { return editableData.IsDirty; }
        }

        public void DiscardChanges()
        {
            editableData.DiscardChanges();
        }

        public void AcceptChanges()
        {
            editableData.AcceptChanges();
        }

        public ValueChangedDescriptorCollection GetChanges()
        {
            return editableData.GetChanges();
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
