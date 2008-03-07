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
	public abstract class DbMngrException : ApplicationException
	{
		public DbMngrException()
		{
		}		
		public DbMngrException(string message) : base(message)
		{
		}
		public DbMngrException(
		                       string message, 
		                       Exception inner) : base(message, inner)
		{
		}	
	}
	
	public class DbMngrNoAccesoDbException : DbMngrException
	{
		public DbMngrNoAccesoDbException()
		{
		}		
		public DbMngrNoAccesoDbException(string message) : base(message)
		{
		}
		public DbMngrNoAccesoDbException(
		                                 string message, 
		                                 Exception inner) : base(message, inner)
		{
		}
	}

	public class DbMngrNoExisteDbException : DbMngrException
	{
		public DbMngrNoExisteDbException()
		{
		}		
		public DbMngrNoExisteDbException(string message) : base(message)
		{
		}
		public DbMngrNoExisteDbException(
		                                 string message, 
		                                 Exception inner) : base(message, inner)
		{
		}
	}
	
	public class DbMngrVersionDbIncorrectaException : DbMngrException
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
		
		public DbMngrVersionDbIncorrectaException(int versionDb, int versionEsperada)
		{
			SetVersiones(versionDb, versionEsperada);
		}		
		
		public DbMngrVersionDbIncorrectaException(
		                                          string message, 
		                                          int versionDb, 
		                                          int versionEsperada) : base(message)
		{
			SetVersiones(versionDb, versionEsperada);
		}
		public DbMngrVersionDbIncorrectaException(
		                                          string message, 
		                                          Exception inner, 
		                                          int versionDb, 
		                                          int versionEsperada) : base(message, inner)
		{
			SetVersiones(versionDb, versionEsperada);
		}
	}
	
	public class DbMngrPermisosDbException : DbMngrException
	{
		public DbMngrPermisosDbException()
		{
		}		
		public DbMngrPermisosDbException(string message) : base(message)
		{
		}
		public DbMngrPermisosDbException(
		                                 string message, 
		                                 Exception inner) : base(message, inner)
		{
		}
	}
}
