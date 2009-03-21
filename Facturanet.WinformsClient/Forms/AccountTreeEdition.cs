using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facturanet.UI;
using Facturanet.Validation;
using Facturanet.WinformsClient.Controls;

namespace Facturanet.WinformsClient.Forms
{
    public partial class AccountTreeEdition : Form
    {
        public AccountTreeEdition()
        {
            InitializeComponent();
        }

        private void AddChilds(TreeNode nodo, IEnumerable<ContableAccount> subaccounts)
        {
            /*
            foreach (var sub in subaccounts)
            {
                TreeNode subNode = new TreeNode(string.Format("{0} - {1}", sub.Code, sub.Name));
                subNode.ToolTipText = sub.Description;
                subNode.Tag = sub;
                sub.DiscartableChangesControl = true;
                AddChilds(subNode, sub.Subaccounts);
                nodo.Nodes.Add(subNode);
            }
             * */
        }

        public AccountTreeEdition(Guid accountTreeId)
            : this()
        {
            //hacer un treenode especializado
            var request = new Business.GetCompleteAccountTreeRequest();
            request.AccountTreeId = accountTreeId;
            var response = request.Run();

            AccountTreeListItemTreenode root = new AccountTreeListItemTreenode(response.AccountTreeHeader);
            treeView1.Nodes.Add(root);
            /*
            TreeNode root = treeView1.Nodes.Add(
                response.AccountTreeHeader.Id.ToString(),
                string.Format("{0} - {1}", response.AccountTreeHeader.Code, response.AccountTreeHeader.Name));
            root.ToolTipText = response.AccountTreeHeader.Description;
            root.Tag = response.AccountTreeHeader;
             
            response.AccountTreeHeader.DiscartableChangesControl = true;
             * * */
            /*
            AddChilds(root, response.Items);
            
            //accountTreeListItemEditior1.EditableObject = response.AccountTreeHeader;
             */


            this.treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AccountTreeListItemTreenode accountTreeListItemTreenode = e.Node as AccountTreeListItemTreenode;
            if (accountTreeListItemTreenode != null)
            {
                accountTreeListItemEditor1.EditableObject = accountTreeListItemTreenode.AsociatedObject;
                accountTreeListItemEditor1.Visible = true;
            }
            else
            {
                accountTreeListItemEditor1.Visible = false;
                accountTreeListItemEditor1.EditableObject = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (accountTreeListItemEditor1.Visible)
                accountTreeListItemEditor1.BeginEdit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (accountTreeListItemEditor1.Visible)
                accountTreeListItemEditor1.EndEdit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (accountTreeListItemEditor1.Visible)
                accountTreeListItemEditor1.CancelEdit();
        }
    }
}
