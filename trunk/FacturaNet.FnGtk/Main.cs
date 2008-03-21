// project created on 06/02/2008 at 19:43
using System;
using Gtk;
using FacturaNet.FnAccesoDb;
using AmUtil;

namespace FacturaNet.FnGtk
{
	public class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
		
			SesionMngr sesion = DbMngr.Db.CreateSesion();
			
			FrmLogin loginWindow = new FrmLogin();
			loginWindow.Show();
			
			Application.Run();			
			loginWindow.Destroy();		
		
			if (sesion.Conectado)
			{
				FrmPrincipal frmPrincipal = new FrmPrincipal();
				frmPrincipal.Show ();
				Application.Run ();
			}
			Util.Log("SALE");
		}
	}
}