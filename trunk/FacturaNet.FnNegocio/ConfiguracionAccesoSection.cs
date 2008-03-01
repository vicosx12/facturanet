// ConfiguracionAccesoSection.cs
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
using System.Configuration;

namespace FacturaNet.FnNegocio
{
	public sealed class ConfiguracionAccesoSection : ConfigurationSection
	{
		//HACK: Hay que mejorar este enchastre
	    private static ConfigurationPropertyCollection _Properties;
		private static bool _ReadOnly;

		private static readonly ConfigurationProperty _ProviderName =
	        new ConfigurationProperty("ProviderName", 
	        typeof(string),"", 
	        ConfigurationPropertyOptions.IsRequired);
	    private static readonly ConfigurationProperty _CnnString =
	        new ConfigurationProperty("CnnString", 
	        typeof(string),"", 
	        ConfigurationPropertyOptions.IsRequired);
		private static readonly ConfigurationProperty _Server =
	        new ConfigurationProperty("Server", 
	        typeof(string),"", 
	        ConfigurationPropertyOptions.IsRequired);
	    private static readonly ConfigurationProperty _DataBase =
	        new ConfigurationProperty("DataBase", 
	        typeof(string),"", 
	        ConfigurationPropertyOptions.IsRequired);
		private static readonly ConfigurationProperty _RealUser =
	        new ConfigurationProperty("RealUser", 
	        typeof(string),"", 
	        ConfigurationPropertyOptions.IsRequired);
	    private static readonly ConfigurationProperty _RealPassword =
	        new ConfigurationProperty("RealPassword", 
	        typeof(string),"", 
	        ConfigurationPropertyOptions.IsRequired);

		
		public ConfiguracionAccesoSection()
	    {
	        _Properties = 
	            new ConfigurationPropertyCollection();

	        _Properties.Add(_ProviderName);
	        _Properties.Add(_CnnString);
	        _Properties.Add(_Server);
			_Properties.Add(_DataBase);
	        _Properties.Add(_RealUser);
	        _Properties.Add(_RealPassword);
	   }

		protected override ConfigurationPropertyCollection Properties
	    {
	        get
	        {
	            return _Properties;
	        }
	    }
	    private new bool IsReadOnly
	    {
	        get
	        {
	            return _ReadOnly;
	        }
	    }

	    private void ThrowIfReadOnly(string propertyName)
	    {
	        if (IsReadOnly)
	            throw new ConfigurationErrorsException(
	                "The property " + propertyName + " is read only.");
	    }
	    protected override object GetRuntimeObject()
	    {
	        _ReadOnly = true;
	        return base.GetRuntimeObject();
	    }
	    public string ProviderName
	    {
	        get { return (string)this["ProviderName"]; }
	        set
	        {
	            ThrowIfReadOnly("ProviderName");
	            this["ProviderName"] = value;
	        }
	    }

	    public string CnnString
	    {
	        get { return (string)this["CnnString"]; }
	        set
	        {
	            ThrowIfReadOnly("CnnString");
	            this["CnnString"] = value;
	        }
	    }

	    public string Server
	    {
	        get { return (string)this["Server"]; }
	        set
	        {
	            ThrowIfReadOnly("Server");
	            this["Server"] = value;
	        }
	    }

	    public string DataBase
	    {
	        get { return (string)this["DataBase"]; }
	        set
	        {
	            ThrowIfReadOnly("DataBase");
	            this["DataBase"] = value;
	        }
	    }
	    public string RealPassword
	    {
	        get { return (string)this["RealPassword"]; }
	        set
	        {
	            ThrowIfReadOnly("RealPassword");
	            this["RealPassword"] = value;
	        }
	    }
	    public string RealUser
	    {
	        get { return (string)this["RealUser"]; }
	        set
	        {
	            ThrowIfReadOnly("RealUser");
	            this["RealUser"] = value;
	        }
	    }
	}
}
