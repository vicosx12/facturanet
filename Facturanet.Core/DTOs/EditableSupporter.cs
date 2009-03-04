using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Facturanet.DTOs
{
    internal class EditableSupporter : ICloneable, IBackupable, INotifyPropertyChanging, INotifyPropertyChanged, IDiscartableChanges
    {
        private Hashtable data = new Hashtable();

        public T GetData<T>(string propertyName)
        {
            if (!data.Contains(propertyName))
                return default(T);
            else
                return (T)data[propertyName];
        }

        public void SetData<T>(string propertyName, T value)
        {
            if (
                (value == null && GetData<T>(propertyName) != null)
                || (value != null && !value.Equals(GetData<T>(propertyName)))
                )
            {
                OnPropertyChanging(new PropertyChangingEventArgs(propertyName));

                if (IDiscartableChangesActive && !IsDirty)
                    backupDiscardChanges = ((IBackupable)this).Backup();

                if (value == null || value.Equals(default(T)))
                    data.Remove(propertyName);
                else
                    data[propertyName] = value;
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        #region ICloneable Implementation

        public object Clone()
        {
            EditableSupporter clon = new EditableSupporter();
            clon.Restore(data);
            return clon;
        }

        #endregion

        #region IBackupable Implementation

        public object Backup()
        {
            return data.Clone();
        }

        public void Restore(object backupData)
        {
            Hashtable backup = (Hashtable)backupData;
            data = (Hashtable)backup.Clone();
        }

        public ValueChangedCollection GetDifferences(object backupData)
        {
            Hashtable backup = (Hashtable)backupData;
            ValueChangedCollection changes = new ValueChangedCollection();

            foreach (string key in data.Keys)
            {
                if (!backup.ContainsKey(key))
                    changes.Add(key, null, data[key]);
                else if (!backup[key].Equals(data[key]))
                    changes.Add(key, backup[key], data[key]);
            }

            foreach (string key in backup.Keys)
                if (!data.ContainsKey(key))
                    changes.Add(key, backup[key], null);

            return changes;
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

        #region IDiscartableChanges Implementation

        private object backupDiscardChanges = null;

        public bool IDiscartableChangesActive { get; set; }

        public bool IsDirty
        {
            get
            {
                if (!IDiscartableChangesActive)
                    throw new ApplicationException("Discartable Changes Monitoring is not active.");
                else
                    return backupDiscardChanges != null;
            }
        }

        public void DiscardChanges()
        {
            if (!IDiscartableChangesActive)
                throw new ApplicationException("Discartable Changes Monitoring is not active.");
            else
            {
                OnPropertyChanging(new PropertyChangingEventArgs(""));
                Restore(backupDiscardChanges);
                backupDiscardChanges = null;
                OnPropertyChanged(new PropertyChangedEventArgs(""));
            }
        }

        public void AcceptChanges()
        {
            if (!IDiscartableChangesActive)
                throw new ApplicationException("Discartable Changes Monitoring is not active.");
            else
            {
                backupDiscardChanges = null;
            }
        }

        public ValueChangedCollection GetChanges()
        {
            if (!IDiscartableChangesActive)
                throw new ApplicationException("Discartable Changes Monitoring is not active.");
            else
                return GetDifferences(backupDiscardChanges);
        }
        
        #endregion

        #region IEditableObject Implementation

        private object backupIEditable = null;

        public void BeginEdit()
        {
            if (backupIEditable == null)
                backupIEditable = Backup();
        }

        public void CancelEdit()
        {
            if (backupIEditable != null)
            {
                Restore(backupIEditable);
                backupIEditable = null;
            }
        }

        public void EndEdit()
        {
            if (backupIEditable != null)
                backupIEditable = null;
        }

        #endregion
    }
}
