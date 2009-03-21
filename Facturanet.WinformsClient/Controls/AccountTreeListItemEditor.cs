using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Facturanet.WinformsClient.Controls
{
    [ToolboxItem(true)]
    public partial class AccountTreeListItemEditor : AccountTreeListItemEditorBase
    {
        public AccountTreeListItemEditor()
        {
            InitializeComponent();
        }
    }

    public class AccountTreeListItemEditorBase : Util.FacturanetBaseEditorControl<UI.AccountTreesListItem>
    {
    }

}
