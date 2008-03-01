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
using System.IO;
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
			return Convert.ToBase64String(hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(original)));
		}
		
		private static SymmetricAlgorithm _mCSP = null;
		private static SymmetricAlgorithm mCSP 
		{
			get
			{
				if (_mCSP == null)
				{
					_mCSP = new DESCryptoServiceProvider();
					mCSP.Key = Encoding.UTF8.GetBytes("bws623er");
					mCSP.IV = Encoding.UTF8.GetBytes("ma82ge4a");					
				}
				return _mCSP;
			}
		}
		private static ICryptoTransform _encriptador = null;
		private static ICryptoTransform encriptador
		{
			get
			{
				if (_encriptador == null)
					_encriptador = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);
			}
		}
		private static ICryptoTransform _desencriptador = null; 
		private static ICryptoTransform desencriptador
		{
			get
			{
				if (_desencriptador == null)
					_desencriptador = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);
			}
		}
		public static string EncriptacionPropia(string original)
		{
			MemoryStream ms = new MemoryStream(); 
			CryptoStream cs = new CryptoStream(
			                      ms, 
			                      encriptador,
			                      CryptoStreamMode.Write);
			cs.Write(Encoding.UTF8.GetBytes(original), 0, Encoding.UTF8.GetBytes(original).Length);
			cs.FlushFinalBlock();
			cs.Close();
			return Convert.ToBase64String(ms.ToArray());
		}
		
		public static string DesencriptacionPropia(string encriptado)
		{
			MemoryStream ms = new MemoryStream(); 
			CryptoStream cs = new CryptoStream(
			                      ms,
			                      desencriptador,
			                      CryptoStreamMode.Write);
			cs.Write(Convert.FromBase64String(encriptado), 0, Convert.FromBase64String(encriptado).Length);
			cs.FlushFinalBlock();
			cs.Close();
			return Encoding.UTF8.GetString(ms.ToArray());
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
