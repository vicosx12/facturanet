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
using System.Data;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.Data.Isql;
using FacturaNet.FnConfiguracion;
using AmUtil;

namespace FacturaNet.FnAccesoDb
{
	public class DatabaseFn
	{
		static private DatabaseFn database = null;		
		static public DatabaseFn DatabaseCAMBIAR
		{	
			get
			{
				//if (db == null)
				//	db = new DbMngr();
				return database;
			}
		}		
		static public void Init()
		{
			if (database == null)
				database = new DatabaseFn();
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
					//_dbpFactory = DbProviderFactories.GetFactory(CfgDbMngr.Cfg.ProviderName);
					_dbpFactory = DbProviderFactories.GetFactory(ConfigMngr.Configuracion.AccesoDbProviderName);
				}
				return _dbpFactory;
			}
		}

		internal DbConnection CreateConnection()
		{
			DbConnection cnn = dbpFactory.CreateConnection();
			cnn.ConnectionString = string.Format(
			                                     ConfigMngr.Configuracion.AccesoDbCnnString,
			                                     ConfigMngr.Configuracion.AccesoDbServer,
			                                     ConfigMngr.Configuracion.AccesoDbDataBase,
			                                     ConfigMngr.Configuracion.AccesoDbRealUser,
			                                     AmString.Desencriptar(
			                                                            ConfigMngr.Configuracion.AccesoDbRealPassword, 
			                                                            "bws623er", 
			                                                            "ma82ge4a"));
			                                     //CfgDbMngr.Cfg.CnnString,
			                                      //CfgDbMngr.Cfg.Server,
			                                      //CfgDbMngr.Cfg.DataBaseName,
			                                      //CfgDbMngr.Cfg.RealUser,
			                                      //CfgDbMngr.Cfg.RealPassword);
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
		
		/*
		public SesionMngr CreateSesion()
		{
			VerificarVersionDb();
			return new SesionMngr(this);
		}
		*/
		
		private SessionFn sesion = null;		
		public SessionFn Sesion
		{	
			get
			{
				//if (sesion == null)
				//	sesion = CreateSesion();
				return sesion;
			}
		}
		
		private DatabaseFn()
		{
			VerificarVersionDb();
			sesion = new SessionFn(this);
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

		public void VerificarVersionDb()
		{
			int versionDb;
			
			DbCommand cmd;
			try
			{
				cmd = CreateCommand(@"
Select 
	TS_VERSIONDB.VER
from
	TS_VERSIONDB
where
	TS_VERSIONDB.ID = 0");
			}
			catch (System.Configuration.ConfigurationException e)
			{
				throw new AccesoDbConfiguracionException("Error con el .Net Provider",e);
			}
			
			try
			{
				cmd.Connection.Open();			
				versionDb = (int)cmd.ExecuteScalar();
			}
			catch (FirebirdSql.Data.FirebirdClient.FbException e)
			{
				if (
				    (e.ErrorCode == -2146233087) && (
				                                     (e.Message.Contains("user name") && e.Message.Contains("password")) 
				                                     || (e.Message.Contains("usuario") && e.Message.Contains("clave"))
					))
					throw new AccesoDbPermisosException("Usuario y clave internos no válidos",e);
				else if ((e.ErrorCode == -2146233087) && (
				                                          (e.Message.Contains("file open"))
				                                          || (e.Message.Contains("archivo"))
				    ))
					throw new AccesoDbNoExisteException("No existe la base de datos",e);
				else if ((e.ErrorCode == -2146233087) && (
				                                          (e.Message.Contains("network request"))
				                                          || (e.Message.Contains("red") && e.Message.Contains("solicitud"))
				    ))
					throw new AccesoDbServidorNoEncontradoException("No se puede conectar con el servidor de la base de datos",e);
				else
					throw new AccesoDbInesperadoException("Error desconocido",e);
			}
			finally
			{
				cmd.Connection.Close();
			}
			
			if (versionDb != VersionEsperada) 
				throw new AccesoDbVersionException(
				                                             "La versión de la base de datos no corresponde con los binarios",
				                                             versionDb, 
				                                             VersionEsperada);
		}

		public void ActualizarDb()
		{
			Console.WriteLine("Aca se supone que tengo que actualizar o crear la base de datos");
			
		}
		
		public void CrearUsuario(string user, string password)
		{
			//TODO: Agregar algo para que no cualquiera pueda hace esto		
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
			                                   AmString.CalcularSHA1(password)));  
			cmd.Connection.Open();
			cmd.ExecuteNonQuery();
			cmd.Connection.Close();
		}
	}
}
