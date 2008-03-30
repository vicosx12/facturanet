// project created on 06/02/2008 at 19:43
using System;
using Gtk;
using FacturaNet.FnAccesoDb;
using FacturaNet.FnNegocio;
using AmUtil;
using FacturaNet.FnConfiguracion;

namespace FacturaNet.FnGtk
{
	public class MainClass
	{
		public static void Main (string[] args)
		{
			ConfiguracionFn configuracion = new ConfiguracionFn(args);
			Run(configuracion);
/*			
			ConfigMngr.Inicializar(args);
			
			if (!ConfigMngr.Configuracion.Salir)
				Run();
*/				
		}
		
		public static void Run(ConfiguracionFn configuracion)
		{
			if (configuracion.NoIngresarAlEntorno)
				return;
			else
			{
				Application.Init ();
				FnAccesoDb.Global.Init(configuracion);
				DatabaseFn database;
				
				try 
				{
					
					database = new DatabaseFn();
				}
				catch (Exception e)
				{
					MessageDialog md = new MessageDialog (null, 
					                                      DialogFlags.Modal,
					                                      MessageType.Error, 
					                                      ButtonsType.Close,
					                                      string.Format(
	@"No se pudo conectar el sistema. Mensaje del error: 
		<i>{0}</i>",e.Message));

					md.Title = "Error conectando con la base de datos";
					md.Run();
					md.Destroy();
					return;
				}
			
				
				FnNegocio.Global.Init(configuracion,database);
				
				FrmLogin loginWindow = new FrmLogin();
				loginWindow.Show();
				
				Application.Run();			
				loginWindow.Destroy();
				
				if (FnNegocio.Global.Session.Conectado)
				{
					FrmPrincipal frmPrincipal = new FrmPrincipal();
					frmPrincipal.Show ();
					Application.Run ();
				}
			}
		}
	}
}