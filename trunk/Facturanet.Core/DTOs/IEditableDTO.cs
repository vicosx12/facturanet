using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Reflection;


namespace Facturanet.DTOs
{
    public interface IEditableDTO : 
        IDTO, 
        IDiscartableChanges,
        INotifyPropertyChanged, 
        INotifyPropertyChanging, 
        IEditableObject
    {
    }

    public interface IBackupable
    {
        object Backup();
        void Restore(object backupData);
        ValueChangedCollection GetDifferences(object backupData);
    }

    public interface IDiscartableChanges
    {
        bool IDiscartableChangesActive { get; set; }
        bool IsDirty { get; }
        ValueChangedCollection GetChanges();
        void DiscardChanges();
        void AcceptChanges();
    }
}
