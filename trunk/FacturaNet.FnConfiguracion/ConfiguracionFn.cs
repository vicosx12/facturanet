// MyClass.cs
// 
// Copyright (C) 2008 Andrés Moschini
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Text;
using System.IO;
using Nini.Config;
using AmUtil;

namespace FacturaNet.FnConfiguracion
{
	public sealed class ConfiguracionFn : Configuracion
	{
		/* TODO
		 * Hacer el grabar
		 * 
		 * Verificar que se grabe correctamente
		 *  
		 * Verificar que se creen nuevas secciones
		 * 
		 * Verificar que se cambie de seccion
		 * 
		 * Ver donde llamar el actualizarDb (usar recursos: 
		 * 	  Assembly a = Assembly.GetExecutingAssembly();
		 *	  //string [] resNames = a.GetManifestResourceNames();
		 *	  Console.WriteLine((a.GetManifestResourceStream("recurso.txt")).Length);
		 * 
		 * Ver donde llamar el creador de usuarios
		 *
		 * 
		 */		
		
#region AccesoDb
		public string AccesoDbSelectedProvider
		{
			get { return ConfigGetString("AccesoDb","SelectedProvider","Default"); }
			private set
			{
				ConfigSet("AccesoDb","SelectedProvider",value);
			}
		}
				
		private void AccesoDbSelectedProviderSet(string key, object value)
		{
			ConfigSet("AccesoDb_" + AccesoDbSelectedProvider,key,value);
		}
		private string AccesoDbSelectedProviderGetString(string key, string defaultValue)
		{
			return ConfigGetString("AccesoDb_" + AccesoDbSelectedProvider,key,defaultValue);
		}

		public string AccesoDbProviderName
		{
		    get { return AccesoDbSelectedProviderGetString("ProviderName","* UNDEFINED *"); }
		    set { AccesoDbSelectedProviderSet("ProviderName", value); }
		}	    
		public string AccesoDbCnnString
		{
		    get { return AccesoDbSelectedProviderGetString("CnnString","* UNDEFINED *"); }
		    set { AccesoDbSelectedProviderSet("CnnString", value); }
		}
	    public string AccesoDbServer
		{
		    get { return AccesoDbSelectedProviderGetString("Server","* UNDEFINED *"); }
		    set { AccesoDbSelectedProviderSet("Server", value); }
		}
	    public string AccesoDbDataBase
		{
		    get { return AccesoDbSelectedProviderGetString("DataBase","* UNDEFINED *"); }
		    set { AccesoDbSelectedProviderSet("DataBase", value); }
		}
	    public string AccesoDbRealPassword
		{
		    get { return AccesoDbSelectedProviderGetString("RealPassword","* UNDEFINED *"); }
		    set { AccesoDbSelectedProviderSet("RealPassword", value); }
		} 
	    public string AccesoDbRealUser
		{
		    get { return AccesoDbSelectedProviderGetString("RealUser","* UNDEFINED *"); }
		    set { AccesoDbSelectedProviderSet("RealUser", value); }
		} 
#endregion

#region AccionesAdministracion
		private bool accionesAdministracionActualizarDb;
		public bool AccionesAdministracionActualizarDb 
		{
	    	get { return accionesAdministracionActualizarDb; }
			private set { accionesAdministracionActualizarDb = value; }
	    }
		
		private string[] accionesAdministracionAgregarUsuarioNombreClave;
		public bool AccionesAdministracionAgregarUsuario 
		{
	    	get { return accionesAdministracionAgregarUsuarioNombreClave != null; }
	    }
		public string AccionesAdministracionAgregarUsuarioNombre 
		{
	    	get { return accionesAdministracionAgregarUsuarioNombreClave[0]; }
	    }
		public string AccionesAdministracionAgregarUsuarioClave 
		{
	    	get { return accionesAdministracionAgregarUsuarioNombreClave[1]; }
	    }
#endregion
		
		public ConfiguracionFn(string[] args) : base(Global.NombreIni, args)
		{
		}
		public ConfiguracionFn() : base(Global.NombreIni)
		{			
		}		

