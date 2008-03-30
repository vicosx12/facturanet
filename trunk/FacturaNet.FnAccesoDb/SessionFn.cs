using System;
using System.Data;
using System.Data.Common;
using AmUtil;

namespace FacturaNet.FnAccesoDb
{
	public class SessionFn
	{
		/*
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
		*/
		
		private DatabaseFn database;
		
		internal SessionFn(DatabaseFn db)
		{
			this.database = db;
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
			return Conectado? database.CreateConnection() : null;
		}
		public DbDataAdapter CreateDataAdapter()
		{
			return Conectado? database.CreateDataAdapter() : null;
		}
		public DbDataAdapter CreateDataAdapter(string selectCommand)
		{
			return Conectado? database.CreateDataAdapter(selectCommand) : null;
		}
		public DbCommand CreateCommand()
		{
			return Conectado? database.CreateCommand() : null;
		}
		public DbCommand CreateCommand(string commandText)
		{
			return Conectado? database.CreateCommand(commandText) : null;
		}
		public DbParameter CreateParameter()
		{
			return Conectado? database.CreateParameter() : null;
		}
		public DbParameter CreateParameter(string parameterName, DbType dbType)
		{
			return Conectado? database.CreateParameter(parameterName, dbType) : null;
		}
		public DbParameter CreateParameter(string parameterName, DbType dbType, object value)
		{
			return Conectado? database.CreateParameter(parameterName, dbType, value) : null;
		}
		
		public bool ReConectar()
		{
			conectado = false;
			DbCommand cmd = database.CreateCommand("SPS_VRF_USUARIO"); 
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(database.CreateParameter(
			                                   "@NB_USUARIO",
			                                   DbType.String,
			                                   user));  
			cmd.Parameters.Add(database.CreateParameter(
			                                   "@CLAVE",
			                                   DbType.String,
			                                   AmString.CalcularSHA1(password)));  
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
