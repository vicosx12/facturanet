namespace Facturanet.WinformsClient
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.impuestosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articuloBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombreDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clienteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numeroDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clienteDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.facturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemFacturaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.articuloBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.clienteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.facturaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemFacturaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nombreDataGridViewTextBoxColumn,
            this.impuestosDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.articuloBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 25);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(432, 93);
            this.dataGridView1.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nombreDataGridViewTextBoxColumn
            // 
            this.nombreDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nombreDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nombreDataGridViewTextBoxColumn.Name = "nombreDataGridViewTextBoxColumn";
            // 
            // impuestosDataGridViewTextBoxColumn
            // 
            this.impuestosDataGridViewTextBoxColumn.DataPropertyName = "Taxes";
            this.impuestosDataGridViewTextBoxColumn.HeaderText = "Taxes";
            this.impuestosDataGridViewTextBoxColumn.Name = "impuestosDataGridViewTextBoxColumn";
            // 
            // articuloBindingSource
            // 
            this.articuloBindingSource.DataSource = typeof(Facturanet.Entities.Product);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(501, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn1,
            this.nombreDataGridViewTextBoxColumn1});
            this.dataGridView2.DataSource = this.clienteBindingSource;
            this.dataGridView2.Location = new System.Drawing.Point(12, 137);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(432, 86);
            this.dataGridView2.TabIndex = 2;
            // 
            // idDataGridViewTextBoxColumn1
            // 
            this.idDataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn1.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn1.Name = "idDataGridViewTextBoxColumn1";
            // 
            // nombreDataGridViewTextBoxColumn1
            // 
            this.nombreDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nombreDataGridViewTextBoxColumn1.HeaderText = "Name";
            this.nombreDataGridViewTextBoxColumn1.Name = "nombreDataGridViewTextBoxColumn1";
            // 
            // clienteBindingSource
            // 
            this.clienteBindingSource.DataSource = typeof(Facturanet.Entities.Customer);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AutoGenerateColumns = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn2,
            this.numeroDataGridViewTextBoxColumn,
            this.clienteDataGridViewTextBoxColumn});
            this.dataGridView3.DataSource = this.facturaBindingSource;
            this.dataGridView3.Location = new System.Drawing.Point(12, 242);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(432, 78);
            this.dataGridView3.TabIndex = 3;
            // 
            // idDataGridViewTextBoxColumn2
            // 
            this.idDataGridViewTextBoxColumn2.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn2.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn2.Name = "idDataGridViewTextBoxColumn2";
            // 
            // numeroDataGridViewTextBoxColumn
            // 
            this.numeroDataGridViewTextBoxColumn.DataPropertyName = "Number";
            this.numeroDataGridViewTextBoxColumn.HeaderText = "Number";
            this.numeroDataGridViewTextBoxColumn.Name = "numeroDataGridViewTextBoxColumn";
            // 
            // clienteDataGridViewTextBoxColumn
            // 
            this.clienteDataGridViewTextBoxColumn.DataPropertyName = "Customer";
            this.clienteDataGridViewTextBoxColumn.HeaderText = "Customer";
            this.clienteDataGridViewTextBoxColumn.Name = "clienteDataGridViewTextBoxColumn";
            // 
            // facturaBindingSource
            // 
            this.facturaBindingSource.DataSource = typeof(Facturanet.Entities.Invoice);
            this.facturaBindingSource.CurrentChanged += new System.EventHandler(this.facturaBindingSource_CurrentChanged);
            // 
            // dataGridView4
            // 
            this.dataGridView4.AutoGenerateColumns = false;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn3});
            this.dataGridView4.DataSource = this.itemFacturaBindingSource;
            this.dataGridView4.Location = new System.Drawing.Point(12, 339);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(432, 150);
            this.dataGridView4.TabIndex = 4;
            // 
            // idDataGridViewTextBoxColumn3
            // 
            this.idDataGridViewTextBoxColumn3.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn3.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn3.Name = "idDataGridViewTextBoxColumn3";
            // 
            // itemFacturaBindingSource
            // 
            this.itemFacturaBindingSource.DataSource = typeof(Facturanet.Entities.InvoiceItem);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Artículos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Clientes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Facturas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 323);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Items";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(589, 32);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(76, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Usar Mock";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(509, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 544);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView4);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.articuloBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.clienteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.facturaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemFacturaBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn impuestosDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource articuloBindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombreDataGridViewTextBoxColumn1;
        private System.Windows.Forms.BindingSource clienteBindingSource;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeroDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clienteDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource facturaBindingSource;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn3;
        private System.Windows.Forms.BindingSource itemFacturaBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
    }
}

