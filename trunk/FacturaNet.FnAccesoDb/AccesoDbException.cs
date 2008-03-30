// DbMngrException.cs
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

namespace FacturaNet.FnAccesoDb
{
	public abstract class AccesoDbException : ApplicationException
	{
		public AccesoDbException(string message) : base(message)
		{
		}
		public AccesoDbException(
		                       string message, 
		                       Exception inner) : base(message, inner)
		{
		}	
	}

	public class AccesoDbConfiguracionException : AccesoDbException
	{
		public AccesoDbConfiguracionException(
		                                 string message, 
		                                 Exception inner) : base(message, inner)
		{
		}
	}
	
	public class AccesoDbPermisosException : AccesoDbException
	{
		public AccesoDbPermisosException(
		                                 string message, 
		                                 Exception inner) : base(message, inner)
		{
		}
	}
	
	public class AccesoDbNoExisteException : AccesoDbException
	{
		public AccesoDbNoExisteException(
		                                 string message, 
		                                 Exception inner) : base(message, inner)
		{
		}
	}
	
	public class AccesoDbServidorNoEncontradoException : AccesoDbException
	{
		public AccesoDbServidorNoEncontradoException(
		                                 string message, 
		                                 Exception inner) : base(message, inner)
		{
		}
	}

	public class AccesoDbInesperadoException : AccesoDbException
	{
		public AccesoDbInesperadoException(
		                                 string message, 
		                                 Exception inner) : base(message, inner)
		{
		}
	}
	
	public class AccesoDbVersionException : AccesoDbException
	{
		private int versionDb;
		private int versionEsperada;

		public int VersionDb
		{
			get { return versionDb;}
		}
		
		public int VersionEsperada
		{
			get { return versionEsperada;}
		}

		private void SetVersiones(int versionDb, int versionEsperada)
		{
			this.versionDb = versionDb;
			this.versionEsperada = versionEsperada;
		}

		public AccesoDbVersionException(
		                                          string message, 
		                                          int versionDb, 
		                                          int versionEsperada) : base(message)
		{
			SetVersiones(versionDb, versionEsperada);
		}
	}
}
