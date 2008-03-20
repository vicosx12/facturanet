// Opciones.cs
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
using System.Reflection;
using Commons.GetOptions;

namespace FacturaNet.FnMngr
{
	public class Opciones : Options
	{
		[Option ("Actualiza la base de datos.", ShortForm='u', LongForm="actualizar_db")]
		public bool actualizar_db 
		{
			set { Modo = ModoDeEjecucion.Actualizar_db; }
		}
		[Option ("Agrega una configuración para acceder a la base de datos.", ShortForm='a', LongForm="agregar_acceso_db")]
		public bool agregar_acceso_db
		{
			set { Modo = ModoDeEjecucion.Agregar_acceso_db; }
		}
		[Option ("Crea un usuario para el sistema.", ShortForm='c', LongForm="crear_usuario")]
		public bool crear_usuario
		{
			set { Modo = ModoDeEjecucion.Crear_usuario; }
		}
		[Option ("Selecciona uno de los accesos a base de datos configurados.", ShortForm='s', LongForm="sel_acceso_db")]
		public bool sel_acceso_db
		{
			set { Modo = ModoDeEjecucion.Seleccionar_acceso_db; }
		}
		[KillInheritedOption]
		public override WhatToDoNext DoHelp2() { return WhatToDoNext.GoAhead; }
		[KillInheritedOption]
		public override WhatToDoNext DoUsage() { return WhatToDoNext.GoAhead; }

		private ModoDeEjecucion modo = ModoDeEjecucion.NoAsignado;
		public ModoDeEjecucion Modo 
		{
			get { return modo; }
			set
			{
				if (modo != ModoDeEjecucion.NoAsignado)
					modo = ModoDeEjecucion.Invalido;
				else
					modo = value;
			}
			
		}

		/*
		[Option ("Read from the specified file.", ShortForm='i', LongForm="input-file")]		
		public readonly string InputFile; //Si no se ingresa es nulo, si se ingresa sin contenido es "", ojo, si se ingresa -i -o toma -o como el valor de -i 
		[Option ("Write to the specified file.", ShortForm = 'o', LongForm = "output-file")]
		public readonly string OutputFile;// = "a";
		[Option ("don't print status messages to stdout", 'q')]
		public bool quiet;
		*/
		
		public readonly string AccesoDb_Nombre;
		public readonly string AccesoDb_ProviderName;
		public readonly string AccesoDb_CnnString;
		public readonly string AccesoDb_Server;
		public readonly string AccesoDb_DataBase;
		public readonly string AccesoDb_RealUser;
		public readonly string AccesoDb_RealPassword;

		public readonly string Usuario_Nombre;
		public readonly string Usuario_Clave;

		private void AyudaModo()
		{
			//HACK: Mejorar esto
			switch (Modo)
			{
				case ModoDeEjecucion.Actualizar_db :
					Console.WriteLine("Actualiza la base de datos a la versión correspondiente a la versión del ejecutable");
					break;
				case ModoDeEjecucion.Agregar_acceso_db :
					Console.WriteLine(
					                  @"Configura una base de datos para el sistema.
	Parametros:
		AccesoDb_Nombre
		AccesoDb_ProviderName
		AccesoDb_CnnString
		AccesoDb_Server
		AccesoDb_DataBase
		AccesoDb_RealUser
		AccesoDb_RealPassword");
					break;
				case ModoDeEjecucion.Crear_usuario :
					Console.WriteLine(
					                  @"Crea un usuario para el sistema en la base de datos actual.
	Parametros:
		Usuario_Nombre
		Usuario_Clave");
					break;
				case ModoDeEjecucion.Seleccionar_acceso_db :
					Console.WriteLine(
					                  @"Cambia el acceso a la base de datos a utlizar.
	Parametros:
		AccesoDb_Nombre");
					break;
				case ModoDeEjecucion.Invalido :
					Console.WriteLine("Solo puede seleccionarse un modo.");
					break;
				case ModoDeEjecucion.NoAsignado:
					Console.WriteLine("Debe seleccionarse un modo.");
					break;
					
			}
			System.Environment.Exit(1);
		}
		
		public Opciones(string[] args) : base ()		                          
		{
			base.ParsingMode = OptionsParsingMode.GNU_DoubleDash;
			base.ProcessArgs(args);
			//HACK: Mejorar esto
			if (
			    (RemainingArguments.Length > 0) 
			    && (RemainingArguments[0].ToUpper() == "HELP"))
				AyudaModo();
			else
			{
				switch (Modo)
				{
					case ModoDeEjecucion.Actualizar_db :
						if (RemainingArguments.Length != 0) 
							AyudaModo();
						break;
					case ModoDeEjecucion.Agregar_acceso_db :
						if (RemainingArguments.Length != 7) 
							AyudaModo();
						AccesoDb_Nombre = RemainingArguments[0];
						AccesoDb_ProviderName = RemainingArguments[1];
						AccesoDb_CnnString = RemainingArguments[2];
						AccesoDb_Server = RemainingArguments[3];
						AccesoDb_DataBase = RemainingArguments[4];
						AccesoDb_RealUser = RemainingArguments[5];
						AccesoDb_RealPassword = RemainingArguments[6];
						break;
					case ModoDeEjecucion.Crear_usuario :
						if (RemainingArguments.Length != 2) 
							AyudaModo();
						Usuario_Nombre = RemainingArguments[0];
						Usuario_Clave = RemainingArguments[1];
						break;
					case ModoDeEjecucion.Seleccionar_acceso_db :
						if (RemainingArguments.Length != 1) 
							AyudaModo();
						AccesoDb_Nombre = RemainingArguments[0];
						break;
					default :
						AyudaModo();	
						break;
				}
			}
		}
	}
}
