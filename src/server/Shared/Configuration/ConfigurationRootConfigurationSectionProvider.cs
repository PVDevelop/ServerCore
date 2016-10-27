using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace PVDevelop.UCoach.Configuration
{
	public class ConfigurationRootConfigurationSectionProvider<TSection> : IConfigurationSectionProvider<TSection>
		where TSection : class, new()
	{
		private readonly IConfigurationRoot _configurationRoot;
		private readonly string _sectionName;

		public ConfigurationRootConfigurationSectionProvider(
			IConfigurationRoot configurationRoot,
			string sectionName)
		{
			if (configurationRoot == null) throw new ArgumentNullException(nameof(configurationRoot));
			if (string.IsNullOrWhiteSpace(sectionName)) throw new ArgumentException("Not set", nameof(sectionName));

			_configurationRoot = configurationRoot;
			_sectionName = sectionName;
		}

		public TSection GetSection()
		{
			var result = new TSection();

			foreach (var property in _configurationRoot.GetSection(_sectionName).GetChildren())
			{
				SetValue(result, property);
			}

			return result;
		}

		private void SetValue(
			TSection target,
			IConfigurationSection configurationSection)
		{
			var property = typeof(TSection).GetRuntimeProperty(configurationSection.Key);
			var value =
				property.PropertyType == typeof(string) ? 
				configurationSection.Value :
				JsonConvert.DeserializeObject(configurationSection.Value, property.PropertyType);
			property.SetValue(target, value);
		}
	}
}
