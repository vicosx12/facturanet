using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Facturanet.Util;

namespace Facturanet.UI
{
    public class FacturanetBindingList<T> : BindingList<T>
    {
        //Aca podría llevar un registro de los items que se eliminaron
        //por ahí puedo usar un delegado que me obtenga los indices de los que se van eliminando
        private List<T> deletedItems;
        private List<T> insertedItems;
        private List<T> updatedItems;
        private bool typeIsDiscartableChanges;
        private bool typeIsCreable;
        private bool typeIsDeletable;

        public IEnumerable<T> GetDeletedItems()
        {
            return deletedItems.AsEnumerable();
        }

        public FacturanetBindingList(IList<T> list)
            : base()
        {
            Type type = typeof(T);
            typeIsDiscartableChanges = type.ImplementsInterface<UI.IDiscartableChanges>();
            typeIsCreable = type.ImplementsInterface<UI.ICreableUIObject>();
            typeIsDeletable = type.ImplementsInterface<UI.IDeletableUIObject>();
            if (typeIsDiscartableChanges)
            {
                AllowEdit = true;
                AllowNew = typeIsCreable;
                AllowRemove = typeIsDeletable;
            }

            if (list != null)
                foreach (var item in list)
                    Add(item);

            deletedItems = new List<T>();
            insertedItems = new List<T>();
            updatedItems = new List<T>();
        }

        protected override void InsertItem(int index, T item)
        {
            if (typeIsDiscartableChanges)
                (item as UI.IDiscartableChanges).DiscartableChangesControl = true;

            if (insertedItems != null && (!typeIsCreable || (item as UI.ICreableUIObject).IsNew))
                insertedItems.Add(item);

            base.InsertItem(index, item);
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            base.OnListChanged(e);
            Console.WriteLine("******" + e.ListChangedType);
        }

        protected override void RemoveItem(int index)
        {
            if (index >= 0 && index < this.Count)
            {
                var item = this[index];

                if (typeIsDeletable)
                {
                    UI.IDeletableUIObject deletable = item as UI.IDeletableUIObject;
                    deletable.IsDeleted = true;
                }

                if (!typeIsCreable || !(item as UI.ICreableUIObject).IsNew)
                    deletedItems.Add(item);

                base.RemoveItem(index);
            }
        }

        #region Sorting //From http://code.google.com/p/banshee32/trunk/banshee32/Control/SearchableSortableBindingList.cs

        private bool isSorted;
        private PropertyDescriptor sortProperty;
        private ListSortDirection sortDirection;

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        // Missing from Part 2
        protected override ListSortDirection SortDirectionCore
        {
            get { return sortDirection; }
        }

        // Missing from Part 2
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return sortProperty; }
        }

        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
        {
            // Get list to sort
            // Note: this.Items is a non-sortable ICollection<T>
            List<T> items = this.Items as List<T>;

            // Apply and set the sort, if items to sort
            if (items != null)
            {
                PropertyComparer<T> pc = new PropertyComparer<T>(property, direction);
                
                items.Sort(pc);
                isSorted = true;
            }
            else
            {
                isSorted = false;
            }

            sortProperty = property;
            sortDirection = direction;

            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override bool IsSortedCore
        {
            get { return isSorted; }
        }

        protected override void RemoveSortCore()
        {
            //isSorted = false;
            //this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        #endregion

        #region Searching //From http://code.google.com/p/banshee32/trunk/banshee32/Control/SearchableSortableBindingList.cs

        protected override bool SupportsSearchingCore
        {
            get { return true; }
        }

        protected override int FindCore(PropertyDescriptor property, object key)
        {
            //TODO: Podría hacer algo como que si property es null busque en todos los campos
            //Todo: si solo quiero buscar por el comienzo?
            //Todo: el qkey tienq que ser texto si o si?


            // Specify search columns
            if (property == null) return -1;

            // Get list to search
            List<T> items = this.Items as List<T>;

            // Traverse list for value
            foreach (T item in items)
            {
                // Test column search value
                string value = (string)property.GetValue(item);

                // If value is the search value, return the 
                // index of the data item
                if ((string)key == value) return IndexOf(item);
            }
            return -1;
        }

        #endregion
    }
 }