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

/* PENDIENTE:
 * tengo que hacer que use el provider independent para las consultas.
 * tengo que ver como hacer para no necesitar gacutil y modificar
 * /etc/mono/2.0/machine.config, donde puse las siguientes lineas (dentro de DbProviderFactories):
    <add name="Firebird Data Provider" invariant="FirebirdSql.Data.FirebirdClient" 
		 description="Firebird"
		 type="FirebirdSql.Data.FirebirdClient.FirebirdClientFactory, FirebirdSql.Data.FirebirdClient, Version=2.0.1.0, Culture=neutral, PublicKeyToken=3750abcc3150b00c" />

    <add name="MySql Data Provider" invariant="MySql.Data.MySqlClient" 
		 description="MySql"
		 type="MySql.Data.MySqlClient.MySqlClientFactory, MySql.Data.MySqlClient, Version=5.2.0.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d" />  
 * Me gustaría generar la base de datos desde el ejecutable
 * Tengo que mostrar el listado de usuarios en las tablas y los combos para probar gtk
 * Probar en windows. 
 * */ 
  

using System;
using Gtk;
using FacturaNet.FnAccesoDb;

namespace FacturaNet.FnGtk
{
	public partial class FrmLogin : Gtk.Window
	{
		private int intentos;
		
		public FrmLogin() : 
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
			Application.Quit();
		}

		protected virtual void OnBtnAceptarClicked (object sender, System.EventArgs e)
		{		
			if (DatabaseFn.DatabaseCAMBIAR.Sesion.Conectar(txtNombre.Text,txtClave.Text)) 
				Application.Quit();
			else if (++intentos < 3)
				Console.WriteLine("Usuario o contraseñas no válidos.");
			else
			{
				Console.WriteLine("Usuario y contraseña correcto.");
				Application.Quit();
			}
		}
	}
}
