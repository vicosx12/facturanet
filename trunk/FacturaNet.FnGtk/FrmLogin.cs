// FrmLogin.cs
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
	public class FrmLogin : Gtk.Window
	{
		private VBox boxPrincipal;
		private HButtonBox boxButtons;
		private VBox boxDatos;
		private Button btnCancelar;
		private Button btnAceptar;
		private Table tblDatos;
		private Entry txtNombre;
		private Entry txtClave;
		private Label laNombre;
		private Label laClave;
		private int intentos;
		
		public FrmLogin() : base(Gtk.WindowType.Toplevel)		                         
		{
			intentos = 0;
			
			//frmLogin
			this.DeleteEvent += new Gtk.DeleteEventHandler(this_DeleteEvent);	
			this.BorderWidth = 5;
			//this.DefaultHeight = 100;
			this.DefaultWidth = 300;

			//boxPrincipal
			boxPrincipal = new VBox(false, 0);
			boxPrincipal.Spacing = 2;
			boxPrincipal.Homogeneous = false;
			this.Add(boxPrincipal);
			boxPrincipal.Show();

			
			//boxDatos
			boxDatos = new VBox();
			boxPrincipal.PackStart(boxDatos,true,true,2);
			boxDatos.Show();

			tblDatos = new Table(2,2,false);
			boxDatos.Add(tblDatos);
			tblDatos.Show();			
			tblDatos.RowSpacing = 4;
			tblDatos.ColumnSpacing = 4;
			tblDatos.BorderWidth = 4;
					
			laNombre = new Label("Usuario");
			laNombre.Show();
			tblDatos.Attach(laNombre,0,1,0,1,AttachOptions.Fill, AttachOptions.Fill, 0, 0);
			txtNombre = new Entry();
			txtNombre.Show();
			tblDatos.Attach(txtNombre,1,2,0,1,AttachOptions.Fill, AttachOptions.Fill, 0, 0);
			
			

			laClave = new Label("Contraseña");
			laClave.Show();
			tblDatos.Attach(laClave,0,1,1,2);//,AttachOptions.Expand, AttachOptions.Fill, 0, 0);
			txtClave = new Entry();
			txtClave.Show();
			tblDatos.Attach(txtClave,1,2,1,2);//,AttachOptions.Expand, AttachOptions.Fill, 0, 0);
					
			//boxButtons
			boxButtons = new HButtonBox();
			boxButtons.Spacing = 4;
			boxButtons.BorderWidth = 2;
			boxButtons.LayoutStyle = ButtonBoxStyle.End; 
			boxButtons.Homogeneous = false;	
			boxPrincipal.PackStart (boxButtons, false, true, 0);
			boxButtons.Show();			
			
			//btnCancelar
			btnCancelar = new Button(Stock.Cancel);
			btnCancelar.Clicked += new EventHandler(btnCancelar_Clicked);
			btnCancelar.CanDefault = true; 
			boxButtons.PackStart(btnCancelar, true, true, 0);
			btnCancelar.Show();

			//btnAceptar
			btnAceptar = new Button(Stock.Ok);
			btnAceptar.Clicked += new EventHandler(btnAceptar_Clicked);
			btnAceptar.CanDefault = true; 
			boxButtons.PackStart(btnAceptar, true, true, 0);
			btnAceptar.Show();
			this.Default = btnAceptar;
		}
		
		static void this_DeleteEvent(object sender,  DeleteEventArgs e)
		{
			Application.Quit();
		}		
		static void btnCancelar_Clicked(object sender,  EventArgs e)
		{
			Sesion.SesionSingleton.Desconectar();
			Application.Quit();
		}		
		private void btnAceptar_Clicked(object sender,  EventArgs e)
		{
			if ((Sesion.SesionSingleton.Conectar(txtNombre.Text,txtClave.Text)) || (++intentos == 3))
				Application.Quit();
		}		
	}
}
