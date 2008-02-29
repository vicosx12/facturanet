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
using FacturaNet.FnNegocio;
using AmUtil;


namespace FacturaNet.FnMngr
{
	class FnMngr
	{
/*		
		public static void CrearUsuario(string user, string password)
		{
			Sesion.SesionSingleton.CrearUsuario(args[1],args[2]);
			Console.WriteLine("Usuario creado exitosamente");
		}
*/		
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
					default :
						Console.WriteLine("Parametro desconocido, se continua con el modo gráfico");
						break;
				}
			}
		}
	}
}