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
using Nini.Config;


namespace AmUtil
{
	public abstract class Configuracion
	{
		private string configPath = "";
		protected const string cmdArgsSection = "cmdArgsSection";  

		private IniConfigSource source;
		protected IniConfigSource Source
		{
			get { return source;}
			private set { source = value; }
		}
		protected ArgvConfigSource argvSource = null;
		
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
				argvSource = new ArgvConfigSource(args);
				argvSource.AddSwitch (cmdArgsSection, "help", "h");
				argvSource.AddSwitch (cmdArgsSection, "version", "v");
				argvSource.AddSwitch (cmdArgsSection, "IniFile", "if"); //selecciona un nombre de archivo ini diferente predeterminado (no la ruta) 			
				
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
				InicializarSource(argvSource.Configs[cmdArgsSection].GetString("IniFile",nombreIni));
				
				ProcesarCommandLine(nombreIni, args);
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
			Source = new IniConfigSource(configPath);
			//SI NO EXISTE?
			/* también abrir esta? (ahi se van a grabar los cambios)
			 * C:\Documents and Settings\[username]\Local Settings\Application Data\[Application Name]\Settings.ini
			 * string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			 */		
		}
		
		protected abstract void ProcesarCommandLine(string nombreIni, string[] args); //para cuando se carga esto ya esta el help y cargado el ini correspondiente
/*		
		protected virtual void ProcesarCommandLine(string nombreIni, string[] args)
		{
			argvSource = new ArgvConfigSource(args);
			argvSource.AddSwitch (cmdArgsSection, "help", "h");
			argvSource.AddSwitch (cmdArgsSection, "version", "v");
			argvSource.AddSwitch (cmdArgsSection, "IniFile", "if"); //selecciona un nombre de archivo ini diferente predeterminado (no la ruta) 			
			
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
			InicializarSource(argvSource.Configs[cmdArgsSection].GetString("IniFile",nombreIni));
		}
*/
		protected abstract void PrintUsage();
		
		protected abstract void PrintVersion();
		
		protected void SaveConfig()
		{
			//TODO: hay que hacer que se pueda grabar el ini
			Console.WriteLine("Aca se debería grabar");
			
			try 
			{
				source.Save(configPath);
			}
			catch (Exception e)
			{
				throw new Exception("No se pudo grabar el archivo " + configPath,e);
			}
		}
	}
}
