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

        private AccountTreeListItemTreenode GenerateTree(AccountTreeListItem tree, IEnumerable<ContableAccount> accounts)
        {
            AccountTreeListItemTreenode root = new AccountTreeListItemTreenode(tree);

            Dictionary<Guid, ContableAccountTreenode> aux = new Dictionary<Guid, ContableAccountTreenode>();
            
            foreach (ContableAccount account in accounts)
                aux.Add(
                    account.Id,
                    new ContableAccountTreenode(account));

            foreach (var auxItem in aux)
            {
                var accountNode = auxItem.Value;
                Guid? parentId = accountNode.TypedAsociatedObject.ParentAccountId;
                if (parentId.HasValue)
                {
                    var parentNode = aux[parentId.Value];
                    parentNode.Nodes.Add(accountNode);
                }
                else
                    root.Nodes.Add(auxItem.Value);
            }

            return root;
        }

        public AccountTreeEdition(Guid accountTreeId)
            : this()
        {
            var request = new Business.GetCompleteAccountTreeRequest();
            request.AccountTreeId = accountTreeId;
            var response = request.Run();

            this.treeView1.Nodes.Add(GenerateTree(response.AccountTreeHeader, response.Items));

            this.treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            accountTreeListItemEditor1.Visible = false;
            contableAccountEditor1.Visible = false;

            var editor = GetNodeEditor(e.Node);
            var node = e.Node as Util.IFacturanetTreenode;

            editor.EditableObject = node.AsociatedObject;
            editor.Visible = true;
        }

        private Util.IFacturanetEditorControl GetNodeEditor(TreeNode node)
        {
            if (node == null)
                return null;
            if (node is AccountTreeListItemTreenode)
                return accountTreeListItemEditor1;
            else if (node is ContableAccountTreenode)
                return contableAccountEditor1;
            else
                return null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var editor = GetNodeEditor(treeView1.SelectedNode);

            if (editor != null)
                editor.BeginEdit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var editor = GetNodeEditor(treeView1.SelectedNode);
            if (editor != null)
                editor.EndEdit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var editor = GetNodeEditor(treeView1.SelectedNode);
            if (editor != null)
                editor.CancelEdit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var parent = treeView1.SelectedNode;
            var account = new ContableAccount();
            var node = new ContableAccountTreenode(account);
            parent.Nodes.Add(node);
            treeView1.SelectedNode = node;
        }
    }
}
