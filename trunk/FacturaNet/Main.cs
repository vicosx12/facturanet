// project created on 06/02/2008 at 19:35
using System;
using FacturaNet.FnConfiguracion;

namespace FacturaNet
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			ConfiguracionFn configuracion = new ConfiguracionFn(args);

			//TODO: invocar un módulo para esas cosas
			if (configuracion.AccionesAdministracionActualizarDb)
				Console.WriteLine("Actualizar DB");
			
			if (configuracion.AccionesAdministracionAgregarUsuario)
				Console.WriteLine(
				                  "Agregar usuario nombre: {0}, clave: {1}",
				                  configuracion.AccionesAdministracionAgregarUsuarioNombre,
				                  configuracion.AccionesAdministracionAgregarUsuarioClave);
			
			if (!configuracion.NoIngresarAlEntorno)
				FacturaNet.FnGtk.MainClass.Run(configuracion);
			
		}
	}
}