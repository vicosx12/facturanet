// Utiles.cs
//
//  Copyright (C) 2008 [name of author]
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
//

using System;
using System.Security.Cryptography;
using System.Text;

namespace AmUtil
{
	public class Util
	{
		public static string BytesToStr(byte[] bytes)
		{
			StringBuilder builder = new StringBuilder(bytes.Length);
			foreach (byte b in bytes)
				builder.Append(b.ToString("X2"));
			return builder.ToString();
		}
		public static string CalcularSHA1(string original)
		{
			SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();
			UTF8Encoding encoder = new UTF8Encoding();
			return BytesToStr(hasher.ComputeHash(encoder.GetBytes(original)));
		}
		public static void Log(string texto)
		{
			Console.WriteLine(texto);
		}

		private Util()
		{
		}
	}
}
