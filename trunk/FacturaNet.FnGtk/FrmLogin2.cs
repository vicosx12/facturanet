// FrmLogin2.cs
//
//  Copyright (C) 2008 [name of author]
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
//

using System;
using Gtk;
using FacturaNet.FnNegocio;

namespace FacturaNet.FnGtk
{
	
	
	public partial class FrmLogin2 : Gtk.Window
	{
		private int intentos;
		
		public FrmLogin2() : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			
			intentos = 0;
		}

		protected virtual void OnDeleteEvent (object o, Gtk.DeleteEventArgs args)
		{
			Application.Quit();
		}

		protected virtual void OnBtnCancelarClicked (object sender, System.EventArgs e)
		{
			Sesion.SesionSingleton.Desconectar();
			Application.Quit();
		}

		protected virtual void OnBtnAceptarClicked (object sender, System.EventArgs e)
		{
			if ((Sesion.SesionSingleton.Conectar(txtNombre.Text,txtClave.Text)) || (++intentos == 3))
				Application.Quit();
		}
	}
}
