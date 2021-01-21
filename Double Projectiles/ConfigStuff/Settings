using Double_Projectile_Mod;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigHelper
{
	internal class Settings
	{
		public static string settingsPath = Class1.path;
		public static Settings LoadedSettings
		{
			get
			{
				return (Settings.settings != null) ? Settings.settings : Settings.LoadSettings();
			}
			set
			{
				Settings.settings = value;
			}
		}
		public List<string> IgnoredTowers { get; set; } = new List<string>();

		public static Settings LoadSettings()
		{
			Settings.settings = (File.Exists(Settings.settingsPath) ? Serializer.LoadFromFile<Settings>(Settings.settingsPath) : Settings.CreateNewSettingsFile());
			return Settings.settings;
		}
		public static Settings CreateNewSettingsFile()
		{
			Settings settings = new Settings();
			settings.Save();
			return settings;
		}
		public void Save()
		{
			Serializer.SaveToFile<Settings>(Settings.settings, Settings.settingsPath, true);
		}
		private static Settings settings;
	}
}
