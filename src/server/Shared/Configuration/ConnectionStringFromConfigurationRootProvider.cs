using System;
using Microsoft.Extensions.Configuration;

namespace PVDevelop.UCoach.Configuration
{
	public class ConnectionStringFromConfigurationRootProvider : IConnectionStringProvider
	{
		private readonly IConfigurationRoot _configurationRoot;
		private readonly string _name;

		public ConnectionStringFromConfigurationRootProvider(
			IConfigurationRoot configurationRoot,
			string name)
		{
			if (configurationRoot == null) throw new ArgumentNullException(nameof(configurationRoot));
			if (string.IsNullOrEmpty(name)) throw new ArgumentException("Not set", nameof(name));

			_configurationRoot = configurationRoot;
			_name = name;
		}

		public string ConnectionString => _configurationRoot.GetConnectionString(_name);
	}
}
