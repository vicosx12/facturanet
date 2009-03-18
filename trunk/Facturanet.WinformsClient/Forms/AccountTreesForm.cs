using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Facturanet.Entities;
using Facturanet.Business;
using Facturanet.UI;
using Facturanet.Validation;

namespace Facturanet.WinformsClient.Forms
{
    public partial class AccountTreesForm : Form
    {
        private FacturanetBindingList<AccountTreesListItem> list;
        
        public AccountTreesForm()
        {
            InitializeComponent();
            RefreshList();
        }

        /// <summary>
        /// Refreshes the list.
        /// </summary>
        private void RefreshList()
        {
            var request = new ListAccountTreesRequest();
            var response = request.Run();
            list = new FacturanetBindingList<AccountTreesListItem>(response.Items);
            list.ListChanged += new ListChangedEventHandler(list_ListChanged);
            //ACA ME QUEDE
            listBindingSource.DataSource = list;
            //listDataGridView.DataSource = list;
            //listBindingNavigator.BindingSource = list;
        }

        void col_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine(e.Action);
            Console.WriteLine(e.NewItems);
            Console.WriteLine(e.NewStartingIndex);
            Console.WriteLine(e.OldItems);
            Console.WriteLine(e.OldStartingIndex);
        }

        void list_ListChanged(object sender, ListChangedEventArgs e)
        {
            Console.WriteLine("******list_ListChanged******");
            Console.WriteLine(e.ListChangedType);
            Console.WriteLine(e.NewIndex);
            Console.WriteLine(e.OldIndex);
            Console.WriteLine(e.PropertyDescriptor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var request = new CreateAccountTreeRequest(new Entities.AccountTree() { 
                Description = "prueba", 
                Name = "name", 
                Active = true, 
                Code = "codeeee"
            });
            request.Run();
        }

        private void listBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            Console.WriteLine("listBindingSource_AddingNew");
        }

        private void listBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            Console.WriteLine("******listBindingSource_ListChanged******");
            Console.WriteLine(e.ListChangedType);
            Console.WriteLine(e.NewIndex);
            Console.WriteLine(e.OldIndex);
            Console.WriteLine(e.PropertyDescriptor);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var request = new Business.UpdateAccountTreeListRequest(
                list.GetInsertedItems(),
                list.GetUpdatedItems(),
                list.GetDeletedItems());

            if (request.IsValid())
                request.Run();
            else
            {
                Validation.ValidationResultBase results = request.GetValidationResult();

                if (results.Level == Validation.Level.Empty)
                    Console.WriteLine("Sin errores");
                else
                    Console.WriteLine("Maximo error: {0}", results.Level);

                Console.WriteLine("Cantidad de propiedades con errores: {0}", results.Length);

                MessageBox.Show(results.GetResultText());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine(list[0].IsValid() ? "IS VALID" : "IS NOT VALID");

            Validation.ValidationResultBase results = list[0].GetValidationResult();

            if (results.Level == Validation.Level.Empty)
                Console.WriteLine("Sin errores");
            else
                Console.WriteLine("Maximo error: {0}", results.Level);

            Console.WriteLine("Cantidad de propiedades con errores: {0}", results.Length);
            
            foreach (var item in results)
                Console.WriteLine(item);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine(list.GetDeletedItems().Count());
            Console.WriteLine(list.GetUpdatedItems().Count());
            Console.WriteLine(list.GetInsertedItems().Count());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.listBindingSource.RemoveCurrent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Console.WriteLine(listBindingSource.AllowRemove);
            Console.WriteLine(listBindingSource.Count);
            Console.WriteLine(listBindingSource.Current);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("clieckkkkkkkkkkkk");
        }

        private void bindingNavigatorDeleteItem_EnabledChanged(object sender, EventArgs e)
        {
            Console.WriteLine("enableddddddddd");
            var tsi = (ToolStripItem) sender;
            tsi.Enabled = tsi.Enabled && listBindingSource.Current != null;
        }
    }
}
