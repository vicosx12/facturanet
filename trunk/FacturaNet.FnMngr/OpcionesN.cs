// OpcionesN.cs
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

namespace FacturaNet.FnMngr
{
	public class OpcionesN
	{
		private ArgvConfigSource cmdCfgSrc;
		private IConfig cmdCfg
		{
			get 
			{
				if (cmdCfgSrc == null) 
					return null;
				else
					return cmdCfgSrc.Configs["cmdline"]; 
			} 
		}
		
		private ModoDeEjecucion _modo = ModoDeEjecucion.NoAsignado;
		public ModoDeEjecucion Modo
		{
			get { return _modo; }
			private set
			{
				if (_modo != ModoDeEjecucion.NoAsignado)
					_modo = ModoDeEjecucion.Invalido;
				else
					_modo = value;
			}
		}
		
		public OpcionesN(string[] args)
		{
			cmdCfgSrc = new ArgvConfigSource(args);
			cmdCfgSrc.AddConfig("cmdline");	
			cmdCfgSrc.AddSwitch("cmdline", "actualizar_db", "u");
			cmdCfgSrc.AddSwitch("cmdline", "agregar_acceso_db", "a");
			cmdCfgSrc.AddSwitch("cmdline", "crear_usuario", "c");
			cmdCfgSrc.AddSwitch("cmdline", "sel_acceso_db", "s");

			cmdCfg.Alias.AddAlias("actualizar_db",false);
			
			if (cmdCfg.GetBoolean("actualizar_db"))
				Modo = ModoDeEjecucion.Actualizar_db;
			if (cmdCfg.GetBoolean("agregar_acceso_db",false))
				Modo = ModoDeEjecucion.Agregar_acceso_db;
			if (cmdCfg.GetBoolean("crear_usuario",false))
				Modo = ModoDeEjecucion.Crear_usuario;
			if (cmdCfg.GetBoolean("sel_acceso_db",false))
				Modo = ModoDeEjecucion.Seleccionar_acceso_db;
			
			
		}
	}
}
