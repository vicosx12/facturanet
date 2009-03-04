using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Facturanet.DTOs
{
    public class FacturanetBindingList<T> : BindingList<T>
    {
        //Aca podría llevar un registro de los items que se eliminaron
        //por ahí puedo usar un delegado que me obtenga los indices de los que se van eliminando

        public FacturanetBindingList(IList<T> list)
            : base()
        {
            if (list != null)
                foreach (var item in list)
                    Add(item);
        }

        protected override void InsertItem(int index, T item)
        {
            var aux = item as DTOs.IDiscartableChanges;
            if (aux != null)
                aux.IDiscartableChangesActive = true;

            base.InsertItem(index, item);
        }
    }
}
