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
using Nini.Config;
using AmUtil;

namespace FacturaNet.FnConfiguracion
{
	public sealed class ConfiguracionFn : Configuracion
	{			
		/* TODO
		 * Hacer la ayuda, el cartel de la version y el grabar
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
			get { return Source.Configs["AccesoDb"].GetString("SelectedProvider","Default"); }
			private set  
			{ 
				Source.Configs["AccesoDb"].Set("SelectedProvider",value);
				ConfiguracionSelectedProvider = Source.Configs["AccesoDb_" + value];				
				/*
				foreach (string key in ConfiguracionSelectedProvider.GetKeys())
					Console.WriteLine(key);
				foreach (string val in ConfiguracionSelectedProvider.GetValues())
					Console.WriteLine(val);
				*/
				//OJO CnnString y real password debería ir entre comillas por si hay puntos y comas
			}
		}
		
		private IConfig ConfiguracionSelectedProvider = null;

		public string AccesoDbProviderName
		{
			get { return ConfiguracionSelectedProvider.GetString("ProviderName","* UNDEFINED *"); }
			private set { ConfiguracionSelectedProvider.Set("ProviderName", value); }
		}	    
		public string AccesoDbCnnString
		{
			get { return ConfiguracionSelectedProvider.GetString("CnnString","* UNDEFINED *"); }
			private set { ConfiguracionSelectedProvider.Set("CnnString", value); }
		}
	    public string AccesoDbServer
		{
			get { return ConfiguracionSelectedProvider.GetString("Server","* UNDEFINED *"); }
			private set { ConfiguracionSelectedProvider.Set("Server", value); }
		}
	    public string AccesoDbDataBase
		{
			get { return ConfiguracionSelectedProvider.GetString("DataBase","* UNDEFINED *"); }
			private set { ConfiguracionSelectedProvider.Set("DataBase", value); }
		}
	    public string AccesoDbRealPassword
		{
			get { return ConfiguracionSelectedProvider.GetString("RealPassword","* UNDEFINED *"); }
			private set { ConfiguracionSelectedProvider.Set("RealPassword", value); }
			//encriptado debería dejar las claves de encriptacion solamente en la librería que se conecta a la base de datos
		} 
	    public string AccesoDbRealUser
		{
			get { return ConfiguracionSelectedProvider.GetString("RealUser","* UNDEFINED *"); }
			private set { ConfiguracionSelectedProvider.Set("RealUser", value); }
		} 
#endregion
		
		
		public ConfiguracionFn(string nombreIni, string[] args) : base(nombreIni, args)
		{
		}

		public ConfiguracionFn(string nombreIni) : base(nombreIni)
		{			
		}		

		protected override void ProcesarCommandLine(string nombreIni, string[] args)
		{
			// agrego los switchs de control de la aplicacion (no configuracion) Esto se podrá hacer en la clase padre?
			argvSource.AddSwitch (cmdArgsSection, "save-user", "su"); //graba los cambios de la configuración en el usuario y sale
			//argvSource.AddSwitch (cmdArgsSection, "SaveCommon", "sc"); //graba los cambios de la configuracion para todos los usuarios y sale
			//argvSource.AddSwitch (cmdArgsSection, "SaveIni", "si"); //graba toda la configuración en un archivo personalizado			
			// agrego el switch de seleccion de configuracion
			argvSource.AddSwitch ("AccesoDb", "SelectedProvider", "sp"); //indica cual es la configuración seleccionada
			// cargo los datos de accesoDb
			argvSource.AddSwitch ("AccesoDb", "ProviderName", "pn");
			argvSource.AddSwitch ("AccesoDb", "CnnString", "cs");
			argvSource.AddSwitch ("AccesoDb", "DataBase", "db");
			argvSource.AddSwitch ("AccesoDb", "RealPassword", "rp");
			argvSource.AddSwitch ("AccesoDb", "RealUser", "ru");
			argvSource.AddSwitch ("AccesoDb", "Server", "s");
			// seteo la configuracion seleccionada en la configuracion principal
			AccesoDbSelectedProvider = argvSource.Configs["AccesoDb"].GetString("SelectedProvider",AccesoDbSelectedProvider);
			// seteo los datos de la configuracion en la configuraion principal
			AccesoDbProviderName = argvSource.Configs["AccesoDb"].GetString("ProviderName",AccesoDbProviderName);
			AccesoDbCnnString = argvSource.Configs["AccesoDb"].GetString("CnnString",AccesoDbCnnString);
			AccesoDbDataBase = argvSource.Configs["AccesoDb"].GetString("DataBase",AccesoDbDataBase);
			AccesoDbRealUser = argvSource.Configs["AccesoDb"].GetString("RealUser",AccesoDbRealUser);
			AccesoDbServer = argvSource.Configs["AccesoDb"].GetString("Server",AccesoDbServer);


			if (argvSource.Configs["AccesoDb"].Get("RealPassword") != null)
				AccesoDbRealPassword = Util.MiEncriptacion(
				                                              argvSource.Configs["AccesoDb"].GetString("RealPassword"),
				                                              "bws623er",
				                                              "ma82ge4a"); 
			
			if (argvSource.Configs[cmdArgsSection].Get("save") != null) 
			{
				SaveConfig();
				Salir = true;
				return;
			}
		}

		protected override void PrintUsage()
		{
			//TODO: hay que mostrar la ayuda
			Console.WriteLine("Aca se debería mostrar la ayuda");
		}
		
		protected override void PrintVersion()
		{
			//TODO: hay que mostrar la versión
			Console.WriteLine("Aca se debería mostrar la version");
		}
	}
}
