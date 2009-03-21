using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facturanet.Util;
using Facturanet.UI;

namespace Facturanet.WinformsClient.Controls
{
    public class AccountTreeListItemTreenode : Util.FacturanetBaseTreenode<AccountTreesListItem>
    {
        public AccountTreeListItemTreenode(AccountTreesListItem asociatedObject)
            : base(asociatedObject)
        {
        }

        public AccountTreeListItemTreenode()
            : base()
        {
        }

        protected override string GetNodeName()
        {
            return AsociatedObject.Id.ToString();
        }

        protected override string GetNodeText()
        {
            return string.Format(
                "{0} - {1}",
                AsociatedObject.Code,
                AsociatedObject.Name);
        }

        protected override string GetNodeToolTipText()
        {
            return AsociatedObject.Description;
        }
    }
}
