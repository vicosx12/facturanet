// Coneccion.cs created with MonoDevelop
// User: andres at 20:16 29/01/2008
//
//
using System;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using AmUtil;

namespace FacturaNet.FnNegocio
{
	public class Sesion
	{
		static private string realUser = GetSesionCfg("RealUser");
		static private string realPassword = GetSesionCfg("RealPassword");	
		static private string server = GetSesionCfg("Server");
		static private string dataBase = GetSesionCfg("DataBase");
		
		static private string GetSesionCfg(string configuracion)
		{
			//TODO: Sacar estos datos de un archivo de configuracion
			switch (configuracion)
			{
				case "RealUser" : 
					return "SYSDBA";
				case "RealPassword" :
					return "masterkey";
				case "Server" :
					return "ubandres";
				case "DataBase" :
					return "prbgtk";
				default :
					throw new Exception("Parámetro de configuración no encontrado."); 
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

		static public int FillUsuarios(DataTable tablaUsuarios)
		{
			FbDataAdapter dataAdapter = new FbDataAdapter(
@"
SELECT  
	NB_USUARIO,
	DES_USUARIO
FROM
	SPS_LST_USUARIOS",
			                                              _GetFbConnection());
			return dataAdapter.Fill(tablaUsuarios);
		}
		
		
		private string user = "";
		private string password = "";
		private RolDb rolDb = RolDb.NoAsignado;

		private Sesion()
		{
		}
		
		private string StrRol
		{
			get 
			{
				// TODO: definir los roles en la db para que no pueda hacer cualquier cosa
				switch (rolDb)
				{
				case RolDb.NoAsignado : 
					return "";
					//return "Role=ROL_SINASIGNAR";
				case RolDb.UsuarioStandard :
					return ""; 					 
					//return "Role=ROL_USUARIO;";
				default :
					return "";//"Role=ROL_SINPERMISOS;";
				}
			}
		}
		
			
		static private FbConnection _GetFbConnection(string strRol)
		{
			return new FbConnection(
				                        "User=" + realUser + ";" +
				                        "Password=" + realPassword + ";" +
				                        "Database=" + dataBase + ";" +
				                        "DataSource=" + server + ";" +
				                        strRol);
		}
			
		static private FbConnection _GetFbConnection()
		{
			return _GetFbConnection("");				                       
		}
		public RolDb RolDb
		{
			get { return this.rolDb; }				
		}

		public FbConnection GetFbConnection()
		{
			return Sesion._GetFbConnection(StrRol);
		}

		public bool ReConectar()
		{
			rolDb = RolDb.NoAsignado;
			FbCommand cmd = new FbCommand();
			cmd.Connection = GetFbConnection();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@NB_USUARIO",user);
			cmd.Parameters.Add("@CLAVE",password);			
			cmd.CommandText = "SPS_VRF_USUARIO";
			cmd.Connection.Open();
			rolDb = (RolDb)(int)cmd.ExecuteScalar();
			cmd.Connection.Close();
			if (rolDb == RolDb.NoEsUsuario) 
				Util.Log("Falló autentificando usuario"); 
			else
				Util.Log("Usuario autentificado");
			return rolDb != RolDb.NoEsUsuario;
		}

		public void Desconectar()
		{
			user = "";
			password = "";
			rolDb = RolDb.NoAsignado;
		}
		
		public bool Conectar(string user, string password)
		{   
			this.user = user;
			this.password = password;
			return ReConectar(); 			
		}
	}
}
