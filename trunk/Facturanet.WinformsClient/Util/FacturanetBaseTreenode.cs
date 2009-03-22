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
    public interface IFacturanetTreenode
    {
        object AsociatedObject { get; }
        void RefreshData();
    }

    public abstract class FacturanetBaseTreenode<T> : TreeNode, IFacturanetTreenode
        where T : class
    {
        private bool typeIsUIObject;
        private bool typeIsEditable;
        private bool typeIsDeletable;

        private T asociatedObject = null;
        public object AsociatedObject
        {
            get { return asociatedObject; }
        }

        public T TypedAsociatedObject
        {
            get { return asociatedObject; }
        }

        public FacturanetBaseTreenode(T asociatedObject)
            : base()
        {
            Type type = typeof(T);
            typeIsUIObject = type.ImplementsInterface<UI.IUIObject>();
            typeIsEditable = type.ImplementsInterface<UI.IEditableUIObject>();
            typeIsDeletable = type.ImplementsInterface<UI.IDeletableUIObject>();
            this.asociatedObject = asociatedObject;

            if (typeIsEditable)
            {
                var editable = asociatedObject as UI.IEditableUIObject;
                editable.PropertyChanged += new PropertyChangedEventHandler(editable_PropertyChanged);
            }

            RefreshData();
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
