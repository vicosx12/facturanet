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
	public sealed class Configuracion
	{
		private const string cmdArgsSection = "cmdArgsSection";  
		private string nombreIni = "facturanet.ini";
		private IConfigSource source;
		ArgvConfigSource argvSource = null;
		
		private bool salir = false;
		public bool Salir
		{
			get { return salir; }
		}
				
#region AccesoDb
		public string AccesoDbSelectedProvider
		{
			get { return source.Configs["AccesoDb"].GetString("SelectedProvider","Default"); }
			private set  
			{ 
				source.Configs["AccesoDb"].Set("SelectedProvider",value);
				ConfiguracionSelectedProvider = source.Configs["AccesoDb_" + value];
				
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
			
		/*
		private IConfig ConfiguracionSelectedProvider
		{
			get { return source.Configs["AccesoDb_" + AccesoDbSelectedProvider]; }
		}
		*/
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
		
		private void SetSource(string archivo)
		{
			source = new IniConfigSource(archivo);
			//SI NO EXISTE?
			/* también abrir esta? (ahi se van a grabar los cambios)
			 * C:\Documents and Settings\[username]\Local Settings\Application Data\[Application Name]\Settings.ini
			 * string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			 */		
		}
		
		public Configuracion(string[] args)
		{
			if (args != null) 
				ProcesarCommandLine(args);
			else
				SetSource(nombreIni);
		}

		private void ProcesarCommandLine(string[] args)
		{
			argvSource = new ArgvConfigSource(args);

			// agrego los switchs de control de la aplicacion (no configuracion)
			argvSource.AddSwitch (cmdArgsSection, "help", "h");
			argvSource.AddSwitch (cmdArgsSection, "version", "v");
			argvSource.AddSwitch (cmdArgsSection, "save-user", "su"); //graba los cambios de la configuración en el usuario y sale
			//argvSource.AddSwitch (cmdArgsSection, "SaveCommon", "sc"); //graba los cambios de la configuracion para todos los usuarios y sale
			//argvSource.AddSwitch (cmdArgsSection, "SaveIni", "si"); //graba toda la configuración en un archivo personalizado			
			argvSource.AddSwitch (cmdArgsSection, "IniFile", "if"); //selecciona un nombre de archivo ini diferente predeterminado (no la ruta) 			


			if (argvSource.Configs[cmdArgsSection].Get("help") != null)
			{
				PrintUsage();
				salir = true;
				return;
			}
			if (argvSource.Configs[cmdArgsSection].Get("version") != null) 
			{
				PrintVersion ();
				salir = true;
				return;
			}
			
			// cargo los inifiles
			SetSource(argvSource.Configs[cmdArgsSection].GetString("IniFile",nombreIni));
						
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
				salir = true;
				return;
			}
		}

		private void PrintUsage()
		{
			//TODO: hay que mostrar la ayuda
			Console.WriteLine("Aca se debería mostrar la ayuda");
		}
		
		private void PrintVersion()
		{
			//TODO: hay que mostrar la versión
			Console.WriteLine("Aca se debería mostrar la version");
		}
		
		private void SaveConfig()
		{
			//TODO: hay que hacer que se pueda grabar el ini
			Console.WriteLine("Aca se debería grabar");
/*
			if (IsArg ("new")) 
			{
				try 
				{
					CreateNewFile ();
				}
				catch (Exception ex) 
				{
					ThrowError ("Could not create file: " + ex.Message);
				}
			}
			if (!File.Exists (configPath)) 
			{
				ThrowError ("Config file does not exist");
			}
*/
		}
	}
}
