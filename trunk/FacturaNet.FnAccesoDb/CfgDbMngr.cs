// CfgDbMngr.cs
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
using System.Configuration;


namespace FacturaNet.FnAccesoDb
{
	public class CfgDbMngr
	{
		static private CfgDbMngr cfg = null;		
		static public CfgDbMngr Cfg
		{	
			get
			{
				if (cfg == null)
					cfg = new CfgDbMngr();
				return cfg;
			}
		}	
		
		private CfgDbMngr()
		{
		}
		

		private Configuration config = null;
		private ConfiguracionAccesoSection configuracionAccesoSection = null;
		
		internal ConfiguracionAccesoSection ConfiguracionAccesoSection
		{
			get 
			{
				if (configuracionAccesoSection == null) 
				{
					if (config == null)
					{
						try 
						{
							config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
						}
						catch (ConfigurationErrorsException e)
						{
							throw new CfgDbMngrErrorFormatoConfigException("Error en el archivo de configuración",e);
						}
					}
					
					KeyValueConfigurationElement seleccionAccesoDb = config.AppSettings.Settings["AccesoDbSeleccionado"]; 
					
					if (seleccionAccesoDb == null)
						throw new CfgDbMngrNoSeleccionadaException("No se definió el acceso a base de datos"); 
				
					ConfiguracionAccesoSection seccionAccesoDb;
					try
					{
						 seccionAccesoDb = (ConfiguracionAccesoSection) config.Sections.Get(seleccionAccesoDb.Value);
					}
					catch (ConfigurationException e)
					{
						throw new CfgDbMngrErrorFormatoConfigException("Error en el archivo de configuración",e);
					}
					
					if ( seccionAccesoDb == null)
						throw new CfgDbMngrNoSeleccionadaException("No se definió la sección con la configuración seleccionada"); 
					
					configuracionAccesoSection = seccionAccesoDb;
				}
				return configuracionAccesoSection;
			}
		}
		
		internal string ProviderName 
		{
			get { return ConfiguracionAccesoSection.ProviderName; } 
		}
		
		internal string RealUser
		{
			get { return ConfiguracionAccesoSection.RealUser; }
		}
		
		internal string RealPassword 
		{
			get { return ConfiguracionAccesoSection.RealPassword; }
		}
		
		internal string Server 
		{
			get { return ConfiguracionAccesoSection.Server; }
		}
		
		internal string DataBaseName 
		{
			get { return ConfiguracionAccesoSection.DataBase; }
		}
		
		internal string CnnString 
		{
			get { return ConfiguracionAccesoSection.CnnString; }
		} 

		public void SeleccionarAccesoDb(string nombre)
		{
			if (config == null) 
				config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			
			config.AppSettings.Settings.Add("AccesoDbSeleccionado", nombre);
			
			config.Save(ConfigurationSaveMode.Full, true);
		}
		
		public void AgregarAccesoDb(string nombre, string providerName, string cnnString, string server, string dataBase, string realUser, string realPassword)
		{
			if (config == null) 
				config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			ConfiguracionAccesoSection section = new ConfiguracionAccesoSection();
			section.CnnString = cnnString; 
			section.ProviderName = providerName;
			section.Server = server;
			section.DataBase = dataBase;
			section.RealUser = realUser;
			section.RealPassword = realPassword;
			
			config.Sections.Add(nombre,section);
			config.Save(ConfigurationSaveMode.Full, true);
		}
	}
}
