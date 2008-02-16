// Coneccion.cs created with MonoDevelop
// User: andres at 20:16 29/01/2008
//
//
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;
using AmUtil;

namespace FacturaNet.FnNegocio
{
	public class Sesion
	{
		static private string providerName {get{return GetSesionCfg("ProviderName");}}
		
		static private string realUser {get{return GetSesionCfg("RealUser");}}
		static private string realPassword {get{return GetSesionCfg("RealPassword");}}
		static private string server {get{return GetSesionCfg("Server");}}
		static private string dataBase {get{return GetSesionCfg("DataBase");}}
		static private string cnnString {get{return GetSesionCfg("CnnString");}} 
		static private DbProviderFactory _dbpFactory 
		{
			get
			{
				Console.WriteLine();Console.WriteLine();
				foreach (DataRow row in DbProviderFactories.GetFactoryClasses().Rows)
					Console.WriteLine("{0}\t{1}\t{2}\t{3}\n",row[0], row[1], row[2], row[3]);
				Console.WriteLine();Console.WriteLine();
				
				return DbProviderFactories.GetFactory(providerName);
			}
		}

		static private string GetSesionCfg(string configuracion)
		{
			System.Configuration.Configuration config =
				ConfigurationManager.OpenExeConfiguration(
				      ConfigurationUserLevel.None);
			//config.AppSettings.Settings.Add("a", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
			//config.Save(ConfigurationSaveMode.Modified);
			return config.AppSettings.Settings[configuracion].Value;			
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
		//private RolDb rolDb = RolDb.NoAsignado;
		private bool conectado = false;

		private Sesion()
		{
		}
		
		/*
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
		*/
		/*	
		static private FbConnection _GetFbConnection(string strRol)
		{
			return new FbConnection(
				                        "User=" + realUser + ";" +
				                        "Password=" + realPassword + ";" +
				                        "Database=" + dataBase + ";" +
				                        "DataSource=" + server + ";" +
				                        strRol);
		}
		*/
		
		static private FbConnection _GetFbConnection()
		{
			return new FbConnection(
			                        string.Format(
			                                      cnnString,
			                                      server,
			                                      dataBase,
			                                      realUser,
			                                      realPassword));				                       
		}

/*		public RolDb RolDb
		{
			get { return this.rolDb; }				
		}
*/
		public bool Conectado
		{
			get {return conectado;}
		}
		public FbConnection GetFbConnection()
		{
			return _GetFbConnection();
		}

		public bool ReConectar()
		{
			conectado = false;
			FbCommand cmd = new FbCommand();
			cmd.Connection = GetFbConnection();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@NB_USUARIO",user);
			cmd.Parameters.Add("@CLAVE",Util.CalcularSHA1(password));
			cmd.CommandText = "SPS_VRF_USUARIO";
			cmd.Connection.Open();
			conectado = (int)cmd.ExecuteScalar() == 1;
			//rolDb = (RolDb)(int)cmd.ExecuteScalar();
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
			FbCommand cmd = new FbCommand();
			cmd.Connection = GetFbConnection();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "SPS_NEW_USUARIO";
			cmd.Parameters.Add("@NB_USUARIO",user);
			cmd.Parameters.Add("@DES_USUARIO",user);
			cmd.Parameters.Add("@CLAVE",Util.CalcularSHA1(password));
			cmd.Connection.Open();
			cmd.ExecuteNonQuery();
			cmd.Connection.Close();
		}
	}
}
