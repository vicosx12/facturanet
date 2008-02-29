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
			bool detener = false;
			if (args.Length > 0)
			{
				//HACK: hay que mejorar la lectura de las opciones de linea de comandos
				switch (args[0])
				{
					case "--help" :
						Console.WriteLine("AYUDA");
						detener = true;
						break;
					default :
						Console.WriteLine("Parametro desconocido, se continua con el modo gráfico");
						break;
				}
			}
			return detener;
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