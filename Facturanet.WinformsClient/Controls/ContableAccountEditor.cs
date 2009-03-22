using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Facturanet.WinformsClient.Controls
{
    [ToolboxItem(true)]
    public partial class ContableAccountEditor : ContableAccountEditorBase
    {
        public ContableAccountEditor()
        {
            InitializeComponent();
        }
    }

    public class ContableAccountEditorBase : Util.FacturanetBaseEditorControl<UI.ContableAccount>
    {
    }
}
