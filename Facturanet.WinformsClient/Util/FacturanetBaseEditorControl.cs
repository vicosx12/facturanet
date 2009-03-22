using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Facturanet.WinformsClient.Util
{
    public interface IFacturanetEditorControl : IEditableObject
    {
        object EditableObject { get; set; }
        bool AutoEdit { get; set; }
        bool Visible { get; set; }
    }

    [DockingAttribute(DockingBehavior.Ask)]
    [ToolboxItem(false)]
    public abstract class FacturanetBaseEditorControl<T> : UserControl, IFacturanetEditorControl
        where T : class, IEditableObject
    {
        public FacturanetBaseEditorControl()
        {
            bindingSource = new BindingSource(typeof(T),"");
            EditableObject = null;
        }
        protected System.Windows.Forms.BindingSource bindingSource;

        private T editableObject = null;

        /// <summary>
        /// Sets the object that will be sohowed and can be edited
        /// </summary>
        public object EditableObject
        {
            get { return editableObject; }
            set 
            {
                if (editableObject != value)
                {
                    SetReadOnly(true);
                    editableObject = value as T;
                    if (editableObject == null)
                    {
                        bindingSource.DataSource = typeof(T);
                    }
                    else
                    {
                        bindingSource.DataSource = editableObject;
                        if (AutoEdit)
                            BeginEdit();
                    }
                }
            }
        }

        public T TypedEditableObject
        {
            get { return editableObject; }
            set { EditableObject = value; }
        }

        [DefaultValue(false)]
        public bool AutoEdit { get; set; }

        private void SetReadOnly(bool value)
        {
            this.Enabled = !value;
        }

        public void BeginEdit()
        {
            editableObject.BeginEdit();
            SetReadOnly(false);
        }

        public void CancelEdit()
        {
            SetReadOnly(true);
            editableObject.CancelEdit();
        }

        public void EndEdit()
        {
            SetReadOnly(true);
            editableObject.EndEdit();
        }
    }
}
