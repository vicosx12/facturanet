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
using Nini.Config;

namespace FacturaNet.FnConfiguracion
{
	public sealed class Configuracion
	{
		IConfigSource source;
		
#region AccesoDb
		public string AccesoDbSelectedProvider
		{
			get { return source.Configs["AccesoDb"].GetString("SelectedProvider"); } 
		}		
		private IConfig ConfiguracionSelectedProvider
		{
			get { return source.Configs["AccesoDb_" + AccesoDbSelectedProvider]; } 
		}
	    public string AccesoDbCnnString
		{
			get { return ConfiguracionSelectedProvider.GetString("CnnString"); } 
		}
	    public string AccesoDbServer
		{
			get { return ConfiguracionSelectedProvider.GetString("Server"); } 
		}
	    public string AccesoDbDataBase
		{
			get { return ConfiguracionSelectedProvider.GetString("DataBase"); } 
		}
	    public string AccesoDbRealPassword
		{
			get { return ConfiguracionSelectedProvider.GetString("Password"); }
			//encriptado debería dejar las claves de encriptacion solamente en la librería que se conecta a la base de datos
		} 
	    public string AccesoDbRealUser
		{
			get { return ConfiguracionSelectedProvider.GetString("CnnString"); } 
		} 
#endregion	
		public Configuracion(string archivo)
		{
			source = new IniConfigSource(archivo);		
		}
		public Configuracion() : this("FacturaNet.ini")
		{}
		//TODO tengo que terminar esta clase y permitir leer los valores desde la linea de comandos y grabarlos
	}
}
