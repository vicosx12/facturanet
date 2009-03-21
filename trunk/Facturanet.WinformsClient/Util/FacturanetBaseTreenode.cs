using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facturanet.Util;

namespace Facturanet.WinformsClient.Util
{
    public abstract class FacturanetBaseTreenode<T> : TreeNode
        where T : class, new()
    {
        private bool typeIsUIObject;
        private bool typeIsEditable;
        private bool typeIsDeletable;

        public T AsociatedObject { get; private set; }


        public FacturanetBaseTreenode(T asociatedObject)
            : base()
        {
            Type type = typeof(T);
            typeIsUIObject = type.ImplementsInterface<UI.IUIObject>();
            typeIsEditable = type.ImplementsInterface<UI.IEditableUIObject>();
            typeIsDeletable = type.ImplementsInterface<UI.IDeletableUIObject>();
            AsociatedObject = asociatedObject;

            if (typeIsEditable)
            {
                var editable = AsociatedObject as UI.IEditableUIObject;
                editable.PropertyChanged += new PropertyChangedEventHandler(editable_PropertyChanged);
            }

            RefreshData();
        }

        public FacturanetBaseTreenode()
            : this(new T())
        {
        }

        void editable_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            Name = GetNodeName();
            Text = GetNodeText();
            ToolTipText = GetNodeToolTipText();
        }

        protected abstract string GetNodeName();
        protected abstract string GetNodeText();
        protected abstract string GetNodeToolTipText();
    }
}
