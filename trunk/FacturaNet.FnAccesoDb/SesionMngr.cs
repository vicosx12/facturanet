using System;
using System.Data;
using System.Data.Common;
using AmUtil;

namespace FacturaNet.FnAccesoDb
{
	public class SesionMngr
	{
		static private SesionMngr sesion = null;		
		static public SesionMngr Sesion
		{	
			get
			{
				if (sesion == null)
					sesion = DbMngr.Db.CreateSesion();
				return sesion;
			}
		}		
		
		private DbMngr db;
		
		internal SesionMngr(DbMngr db)
		{
			this.db = db;
		}
		
		private string user = "";
		private string password = "";
		
		private bool conectado = false;
		public bool Conectado
		{
			get {return conectado;}
		}
		
		public DbConnection CreateConnection()
		{
			return Conectado? db.CreateConnection() : null;
		}
		public DbDataAdapter CreateDataAdapter()
		{
			return Conectado? db.CreateDataAdapter() : null;
		}
		public DbDataAdapter CreateDataAdapter(string selectCommand)
		{
			return Conectado? db.CreateDataAdapter(selectCommand) : null;
		}
		public DbCommand CreateCommand()
		{
			return Conectado? db.CreateCommand() : null;
		}
		public DbCommand CreateCommand(string commandText)
		{
			return Conectado? db.CreateCommand(commandText) : null;
		}
		public DbParameter CreateParameter()
		{
			return Conectado? db.CreateParameter() : null;
		}
		public DbParameter CreateParameter(string parameterName, DbType dbType)
		{
			return Conectado? db.CreateParameter(parameterName, dbType) : null;
		}
		public DbParameter CreateParameter(string parameterName, DbType dbType, object value)
		{
			return Conectado? db.CreateParameter(parameterName, dbType, value) : null;
		}
		
		public bool ReConectar()
		{
			conectado = false;
			DbCommand cmd = db.CreateCommand("SPS_VRF_USUARIO"); 
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(db.CreateParameter(
			                                   "@NB_USUARIO",
			                                   DbType.String,
			                                   user));  
			cmd.Parameters.Add(db.CreateParameter(
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
	}
}
