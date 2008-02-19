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
        #region configuracion
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
		
		
		private string user = "";
		private string password = "";
		private bool conectado = false;

		private Sesion()
		{
		}
		public bool Conectado
		{
			get {return conectado;}
		}
		public DbConnection CreateConnection()
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
		public DbDataAdapter CreateDataAdapter()
		{
			return dbpFactory.CreateDataAdapter();
		}
		public DbDataAdapter CreateDataAdapter(string selectCommand)
		{
			DbDataAdapter da = CreateDataAdapter();
			DbCommand cmd = CreateCommand(selectCommand);
			da.SelectCommand = cmd;
			return da;
		}
		public DbCommand CreateCommand()
		{
			DbCommand cmd = dbpFactory.CreateCommand();
			cmd.Connection = CreateConnection();
			return cmd;
		}
		public DbCommand CreateCommand(string commandText)
		{
			DbCommand cmd = CreateCommand();
			cmd.CommandText = commandText;
			return cmd;
		}
		public DbParameter CreateParameter()
		{
			return dbpFactory.CreateParameter();
		}
		public DbParameter CreateParameter(string parameterName, DbType dbType)
		{
			DbParameter par = CreateParameter();
			par.ParameterName = parameterName;
			par.DbType = dbType;
			return par;
		}
		public DbParameter CreateParameter(string parameterName, DbType dbType, object value)
		{
			DbParameter par = CreateParameter(parameterName, dbType);
			par.Value = value;
			return par;
		}
		public bool ReConectar()
		{
			conectado = false;
			DbCommand cmd = CreateCommand("SPS_VRF_USUARIO"); 
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(CreateParameter(
			                                   "@NB_USUARIO",
			                                   DbType.String,
			                                   user));  
			cmd.Parameters.Add(CreateParameter(
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

		public void CrearUsuario(string user, string password)
		{
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
