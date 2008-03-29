// Configuracion.cs
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


namespace AmUtil
{
	public abstract class Configuracion
	{
		private void Encomillar()
		{
			foreach (IConfig config in source.Configs)
				foreach (string key in config.GetKeys())
					if ((config.Get(key).IndexOf(';') >= 0) || (config.Get(key).IndexOf(' ') >= 0) || (config.Get(key).IndexOf('"') >= 0))
						config.Set(
						           key,
						           AmString.PonerComillas(config.Get(key)));
		}
		
		private string configPath = "";
		protected const string cmdArgsSection = "cmdArgsSection";  

		public readonly string[] Args = null;
		
		private IniConfigSource source;

		/*
		protected IniConfigSource Source
		{
			get { return source;}
			private set { source = value; }
		}
	   */
		
		protected string ConfigGetString(string section, string key, string defaultValue)
		{
			return source.Configs[section].GetString(key,defaultValue);
		}
		protected void ConfigSet(string section, string key, object value)
		{
			/*
			Console.WriteLine("**********************");
			Console.WriteLine(section);
			Console.WriteLine(key);
			Console.WriteLine(value);
			Console.WriteLine("**********************");
			*/
			source.Configs[section].Set(key,value);
		}
		
		private bool salir = false;
		public bool Salir
		{
			get { return salir; }
			protected set { salir = value; }
		}
		
		public Configuracion(string nombreIni, string[] args)
		{
			if (args != null)
			{
				Args = args;
				
				ArgvConfigSource argvSource = new ArgvConfigSource(args);

				argvSource.AddSwitch (cmdArgsSection, "help", "h");
				argvSource.AddSwitch (cmdArgsSection, "version", "v");
				argvSource.AddSwitch (cmdArgsSection, "ini-file", "if"); //selecciona un nombre de archivo ini diferente predeterminado (no la ruta) 			
				argvSource.AddSwitch (cmdArgsSection, "save-user", "su"); //graba los cambios de la configuración en el usuario y sale			
				//argvSource.AddSwitch (cmdArgsSection, "SaveCommon", "sc"); //graba los cambios de la configuracion para todos los usuarios y sale
				//argvSource.AddSwitch (cmdArgsSection, "SaveIni", "si"); //graba toda la configuración en un archivo personalizado			
				
				if (argvSource.Configs[cmdArgsSection].Get("help") != null)
				{
					PrintUsage();
					Salir = true;
					return;
				}
				if (argvSource.Configs[cmdArgsSection].Get("version") != null) 
				{
					PrintVersion();
					Salir = true;
					return;
				}
				
				// cargo los inifiles
				InicializarSource(argvSource.Configs[cmdArgsSection].GetString("ini-file",nombreIni));
				
				ProcesarCommandLine(argvSource, nombreIni, args);
				
				if (argvSource.Configs[cmdArgsSection].Get("save-user") != null) 
				{
					SaveConfig();
					Salir = true;
					return;
				}
			}
			else
				InicializarSource(nombreIni);
		}

		public Configuracion(string nombreIni) : this(nombreIni, null)
		{			
		}

		
		protected void InicializarSource(string nombreIni)
		{
			configPath = nombreIni; //aca habria que aplicar alguna transformacion, por ejemplo agregar la carpeta
			source = new IniConfigSource(configPath);
			//SI NO EXISTE?
			/* también abrir esta? (ahi se van a grabar los cambios)
			 * C:\Documents and Settings\[username]\Local Settings\Application Data\[Application Name]\Settings.ini
			 * string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			 */		
		}
		
		protected abstract void ProcesarCommandLine(ArgvConfigSource argvSource, string nombreIni, string[] args); //para cuando se carga esto ya esta el help y cargado el ini correspondiente

		protected virtual void PrintUsage()
		{
			StringWriter writer = new StringWriter ();
			writer.WriteLine("");
			writer.WriteLine("Opciones generales:");
			writer.WriteLine("  -h,  --help                     Muestra esta ayuda");
			writer.WriteLine("  -v,  --version                  Muestra la versión de la aplicación");
			writer.WriteLine("  -if, --ini                  Muestra la versión de la aplicación");
			writer.WriteLine("  -su, --save-user                Graba la configuración en el ini del usuario");
			writer.WriteLine(""); 
			Console.WriteLine(writer.ToString ());
		}

		protected virtual string GetProductVersion ()
		{
			return "0.0.0";
		}
		protected virtual void PrintVersion()
		{
			StringWriter writer = new StringWriter ();
			writer.WriteLine("");
			writer.WriteLine("Version " + GetProductVersion ());
			writer.WriteLine("");			
			Console.WriteLine(writer.ToString ());

	    }
		
		
		protected void SaveConfig()
		{
			//TODO: hay que hacer que se pueda grabar el ini
			Console.WriteLine("Aca se debería grabar");
			try 
			{
				Encomillar();
				source.Save(configPath);               
			}
			catch (Exception e)
			{
				throw new Exception("No se pudo grabar el archivo " + configPath,e);
			}
		}
	}
}
