// CfgDbMngrException.cs
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
	public abstract class CfgDbMngrException : ApplicationException
	{
		public CfgDbMngrException(string message) : base(message)
		{
		}
		public CfgDbMngrException(
		                       string message, 
		                       Exception inner) : base(message, inner)
		{
		}	
	}
	
	public class CfgDbMngrNoSeleccionadaException : CfgDbMngrException
	{
		public CfgDbMngrNoSeleccionadaException(string message) : base(message)
		{
		}
	}
	
	public class CfgDbMngrSeccionNoDefinidaException : CfgDbMngrException
	{
		public CfgDbMngrSeccionNoDefinidaException(string message) : base(message)
		{
		}
	}
	
	public class CfgDbMngrErrorFormatoConfigException : CfgDbMngrException
	{
		public CfgDbMngrErrorFormatoConfigException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
