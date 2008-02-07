// project created on 06/02/2008 at 19:43
using System;
using Gtk;
using FacturaNet.FnNegocio;
using AmUtil;

namespace FacturaNet.FnGtk
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/*
			Application.Init ();
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
			*/
			Application.Init ();
			
			FrmLogin loginWindow = new FrmLogin();
			loginWindow.Show();

			Application.Run();			
			loginWindow.Destroy();

			if (Sesion.SesionSingleton.RolDb == RolDb.UsuarioStandard)
			{
				FrmPrincipal frmPrincipal = new FrmPrincipal();
				frmPrincipal.Show ();
				Application.Run ();
				Util.Log("SALE");
			}
			else if (Sesion.SesionSingleton.RolDb == RolDb.NoEsUsuario)
					Util.Log("El par usuario,clave no es válido."); 
		}
	}
}