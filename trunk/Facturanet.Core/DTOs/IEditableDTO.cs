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
        //IBackupable,
        IDiscartableChanges,
        INotifyPropertyChanged, 
        INotifyPropertyChanging, 
        //ICloneable,
        IEditableObject
        
    {
    }

    public interface IBackupable
    {
        object Backup();
        void Restore(object backupData);
        bool HasDifferences(object backupData);
    }

    public interface IDiscartableChanges
    {
        bool IDiscartableChangesActive { get; set; }
        bool IsDirty { get; }
        void DiscardChanges();
        void AcceptChanges();
    }
}
