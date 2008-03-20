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
			Opciones opciones = new Opciones(args);
			
			switch (opciones.Modo)
			{
				case ModoDeEjecucion.Actualizar_db :
					DbMngr.Db.ActualizarDb();
					break;
				case ModoDeEjecucion.Agregar_acceso_db :
					CfgDbMngr.Cfg.AgregarAccesoDb(
					                              opciones.AccesoDb_Nombre,
					                              opciones.AccesoDb_ProviderName,
					                              opciones.AccesoDb_CnnString,
					                              opciones.AccesoDb_Server,
					                              opciones.AccesoDb_DataBase,
					                              opciones.AccesoDb_RealUser,
					                              opciones.AccesoDb_RealPassword);
					
					break;
				case ModoDeEjecucion.Crear_usuario :
					// FIXME: Esto debería hacerse desde otro lugar, no es muy seguro permitir crear usuarios asi nomas...
					DbMngr.Db.CrearUsuario(
					                       opciones.Usuario_Nombre, 
					                       opciones.Usuario_Clave);
					break;
				case ModoDeEjecucion.Seleccionar_acceso_db :
					CfgDbMngr.Cfg.SeleccionarAccesoDb(
					                                  opciones.AccesoDb_Nombre);					
					break;
			}
			/*
			Assembly a = Assembly.GetExecutingAssembly();
			//string [] resNames = a.GetManifestResourceNames();
			Console.WriteLine((a.GetManifestResourceStream("recurso.txt")).Length);
			*/
		}
	}
}