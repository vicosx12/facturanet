// AmString.cs
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

namespace AmUtil
{
	//TODO hacer esto como metodos de extension
	public static class AmString
	{
		public static string PonerComillas (/*this*/ string self)
		{
			return "\"" + AmString.SacarComillas(self).Replace("\"","\"\"") + "\""; 
		}
		
		public static string SacarComillas (/*this*/ string self)
		{
			if (self.StartsWith("\"") && self.EndsWith("\""))
			{
				self.Replace("\"\"","\"");
				return self.Substring(1,self.Length-2);
			}
			else
				return self;
		}
		
		public static string Encriptar (/*this*/ string desencriptado, string key, string IV)
		{
			return Util.EncriptarDesencriptar(desencriptado, true, key, IV);
		}
		public static string Desencriptar (/*this*/ string encriptado, string key, string IV)
		{
			return Util.EncriptarDesencriptar(encriptado, false, key, IV);
		}
		
		public static string CalcularSHA1 (/*this*/ string original)
		{
			return Util.CalcularSHA1(original);
		}
	}
}
