// AdministracionDB.cs
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
using System.Data;
using System.Data.Common;
using AmUtil;

namespace FacturaNet.FnNegocio
{
	public class DbMngr
	{
		static private DbMngr db = null;		
		static public DbMngr Db
		{	
			get
			{
				if (db == null)
					db = new DbMngr();
				return db;
			}
		}		
		
		public readonly int VersionDbEsperada = 1;

		private DbProviderFactory _dbpFactory = null;
		private DbProviderFactory dbpFactory 
		{
			get
			{
				if (_dbpFactory == null)
				{
					Console.WriteLine(); Console.WriteLine();
					foreach (DataRow row in DbProviderFactories.GetFactoryClasses().Rows)
						Console.WriteLine("{0}\t{1}\t{2}\t{3}\n",row[0], row[1], row[2], row[3]);
					Console.WriteLine(); Console.WriteLine();
					_dbpFactory = DbProviderFactories.GetFactory(providerName);
				}
				return _dbpFactory;
			}
		}



		private Configuration config = null;
		private ConfiguracionAccesoSection configuracionAccesoSection = null;
		
		private ConfiguracionAccesoSection ConfiguracionAccesoSection
		{
			get 
			{
				if (configuracionAccesoSection == null) 
				{
					if (config == null) 
						config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

					string nombreAcceso = config.AppSettings.Settings["AccesoDbSeleccionado"].Value;
					configuracionAccesoSection = (ConfiguracionAccesoSection) config.Sections.Get(nombreAcceso);
				}
				return configuracionAccesoSection;
			}
		}
		private string providerName 
		{
			get { return ConfiguracionAccesoSection.ProviderName; } 
		}
		private string realUser
		{
			get { return ConfiguracionAccesoSection.RealUser; }
		}
		private string realPassword 
		{
			get { return ConfiguracionAccesoSection.RealPassword; }
		}
		private string server 
		{
			get { return ConfiguracionAccesoSection.Server; }
		}
		private string dataBase 
		{
			get { return ConfiguracionAccesoSection.DataBase; }
		}
		private string cnnString 
		{
			get { return ConfiguracionAccesoSection.CnnString; }
		} 

		internal DbConnection CreateConnection()
		{
			DbConnection cnn = dbpFactory.CreateConnection();
			cnn.ConnectionString = string.Format(
			                                      cnnString,
			                                      server,
			                                      dataBase,
			                                      realUser,
			                                      realPassword);
			return cnn;
		}
		internal DbDataAdapter CreateDataAdapter()
		{
			return dbpFactory.CreateDataAdapter();
		}
		internal DbDataAdapter CreateDataAdapter(string selectCommand)
		{
			DbDataAdapter da = CreateDataAdapter();
			DbCommand cmd = CreateCommand(selectCommand);
			da.SelectCommand = cmd;
			return da;
		}
		internal DbCommand CreateCommand()
		{
			DbCommand cmd = dbpFactory.CreateCommand();
			cmd.Connection = CreateConnection();
			return cmd;
		}
		internal DbCommand CreateCommand(string commandText)
		{
			DbCommand cmd = CreateCommand();
			cmd.CommandText = commandText;
			return cmd;
		}
		internal DbParameter CreateParameter()
		{
			return dbpFactory.CreateParameter();
		}
		internal DbParameter CreateParameter(string parameterName, DbType dbType)
		{
			DbParameter par = CreateParameter();
			par.ParameterName = parameterName;
			par.DbType = dbType;
			return par;
		}
		internal DbParameter CreateParameter(string parameterName, DbType dbType, object value)
		{
			DbParameter par = CreateParameter(parameterName, dbType);
			par.Value = value;
			return par;
		}
		
		public int FillUsuarios(DataTable tablaUsuarios)
		{
			DbDataAdapter da = CreateDataAdapter(
@"
SELECT  
	NB_USUARIO,
	DES_USUARIO
FROM
	SPS_LST_USUARIOS");
			return da.Fill(tablaUsuarios);
		}
		
		public SesionMngr CreateSesion()
		{
			return new SesionMngr(this);
		}
		
		private DbMngr()
		{
		}
		
		public void ActualizarDb()
		{
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
		
		public void CrearUsuario(string user, string password)
		{
			// TODO: Agregar algo para verificar que el usuario actual puede hacer esto y que está conectado		
			DbCommand cmd = CreateCommand("SPS_NEW_USUARIO");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(CreateParameter(
			                                   "@NB_USUARIO",
			                                   DbType.String,
			                                   user));
			cmd.Parameters.Add(CreateParameter(
			                                   "@DES_USUARIO",
			                                   DbType.String,
			                                   user));
			cmd.Parameters.Add(CreateParameter(
			                                   "@CLAVE",
			                                   DbType.String,
			                                   Util.CalcularSHA1(password)));  
			cmd.Connection.Open();
			cmd.ExecuteNonQuery();
			cmd.Connection.Close();
		}
	}
}
