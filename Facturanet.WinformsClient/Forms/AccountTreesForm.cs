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
using Facturanet.DTOs;

namespace Facturanet.WinformsClient.Forms
{
    public partial class AccountTreesForm : Form
    {
        //private IList<Lines.ILineAccountTree> list;
        /***/
        //private FacturanetBindingList<Lines.ILineAccountTree, Entities.AccountTree> list;
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
            list[0].Active = !list[0].Active;
            //((IDiscartableChanges)list[1]).DiscardChanges();
            Console.WriteLine(list[1].GetChanges());
        }
    }
}
