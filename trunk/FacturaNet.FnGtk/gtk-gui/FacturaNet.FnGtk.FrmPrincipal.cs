// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace FacturaNet.FnGtk {
    
    
    public partial class FrmPrincipal {
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget FacturaNet.FnGtk.FrmPrincipal
            this.Name = "FacturaNet.FnGtk.FrmPrincipal";
            this.Title = Mono.Unix.Catalog.GetString("FrmPrincipal");
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            this.BorderWidth = ((uint)(6));
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 400;
            this.DefaultHeight = 300;
            this.Show();
            this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
        }
    }
}
