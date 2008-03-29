// project created on 06/02/2008 at 19:35
using System;
using FacturaNet.FnConfiguracion;

namespace FacturaNet
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			ConfigMngr.Inicializar(args);
						
//aca revisar las demas opciones que van a admin
/*
DbMngr.ActualizarDb()
DbMngr.CrearUsuario(string user, string password)
			string[] keyValue = GetArg ("set-key").Split (',');
			if (keyValue.Length < 2) {
				throw new Exception ("Must supply KEY,VALUE");
			}
			config.Set (keyValue[0], keyValue[1]);
			source.Save ();

			if (verbose) {
				PrintLine ("Key " + keyValue[0] + " was saved as " + keyValue[1]);
			}

Seleccionar_acceso_db y Agregar_acceso_db van en la parte de configuracion			
*/
			
			if (!ConfigMngr.Configuracion.Salir)
				FacturaNet.FnGtk.MainClass.Run();
		}
	}
}