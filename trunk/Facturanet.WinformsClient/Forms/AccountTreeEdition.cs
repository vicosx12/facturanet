using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Facturanet.WinformsClient.Forms
{
    public partial class AccountTreeEdition : Form
    {
        public AccountTreeEdition()
        {
            InitializeComponent();
        }

        public AccountTreeEdition(Guid accountTreeId)
            : this()
        {
        }
    }
}
