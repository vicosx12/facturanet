// Global.cs
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
using FacturaNet.FnConfiguracion;
using FacturaNet.FnAccesoDb;

namespace FacturaNet.FnNegocio
{
	public static class Global
	{
		private static ConfiguracionFn configuracion;
		public static ConfiguracionFn Configuracion 
		{
			get { return configuracion; }
			private set { configuracion = value; }
		}
		
		private static DatabaseFn database;
		public static DatabaseFn Database 
		{
			get { return database; }
			private set { database = value; }
		}
		
		static private SessionFn session;
		public static SessionFn Session 
		{
			get { return session; }
			private set { session = value; }
		}
		
		public static void Init(ConfiguracionFn configuracion, DatabaseFn database)
		{
			Configuracion = configuracion;
			Database = database;
			Session = Database.CreateSesion();
		}
	}
}
