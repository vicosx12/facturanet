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

namespace FacturaNet.FnAccesoDb
{
	public static class Global
	{
		private static ConfiguracionFn configuracion;		
		internal static ConfiguracionFn Configuracion 
		{
			get { return configuracion; }
			private set { configuracion = value; }
		}
		
		public static void Init(ConfiguracionFn configuracion)
		{
			Configuracion = configuracion;
		}
		
		static internal readonly int VersionEsperada = 1;
		
/*		
		static private DatabaseFn database = null;		
		static public DatabaseFn DatabaseCAMBIAR
		{	
			get
			{
				//if (db == null)
				//	db = new DbMngr();
				return database;
			}
		}		
		static public void Init()
		{
			if (database == null)
				database = new DatabaseFn();
		}
		
		
*/
		
		
	}
}
