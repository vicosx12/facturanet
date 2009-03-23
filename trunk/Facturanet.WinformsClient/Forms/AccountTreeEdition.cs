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
        AccountTreeListItem accountTreeHeader = null;
        AccountTreeListItemTreenode rootNode = null;

        public AccountTreeEdition()
        {
            InitializeComponent();
        }

        public AccountTreeEdition(Guid accountTreeId)
            : this()
        {
            RefreshTree(accountTreeId);
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

        private TreeNode previousNode = null;

        private enum Mode
        {
            Browsing,
            Editing,
            Creating
        }
        private Mode mode;
        private void SetMode(Mode newMode)
        {
            if (newMode != mode)
            {
                switch (newMode)
                {
                    case Mode.Browsing :
                        treeView1.Enabled = true;
                        btnNewChild.Enabled = true;
                        btnNewBrother.Enabled = true;
                        btnBeginEdit.Enabled = true;
                        btnEndEdit.Enabled = false;
                        btnCancelEdit.Enabled = false;
                        break;
                    case Mode.Editing :
                    case Mode.Creating :
                        treeView1.Enabled = false;
                        btnNewChild.Enabled = false;
                        btnNewBrother.Enabled = false;
                        btnBeginEdit.Enabled = false;
                        btnEndEdit.Enabled = true;
                        btnCancelEdit.Enabled = true;
                        break;
                }
                mode = newMode;
            }
        }


        private void BeginEdit()
        {
            var editor = GetNodeEditor(treeView1.SelectedNode);
            if (editor != null)
            {
                SetMode(Mode.Editing);
                editor.BeginEdit();
            }
        }

        private void EndEdit()
        {
            var editor = GetNodeEditor(treeView1.SelectedNode);
            if (editor != null)
            {
                editor.EndEdit();
                SetMode(Mode.Browsing);
            }
        }

        private void CancelEdit()
        {
            var editor = GetNodeEditor(treeView1.SelectedNode);
            if (editor != null)
            {
                editor.CancelEdit();
                if (mode == Mode.Creating)
                {
                    var node = treeView1.SelectedNode;
                    treeView1.SelectedNode = previousNode;
                    node.Remove();
                }
                SetMode(Mode.Browsing);
            }
        }

        private void BeginNewAccount(bool child)
        {
            if (treeView1.SelectedNode != null)
            {
                var parentNode = child 
                    ? (Util.IFacturanetTreenode)treeView1.SelectedNode
                    : (Util.IFacturanetTreenode)treeView1.SelectedNode.Parent;
                if (parentNode != null)
                {
                    var account = new ContableAccount();
                    var node = new ContableAccountTreenode(account);

                    //TODO: Esto tal vez podría ser código del ContableAccountTreenode junto con el que pone el padre con el dragandrop
                    var parent = parentNode.AsociatedObject as ContableAccount;
                    account.ParentAccountId = parent == null
                        ? null
                        : (Guid?)parent.Id;

                    parentNode.Nodes.Add(node);
                    treeView1.SelectedNode = node;
                    var editor = GetNodeEditor(treeView1.SelectedNode);
                    SetMode(Mode.Creating);
                    editor.BeginEdit();
                }
            }
        }

        private AccountTreeListItemTreenode GenerateTree(AccountTreeListItem accountTreeHeader, IEnumerable<ContableAccount> accounts)
        {
            AccountTreeListItemTreenode rootNode = new AccountTreeListItemTreenode(accountTreeHeader);

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
                    rootNode.Nodes.Add(auxItem.Value);
            }

            return rootNode;
        }

        private void RefreshTree()
        {
            RefreshTree(accountTreeHeader.Id);
        }
        
        private void RefreshTree(Guid id)
        {
            treeView1.Nodes.Clear();
            var request = new Business.GetCompleteAccountTreeRequest();
            request.AccountTreeId = id;
            var response = request.Run();
            accountTreeHeader = response.AccountTreeHeader;
            rootNode = GenerateTree(accountTreeHeader, response.Items);
            treeView1.Nodes.Add(rootNode);
            treeView1.ExpandAll();
        }

        private void btnBeginEdit_Click(object sender, EventArgs e)
        {
            BeginEdit();
        }

        private void btnEndEdit_Click(object sender, EventArgs e)
        {
            EndEdit();
        }

        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            CancelEdit();
        }

        private void btnNewChild_Click(object sender, EventArgs e)
        {
            BeginNewAccount(true);
        }


        private void btnNewBrother_Click(object sender, EventArgs e)
        {
            BeginNewAccount(false);
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

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            previousNode = treeView1.SelectedNode;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTree();
        }

        private void ProcessTree(
            Util.IFacturanetTreenode node,
            Guid? parentId,
            List<ContableAccount> updatedAccounts, 
            List<ContableAccount> createdAccounts)
        {
            ContableAccountTreenode accountNode = node as ContableAccountTreenode;
            ContableAccount account = null;
            if (accountNode != null)
            {
                account = accountNode.TypedAsociatedObject;

                if (account.IsNew())
                    createdAccounts.Add(account);
                else if (account.IsDirty())
                    updatedAccounts.Add(account);
            }

            foreach (Util.IFacturanetTreenode subnode in node.Nodes)
                ProcessTree(
                    subnode,
                    account == null 
                        ? null
                        : (Guid?)account.Id,
                    updatedAccounts, 
                    createdAccounts);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var request = new Business.UpdateCompleteAccountTreeRequest(accountTreeHeader.Id);

            if (accountTreeHeader.IsDirty())
                request.AccountTreeHeader = accountTreeHeader;

            List<ContableAccount> updatedAccounts = new List<ContableAccount>();
            List<ContableAccount> createdAccounts = new List<ContableAccount>();
            ProcessTree(rootNode, null, updatedAccounts, createdAccounts);

            request.UpdatedAccounts = updatedAccounts.ToArray();
            request.CreatedAccounts = createdAccounts.ToArray();

            
            if (request.IsValid())
            {
                request.Run();
                RefreshTree();
            }
            else
            {
                Validation.ValidationResultBase results = request.GetValidationResult();

                if (results.Level == Validation.Level.Empty)
                    Console.WriteLine("Sin errores");
                else
                    Console.WriteLine("Máximo error: {0}", results.Level);

                Console.WriteLine("Cantidad de propiedades con errores: {0}", results.Length);

                MessageBox.Show(results.GetResultText());
            }
            
        }
    }
}
