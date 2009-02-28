using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Facturanet.WinformsClient
{
    public class FacturanetBindingList<ItemListType, NewElementsType> : BindingList<ItemListType>
        where NewElementsType : ItemListType, new()
    {
        //Aca podría llevar un registro de los items que se eliminaron y modificaron
        //por ahí puedo usar un delegado que me obtenga los indices de los que se van eliminando
        public FacturanetBindingList(IList<ItemListType> list)
            : base()
        {
            this.AllowNew = true;
            foreach (var item in list)
                this.Add(item);
        }

        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            e.NewObject = new NewElementsType();
            base.OnAddingNew(e);
        }
    }
}
