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
    public class AccountTreeListItemTreenode : Util.FacturanetBaseTreenode<AccountTreeListItem>
    {
        public AccountTreeListItemTreenode(AccountTreeListItem asociatedObject)
            : base(asociatedObject)
        {
        }

        protected override string GetNodeName()
        {
            return TypedAsociatedObject.Id.ToString();
        }

        protected override string GetNodeText()
        {
            return string.Format(
                "{0} - {1}",
                TypedAsociatedObject.Code,
                TypedAsociatedObject.Name);
        }

        protected override string GetNodeToolTipText()
        {
            return TypedAsociatedObject.Description;
        }
    }
}
