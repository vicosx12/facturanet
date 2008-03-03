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
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
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
		
		public readonly int VersionEsperada = 1;

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

					string nombreAcceso;
					nombreAcceso = config.AppSettings.Settings["AccesoDbSeleccionado"].Value;				
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

			DbCommand cmd = dbpFactory.CreateCommand();
			cmd.Connection = cnn;
			cmd.CommandText = @"
Select 
	TS_VERSIONDB.VER
from
	TS_VERSIONDB
where
	TS_VERSIONDB.ID = 0";
			cmd.Connection.Open();
			int versionDb = (int)cmd.ExecuteScalar();
			cmd.Connection.Close();
			
			Console.WriteLine("*** {0} ***",versionDb);

			/*
			 TODO: verificar conexion y version DB 
			 Tengo que probar la base de datos:
			 tendria que revisar la versión solo una vez?
			      * Si no existe el alias de la db genera una excepción DbMngrNoAccesoDbException
			      * Si existe el alias pero no la db genera otra excepción DbMngrNoExisteDbException
			      * Si no corresponde la version de la db otra excepcion DbMngrVersionDbIncorrectaException
			      * Si no hubiera permisos en algun caso genera otra excepcion DbMngrPermisosDbException
			 despues tengo que agregar los scripts de creacion y actualizacion como recursos y ejecutarlos			 	
			 */
			
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
/*
		public void PrepararFirebird()
		{
			AgregarAccesoDb(
			                "firebird",
			                "FirebirdSql.Data.FirebirdClient",
			                "DataSource={0};Database={1};UserID={2};Password={3}",
			                "localhost",
			                "facturanet",
			                "SYSDBA",
			                "masterkey");
			SeleccionarAccesoDb("firebird");
		}
*/
/*		
		public void CrearFirebirdDb()
		{
			//FbScript script = new FbScript("a.txt");
			                               
			 //((FbConnection)DbMngr.Db).Create
			/
				// create a new database
				FbConnection.CreateDatabase(csb.ToString());
				
				// parse the SQL script
				FbScript script = new FbScript(pathScript);
				script.Parse();
		
				// execute the SQL script
				using(FbConnection c = new FbConnection(csb.ToString()))
				{
					FbBatchExecution fbe = new FbBatchExecution(c);
					foreach (string cmd in script.Results) 
					{
						fbe.SqlStatements.Add(cmd);
					}
					fbe.Execute();
				}

[STAThread]
static void Main(string[] args)
{
    FbConnectionStringBuilder cs = new FbConnectionStringBuilder();

    cs.UserID   = "SYSDBA";
    cs.Password = "masterkey";
    cs.Database = "nunit_testdb";

    FbBackup backupSvc = new FbBackup();
                
    backupSvc.ConnectionString = cs.ToString();
    backupSvc.BackupFiles.Add(new FbBackupFile(@"c:\testdb.gbk", 2048));
    backupSvc.Verbose = true;

    backupSvc.Options = FbBackupFlags.IgnoreLimbo;

    backupSvc.ServiceOutput += new ServiceOutputEventHandler(ServiceOutput);

    backupSvc.Execute();
}

static void ServiceOutput(object sender, ServiceOutputEventArgs e)
{
    Console.WriteLine(e.Message);
}
    

return to top

5. Database restore [v1.7].

[STAThread]
static void Main(string[] args)
{
    FbConnectionStringBuilder cs = new FbConnectionStringBuilder();

    cs.UserID   = "SYSDBA";
    cs.Password = "masterkey";
    cs.Database = "nunit_testdb";

    FbRestore restoreSvc = new FbRestore();

    restoreSvc.ConnectionString = cs.ToString();
    restoreSvc.BackupFiles.Add(new FbBackupFile(@"c:\testdb.gbk", 2048));
    restoreSvc.Verbose = true;
    restoreSvc.PageSize = 4096;
    restoreSvc.Options = FbRestoreFlags.Create | FbRestoreFlags.Replace;

    restoreSvc.ServiceOutput += new ServiceOutputEventHandler(ServiceOutput);

    restoreSvc.Execute();
}

static void ServiceOutput(object sender, ServiceOutputEventArgs e)
{
    Console.WriteLine(e.Message);
}
    


		}
*/
		
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
