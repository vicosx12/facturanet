// Coneccion.cs created with MonoDevelop
// User: andres at 20:16 29/01/2008
//
//
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using AmUtil;

namespace FacturaNet.FnNegocio
{
	public class Sesion
	{
		static public readonly int VersionDbEsperada = 1;
		
#region Configuración estatica
		static private Configuration config = null;
		static private string GetSesionCfg(string configuracion)
		{
			if (config == null) 
				config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			//config.AppSettings.Settings.Add("a", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
			//config.Save(ConfigurationSaveMode.Modified);
			return config.AppSettings.Settings[configuracion].Value;			
		}
		static private string providerName 
		{
			get { return GetSesionCfg("ProviderName"); } 
		}
		static private string realUser
		{
			get { return GetSesionCfg("RealUser"); }
		}
		static private string realPassword 
		{
			get { return GetSesionCfg("RealPassword"); }
		}
		static private string server 
		{
			get { return GetSesionCfg("Server"); }
		}
		static private string dataBase 
		{
			get { return GetSesionCfg("DataBase"); }
		}
		static private string cnnString 
		{
			get { return GetSesionCfg("CnnString"); }
		} 
#endregion
		
#region estaticos (los que no varían si está conectado o no o cual es el usuario)  
		static private DbProviderFactory _dbpFactory = null;
		static private DbProviderFactory dbpFactory 
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
		static internal DbConnection createConnection()
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
		static internal DbDataAdapter createDataAdapter()
		{
			return dbpFactory.CreateDataAdapter();
		}
		static internal DbDataAdapter createDataAdapter(string selectCommand)
		{
			DbDataAdapter da = createDataAdapter();
			DbCommand cmd = createCommand(selectCommand);
			da.SelectCommand = cmd;
			return da;
		}
		static internal DbCommand createCommand()
		{
			DbCommand cmd = dbpFactory.CreateCommand();
			cmd.Connection = createConnection();
			return cmd;
		}
		static internal DbCommand createCommand(string commandText)
		{
			DbCommand cmd = createCommand();
			cmd.CommandText = commandText;
			return cmd;
		}
		static internal DbParameter createParameter()
		{
			return dbpFactory.CreateParameter();
		}
		static internal DbParameter createParameter(string parameterName, DbType dbType)
		{
			DbParameter par = createParameter();
			par.ParameterName = parameterName;
			par.DbType = dbType;
			return par;
		}
		static internal DbParameter createParameter(string parameterName, DbType dbType, object value)
		{
			DbParameter par = createParameter(parameterName, dbType);
			par.Value = value;
			return par;
		}
		static public int FillUsuarios(DataTable tablaUsuarios)
		{
			DbDataAdapter da = createDataAdapter(
@"
SELECT  
	NB_USUARIO,
	DES_USUARIO
FROM
	SPS_LST_USUARIOS");
			return da.Fill(tablaUsuarios);
		}
#endregion
		
#region singleton		
		static private Sesion sesion = null;		
		static public Sesion SesionSingleton
		{	
			get
			{
				if (sesion == null)
					sesion = new Sesion();
				return sesion;
			}
		}
		private Sesion()
		{
		}
#endregion		
		
#region sesion propiamente dicha
		private string user = "";
		private string password = "";
		
		private bool conectado = false;
		public bool Conectado
		{
			get {return conectado;}
		}
		
		public DbConnection CreateConnection()
		{
			return Conectado? createConnection() : null;
		}
		public DbDataAdapter CreateDataAdapter()
		{
			return Conectado? createDataAdapter() : null;
		}
		public DbDataAdapter CreateDataAdapter(string selectCommand)
		{
			return Conectado? createDataAdapter(selectCommand) : null;
		}
		public DbCommand CreateCommand()
		{
			return Conectado? createCommand() : null;
		}
		public DbCommand CreateCommand(string commandText)
		{
			return Conectado? createCommand(commandText) : null;
		}
		public DbParameter CreateParameter()
		{
			return Conectado? createParameter() : null;
		}
		public DbParameter CreateParameter(string parameterName, DbType dbType)
		{
			return Conectado? createParameter(parameterName, dbType) : null;
		}
		public DbParameter CreateParameter(string parameterName, DbType dbType, object value)
		{
			return Conectado? createParameter(parameterName, dbType, value) : null;
		}
		
		public bool ReConectar()
		{
			conectado = false;
			DbCommand cmd = createCommand("SPS_VRF_USUARIO"); 
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(createParameter(
			                                   "@NB_USUARIO",
			                                   DbType.String,
			                                   user));  
			cmd.Parameters.Add(createParameter(
			                                   "@CLAVE",
			                                   DbType.String,
			                                   Util.CalcularSHA1(password)));  
			cmd.Connection.Open();
			conectado = (int)cmd.ExecuteScalar() == 1;
			cmd.Connection.Close();
			if (conectado) 
				Util.Log("Usuario autentificado"); 
			else
				Util.Log("Falló autentificando usuario");
			return conectado;
		}

		public void Desconectar()
		{
			user = "";
			password = "";
			conectado = false;
		}
		
		public bool Conectar(string user, string password)
		{   
			this.user = user;
			this.password = password;
			return ReConectar(); 			
		}
#endregion		
	}
}
