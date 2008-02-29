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
using AmUtil;

namespace FacturaNet.FnNegocio
{
	
	
	public class AdministradorDb
	{
		public AdministradorDb()
		{
		}
		
		public void ActualizarDb()
		{
		}
		
		public void CrearUsuario(string user, string password)
		{
			// TODO: Agregar algo para verificar que el usuario actual puede hacer esto y que está conectado		
			DbCommand cmd = Sesion.createCommand("SPS_NEW_USUARIO");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add(Sesion.createParameter(
			                                   "@NB_USUARIO",
			                                   DbType.String,
			                                   user));
			cmd.Parameters.Add(Sesion.createParameter(
			                                   "@DES_USUARIO",
			                                   DbType.String,
			                                   user));
			cmd.Parameters.Add(Sesion.createParameter(
			                                   "@CLAVE",
			                                   DbType.String,
			                                   Util.CalcularSHA1(password)));  
			cmd.Connection.Open();
			cmd.ExecuteNonQuery();
			cmd.Connection.Close();
		}

	}
}
