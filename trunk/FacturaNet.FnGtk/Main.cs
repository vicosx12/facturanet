// project created on 06/02/2008 at 19:43
using System;
using Gtk;
using FacturaNet.FnNegocio;
using AmUtil;

namespace FacturaNet.FnGtk
{
	class MainClass
	{
		public static bool CmdMode(string[] args)
		{
			bool cmdMode = false;
			if (args.Length > 0)
			{
				switch (args[0])
				{
					case "--crear_usuario" : 
						Sesion.SesionSingleton.CrearUsuario(args[1],args[2]);
						Console.WriteLine("Usuario creado exitosamente");
						cmdMode = true;
						break;
					case "--help" :
						Console.WriteLine("AYUDA");
						cmdMode = true;
						break;
					default :
						Console.WriteLine("Parametro desconocido, se continua con el modo gráfico");
						break;
				}
			}
			return cmdMode;
		}
		public static void Main (string[] args)
		{
			if (!CmdMode(args))
			{
				Application.Init ();
			
				FrmLogin loginWindow = new FrmLogin();
				loginWindow.Show();
				
				Application.Run();			
				loginWindow.Destroy();
				
				if (Sesion.SesionSingleton.Conectado)
				{
					FrmPrincipal frmPrincipal = new FrmPrincipal();
					frmPrincipal.Show ();
					Application.Run ();
				}
			}
			Util.Log("SALE");
		}
	}
}