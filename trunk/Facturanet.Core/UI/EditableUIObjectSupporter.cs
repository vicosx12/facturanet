using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Facturanet.UI
{
    internal class EditableUIObjectSupporter : IEditableUIObjectSupporter
    {
        private Hashtable data = new Hashtable();

        /// <summary>
        /// Gets the value of a property. If the property is not defined returns the default value of the type.
        /// </summary>
        public T GetData<T>(string propertyName)
        {
            if (!data.Contains(propertyName))
                return default(T);
            else
                return (T)data[propertyName];
        }

        /// <summary>
        /// Sets the value of a property. If the property exists override the previous value.
        /// </summary>
        /// <remarks>
        /// Raises PropertyChangingEvent, PropertyChangedEvent and if DiscartableChangesControl is 
        /// enabled mantains a backup.
        /// </remarks>
        public void SetData<T>(string propertyName, T value)
        {
            if (
                (value == null && GetData<T>(propertyName) != null)
                || (value != null && !value.Equals(GetData<T>(propertyName)))
                )
            {
                OnPropertyChanging(new PropertyChangingEventArgs(propertyName));

                if (DiscartableChangesControl && !IsDirty)
                    backupDiscardChanges = ((IBackupable)this).Backup();

                if (value == null || value.Equals(default(T)))
                    data.Remove(propertyName);
                else
                    data[propertyName] = value;
                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }

        #region IBackupable Implementation

        /// <summary>
        /// Returns a object with the backup of current data.
        /// </summary>
        public object Backup()
        {
            return data.Clone();
        }

        /// <summary>
        /// Replace current data with the backup data.
        /// </summary>
        public void Restore(object backupData)
        {
            Hashtable backup = (Hashtable)backupData;
            data = (Hashtable)backup.Clone();
        }

        /// <summary>
        /// Returns the diferences between current and backupData.
        /// </summary>
        public ValueChangedDescriptorCollection GetDifferences(object backupData)
        {
            Hashtable backup = (Hashtable)backupData;
            ValueChangedDescriptorCollection changes = new ValueChangedDescriptorCollection();

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

        /// <summary>
        /// Raises when property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Raises the <see cref="E:PropertyChanging"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangingEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            if (PropertyChanging != null)
                PropertyChanging(this, e);
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        /// <summary>
        /// Raises when property value is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion

        #region IDiscartableChanges Implementation

        private object backupDiscardChanges = null;

        /// <summary>
        /// Gets or sets a value indicating whether the object allow discard changes manually.
        /// </summary>
        public bool DiscartableChangesControl { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has changes.
        /// </summary>
        /// <remarks>
        /// To use this, DiscartableChangesControl has to be true.
        /// </remarks>
        public bool IsDirty
        {
            get
            {
                if (!DiscartableChangesControl)
                    throw new ApplicationException("Discartable Changes Control is not active.");
                else
                    return backupDiscardChanges != null;
            }
        }

        /// <summary>
        /// Discards the changes.
        /// </summary>
        /// <remarks>
        /// To use this, DiscartableChangesControl has to be true.
        /// </remarks>
        public void DiscardChanges()
        {
            if (!DiscartableChangesControl)
                throw new ApplicationException("Discartable Changes Control is not active.");
            else
            {
                OnPropertyChanging(new PropertyChangingEventArgs(""));
                Restore(backupDiscardChanges);
                backupDiscardChanges = null;
                OnPropertyChanged(new PropertyChangedEventArgs(""));
            }
        }

        /// <summary>
        /// Accepts the changes.
        /// </summary>
        /// <remarks>
        /// To use this, DiscartableChangesControl has to be true.
        /// </remarks>
        public void AcceptChanges()
        {
            if (!DiscartableChangesControl)
                throw new ApplicationException("Discartable Changes Control is not active.");
            else
            {
                backupDiscardChanges = null;
            }
        }

        /// <summary>
        /// Gets the changes respect the original version.
        /// </summary>
        /// <remarks>
        /// To use this, DiscartableChangesControl has to be true.
        /// </remarks>
        public ValueChangedDescriptorCollection GetChanges()
        {
            if (!DiscartableChangesControl)
                throw new ApplicationException("Discartable Changes Control is not active.");
            else
                return GetDifferences(backupDiscardChanges);
        }
        
        #endregion
        
        #region IEditableObject Implementation

        private object backupIEditable = null;

        /// <summary>
        /// Starts the object edition.
        /// </summary>
        public void BeginEdit()
        {
            if (backupIEditable == null)
                backupIEditable = Backup();
        }


        /// <summary>
        /// Cancel the changes maked from the last call to <see cref="M:System.ComponentModel.IEditableObject.BeginEdit"/>.
        /// </summary>
        public void CancelEdit()
        {
            if (backupIEditable != null)
            {
                Restore(backupIEditable);
                backupIEditable = null;
            }
        }

        /// <summary>
        /// Acepts the changes maked.
        /// </summary>
        public void EndEdit()
        {
            if (backupIEditable != null)
                backupIEditable = null;
        }

        #endregion
    }
}
