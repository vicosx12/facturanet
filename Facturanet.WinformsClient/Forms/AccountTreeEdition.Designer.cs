namespace Facturanet.WinformsClient.Forms
{
    partial class AccountTreeEdition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.contableAccountEditor1 = new Facturanet.WinformsClient.Controls.ContableAccountEditor();
            this.accountTreeListItemEditor1 = new Facturanet.WinformsClient.Controls.AccountTreeListItemEditor();
            this.button6 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(42, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(112, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "showInactiveItems";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(12, 42);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(567, 176);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(208, 345);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Discard Changes";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(378, 345);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Save Changes";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.contableAccountEditor1);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.accountTreeListItemEditor1);
            this.panel1.Location = new System.Drawing.Point(12, 224);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(566, 115);
            this.panel1.TabIndex = 4;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(488, 61);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 3;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(488, 32);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(488, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // contableAccountEditor1
            // 
            this.contableAccountEditor1.EditableObject = null;
            this.contableAccountEditor1.Location = new System.Drawing.Point(267, 4);
            this.contableAccountEditor1.Name = "contableAccountEditor1";
            this.contableAccountEditor1.Size = new System.Drawing.Size(197, 85);
            this.contableAccountEditor1.TabIndex = 4;
            this.contableAccountEditor1.TypedEditableObject = null;
            // 
            // accountTreeListItemEditor1
            // 
            this.accountTreeListItemEditor1.EditableObject = null;
            this.accountTreeListItemEditor1.Location = new System.Drawing.Point(3, 4);
            this.accountTreeListItemEditor1.Name = "accountTreeListItemEditor1";
            this.accountTreeListItemEditor1.Size = new System.Drawing.Size(225, 80);
            this.accountTreeListItemEditor1.TabIndex = 0;
            this.accountTreeListItemEditor1.TypedEditableObject = null;
            this.accountTreeListItemEditor1.Visible = false;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(373, 11);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 5;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // AccountTreeEdition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 379);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.checkBox1);
            this.Name = "AccountTreeEdition";
            this.Text = "AccountTreeEdition";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private Facturanet.WinformsClient.Controls.AccountTreeListItemEditor accountTreeListItemEditor1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private Facturanet.WinformsClient.Controls.ContableAccountEditor contableAccountEditor1;
        private System.Windows.Forms.Button button6;
    }
}