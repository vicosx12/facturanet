/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Facturanet.WinformsClient
{
    public class FindStrip : ToolStrip
    {
      private BindingSource bindingSource;
      public BindingSource BindingSource {
        get { return bindingSource; }
        set 
        { 
            bindingSource = value;
            
            //searchInToolStripComboBox.Items.Clear();
            if (bindingSource != null)
            {
                // Add column names to Search In list
                PropertyDescriptorCollection properties =
                  ((ITypedList)bindingSource).GetItemProperties(null);
                foreach (PropertyDescriptor property in properties)
                {
                    this.searchInToolStripComboBox.Items.Insert(0, property.Name);
                }

                // Select first column name in list, if column names were added
                if (this.searchInToolStripComboBox.Items.Count > 0)
                {
                    this.searchInToolStripComboBox.SelectedIndex = 0;
                }
            }
        }
      }

    }
}
*/