		protected override void ProcesarCommandLine(ArgvConfigSource argvSource)//, string nombareIni, string[] args)
		{
			ProcesarCommandLineAccesoDb(argvSource);
			ProcesarCommandLineAccionesAdministracion(argvSource);
		}
		private void ProcesarCommandLineAccionesAdministracion(ArgvConfigSource argvSource)
		{
			argvSource.AddSwitch ("AccionesAdministracion", "UpdateDb", "up");
			argvSource.AddSwitch ("AccionesAdministracion", "AddUser", "au");
						
			AccionesAdministracionActualizarDb = (argvSource.Configs["AccionesAdministracion"].Get("UpdateDb") != null);
			
			if (argvSource.Configs["AccionesAdministracion"].Get("AddUser") == null)
				accionesAdministracionAgregarUsuarioNombreClave = null;
			else
			{
				accionesAdministracionAgregarUsuarioNombreClave = argvSource.Configs["AccionesAdministracion"].GetString("AddUser").Split (',');
				if (accionesAdministracionAgregarUsuarioNombreClave.Length != 2)
				{
					accionesAdministracionAgregarUsuarioNombreClave = null;
					throw new Exception ("AddUser requiere un parámetro del tipo USER,PASSWORD");
				}
			}
			
			NoIngresarAlEntorno = AccionesAdministracionActualizarDb | AccionesAdministracionAgregarUsuario;
		}
		private void ProcesarCommandLineAccesoDb(ArgvConfigSource argvSource)
		{
			// agrego el switch de seleccion de configuracion
			argvSource.AddSwitch ("AccesoDb", "SelectedProvider", "sp"); //indica cual es la configuración seleccionada
			
			// cargo los datos de accesoDb
			argvSource.AddSwitch ("AccesoDb", "ProviderName", "pn");
			argvSource.AddSwitch ("AccesoDb", "CnnString", "cs");
			argvSource.AddSwitch ("AccesoDb", "DataBase", "db");
			argvSource.AddSwitch ("AccesoDb", "RealPassword", "rp");
			argvSource.AddSwitch ("AccesoDb", "RealUser", "ru");
			argvSource.AddSwitch ("AccesoDb", "Server", "sv");

			// seteo la configuracion seleccionada en la configuracion del ini
			AccesoDbSelectedProvider = argvSource.Configs["AccesoDb"].GetString("SelectedProvider",AccesoDbSelectedProvider);
			
			// seteo los datos de la configuracion en la configuraion del ini
			if (argvSource.Configs["AccesoDb"].GetString("ProviderName") != null)
				AccesoDbProviderName = argvSource.Configs["AccesoDb"].GetString("ProviderName",AccesoDbProviderName);

			if (argvSource.Configs["AccesoDb"].GetString("CnnString") != null)
				AccesoDbCnnString = argvSource.Configs["AccesoDb"].GetString("CnnString",AccesoDbCnnString);

			if (argvSource.Configs["AccesoDb"].GetString("DataBase") != null)
				AccesoDbDataBase = argvSource.Configs["AccesoDb"].GetString("DataBase",AccesoDbDataBase);

			if (argvSource.Configs["AccesoDb"].GetString("RealUser") != null)
				AccesoDbRealUser = argvSource.Configs["AccesoDb"].GetString("RealUser",AccesoDbRealUser);
			
			if (argvSource.Configs["AccesoDb"].Get("Server") != null)
				AccesoDbServer = argvSource.Configs["AccesoDb"].GetString("Server",AccesoDbServer);

			// encripto el password
			if (argvSource.Configs["AccesoDb"].Get("RealPassword") != null)
				AccesoDbRealPassword = AmString.Encriptar(
				                                              argvSource.Configs["AccesoDb"].GetString("RealPassword"),
				                                              "bws623er",
				                                              "ma82ge4a"); 
		}

		protected override void PrintUsage()
		{
			StringWriter writer = new StringWriter ();
			writer.WriteLine("FacturaNet " + GetProductVersion () + 
							 ", sistema de gestión y facturación");
			writer.WriteLine("Uso: FacturaNet [OPCIONES]");
			writer.WriteLine("");
			writer.WriteLine("Opciones generales:");
			writer.WriteLine("  -h,  --help                     Muestra esta ayuda");
			writer.WriteLine("  -v,  --version                  Muestra la versión de la aplicación");
			writer.WriteLine("  -if, --ini                  Muestra la versión de la aplicación");
			writer.WriteLine("  -su, --save-user                Graba la configuración en el ini del usuario");
			writer.WriteLine(""); 
			writer.WriteLine("Opciones de acceso a la base de datos:");
			writer.WriteLine("  -sp, --SelectedProvider=VALOR   Selecciona el acceso a la DB especificado");
			writer.WriteLine("  -pn, --ProviderName=VALOR       Define el nombre del driver a utilizar");
			writer.WriteLine("  -cs, --CnnString=VALOR          Define el formato del string de conexión");			
			writer.WriteLine("  -sv, --Server=VALOR             Define la dirección del servidor");			
			writer.WriteLine("  -db, --DataBase=VALOR           Define el nombre de la DB a utilizar");
			writer.WriteLine("  -ru, --RealUser=VALOR           Define el usuario de acceso a la DB");
			writer.WriteLine("  -rp, --RealPassword=VALOR       Define el password de acceso a la DB");
			writer.WriteLine(""); 
			writer.WriteLine("Opciones de administración:"); 
			writer.WriteLine("  -up, --UpdateDb                Actualiza la versión de la DB");
			writer.WriteLine("  -au, --AddUser=USER,PASSWORD   Agrega el usuario indicado a la DB");
			writer.WriteLine("");
			writer.WriteLine("FacturaNet homepage: http://code.google.com/p/facturanet/");

			Console.WriteLine(writer.ToString ());
		}
		
		protected override string GetProductVersion ()
		{
			//HACK: HAY QUE SACAR LA VERSION DE OTRO LADO
			return "0.0.1";
		}
		protected override void PrintVersion()
		{
			StringWriter writer = new StringWriter ();
			writer.WriteLine("FacturaNet " + GetProductVersion ());
			writer.WriteLine("");
			writer.WriteLine("Copyright 2008 Andrés Moschini");
			writer.WriteLine("GNU General Public License Version 3:");
			writer.WriteLine("http://www.gnu.org/licenses/gpl.html");
			
			Console.WriteLine(writer.ToString ());
		}
	}
}
