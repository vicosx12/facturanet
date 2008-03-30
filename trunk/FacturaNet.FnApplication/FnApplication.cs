// MyClass.cs
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

namespace FacturaNet.FnApplication
{
	
	
	public static class FnApplication
	{
		static private DatabaseFn database;
		static public DatabaseFn Database 
		{
			get { return database; }
			private set { database = value; }
		}
		
		static private SessionFn session;
		static public SessionFn Session 
		{
			get { return session; }
			private set { session = value; }
		}

		static private ConfiguracionFn configuracion;
		public static ConfiguracionFn Configuracion 
		{
			get { return configuracion; }
			private set { configuracion = value; } 
		}

		
		
		public static void Init(string[] args)
		{
			Configuracion = new ConfiguracionFn("facturanet.ini", args);
		}
		public static void Init()
		{
			Configuracion = new ConfiguracionFn("facturanet.ini");
		}				
		
	}
}
