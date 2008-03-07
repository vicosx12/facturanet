// Main.cs
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
using FacturaNet.FnAccesoDb;
using AmUtil;
using System.Reflection;

namespace FacturaNet.FnMngr
{
	class FnMngr
	{
		public static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				switch (args[0])
				{					
					//HACK: hay que mejorar la lectura de las opciones de linea de comandos
					case "--actualizar_db" :
						DbMngr.Db.ActualizarDb();
						break;
					case "--crear_usuario" : 
						// FIXME: Esto debería hacerse desde otro lugar
						DbMngr.Db.CrearUsuario(args[1], args[2]);
						break;
					case "--help" :
						Console.WriteLine("AYUDA");
						break;
					case "--agregar_acceso_db" :
						CfgDbMngr.Cfg.AgregarAccesoDb(
						                             args[1], //Nombre
						                             args[2], //ProviderName
						                             args[3], //CnnString
						                             args[4], //Server
						                             args[5], //DataBase
						                             args[6], //RealUser
						                             args[7]  //RealPassword
						                          );
						break;
					case "--crear_acceso_defecto" :
						CfgDbMngr.Cfg.AgregarAccesoDb(
						                             "firebird", 
						                             "FirebirdSql.Data.FirebirdClient",
						                             "a",//DataSource={0};Database={1};UserID={2};Password={3}", 
						                             "localhost",
						                             "facturanet",
						                             "SYSDBA", 
						                             "masterkey"
						                          );
						CfgDbMngr.Cfg.SeleccionarAccesoDb(args[1]);
						break;	
					case "--seleccionar_acceso_db" :
						CfgDbMngr.Cfg.SeleccionarAccesoDb(args[1]);
						break;	
					case "--prb1" :
						string e = Util.EncriptacionPropia("andrés");
						Console.WriteLine(e);
						Console.WriteLine(Util.DesencriptacionPropia(e));
						break;		
					case "--prb2" :
						Console.WriteLine(Util.CalcularSHA1("0"));
						break;		
					case "--prb3" :
						Assembly a = Assembly.GetExecutingAssembly();
						//string [] resNames = a.GetManifestResourceNames();
						Console.WriteLine((a.GetManifestResourceStream("recurso.txt")).Length);
						break;		
					default :
						Console.WriteLine("Parametro desconocido");
						break;
				}
			}
		}
	}
}