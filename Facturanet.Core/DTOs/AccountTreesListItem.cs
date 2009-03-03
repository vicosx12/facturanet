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
        #region AccountTree Properties
        public override bool Active
        {
            get { return GetData<bool>("Active"); }
            set { SetData("Active", value); }
        }

        public override string Description
        {
            get { return GetData<string>("Description"); }
            set { SetData("Description", value); }
        }

        public override string Code
        {
            get { return GetData<string>("Code"); }
            set { SetData("Code", value); }
        }

        public override Guid Id
        {
            get { return GetData<Guid>("Id"); }
            set { SetData("Id", value); }
        }

        public override string Name
        {
            get { return GetData<string>("Name"); }
            set { SetData("Name", value); }
        }
        #endregion

        #region PositionalToBeanResultTransformerGeneric Support

        public Entities.Base.AccountTreeBase CopyFromAccountTree
        {
            set
            {
                this.Active = value.Active;
                this.Code = value.Code;
                this.Description = value.Description;
                this.Id = value.Id;
                this.Name = value.Name;
            }
        }

        #endregion

        #region IBackupable Implementation

        object IBackupable.Backup()
        {
            return data.Clone();
        }

        void IBackupable.Restore(object backupData)
        {
            Hashtable backup = (Hashtable)backupData;
            data = (Hashtable)backup.Clone();
        }

        bool IBackupable.HasDifferences(object backupData)
        {
            Hashtable backup = (Hashtable)backupData;
            if (backup.Count != data.Count)
                return true;

            foreach (object key in data.Keys)
            {
                if (!backup.Contains(key))
                    return true;
                else if (!backup[key].Equals(data[key]))
                    return true;
            }

            return false;
        }

        #endregion

        #region Set / Get Properties Support

        private Hashtable data = new Hashtable();

        private T GetData<T>(string propertyName)
        {
            if (!data.Contains(propertyName))
                return default(T);
            else
                return (T)data[propertyName];
        }
        private void SetData<T>(string propertyName, T value)
        {
            if (!value.Equals(GetData<T>(propertyName)))
            {
                OnPropertyChanging(new PropertyChangingEventArgs(propertyName));

                if (((IDiscartableChanges)this).IDiscartableChangesActive && !((IDiscartableChanges)this).IsDirty)
                    backupDiscardChanges = ((IBackupable)this).Backup();

                if (value.Equals(default(T)))
                    data.Remove(propertyName);
                else
                    data[propertyName] = value;
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IDiscartableChanges Implementation

        private object backupDiscardChanges = null;

        [IgnoreDataMember]
        bool IDiscartableChanges.IDiscartableChangesActive { get; set; }

        bool IDiscartableChanges.IsDirty 
        {
            get
            {
                if (!((IDiscartableChanges)this).IDiscartableChangesActive)
                    throw new ApplicationException("IDisctarbleChangesActive is false");
                else
                    return backupDiscardChanges != null;
            }
        }

        void IDiscartableChanges.DiscardChanges()
        {
            OnPropertyChanging(new PropertyChangingEventArgs(""));
            ((IBackupable)this).Restore(backupDiscardChanges);
            backupDiscardChanges = null;
            OnPropertyChanged(new PropertyChangedEventArgs(""));
        }

        void IDiscartableChanges.AcceptChanges()
        {
            backupDiscardChanges = null;
        }
        
        #endregion

        #region ICloneable Implementation
        object ICloneable.Clone()
        {
            AccountTreesListItem clon = new AccountTreesListItem();
            ((IBackupable)clon).Restore(data);
            return clon;
        }
        #endregion

        #region IEditableObject Implementation
        private object backupIEditable = null;

        void IEditableObject.BeginEdit() 
        {
            if (backupIEditable == null)
                backupIEditable = ((IBackupable)this).Backup();
        }

        void IEditableObject.CancelEdit() 
        {
            if (backupIEditable != null)
            {
                ((IBackupable)this).Restore(backupIEditable);
                backupIEditable = null;
            }
        }

        void IEditableObject.EndEdit() 
        {
            if (backupIEditable != null) 
                backupIEditable = null;
        }
        #endregion

        #region INotifyPropertyChanging Implementation
        
        public event PropertyChangingEventHandler PropertyChanging;

        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, e);
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion

        
    }
}
