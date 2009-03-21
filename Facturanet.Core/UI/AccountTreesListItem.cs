﻿using System;
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
        IDeletableUIObject
    {

        #region Constructors
        public AccountTreesListItem()
        {
            EditableData.SetSupportedObject(this);
        }

        public AccountTreesListItem(Guid id)
            : this()
        {
            Id = id;
        }
        #endregion

        #region Editable properties
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

        public override string Name
        {
            get { return EditableData.GetData<string>("Name"); }
            set { EditableData.SetData("Name", value); }
        }

        public override Guid? DeletedMark
        {
            get { return EditableData.GetData<Guid?>("DeletedMark"); }
            set { EditableData.SetData("DeletedMark", value); }
        }

        #endregion

        #region IEditableUIObject Implementation

        private readonly IEditableUIObjectSupporter EditableData = new EditableUIObjectSupporter();

        public bool IsNew()
        {
            return Version == 0;
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

        public bool IsDirty()
        {
            return EditableData.IsDirty();
        }

        public bool IsDirty(bool byValues)
        {
            return EditableData.IsDirty(byValues);
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
