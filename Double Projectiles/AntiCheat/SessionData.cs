using System;
using System.IO;
using Assets.Scripts.Unity.UI_New.Coop;
using Assets.Scripts.Unity.UI_New.Main;
using Assets.Scripts.Unity.UI_New.Main.EventPanel;
using Assets.Scripts.Unity.UI_New.Odyssey;
using ConfigHelper;
using Harmony;
using MelonLoader;

namespace Double_Projectile_Mod.AnitCheat
{
	internal class SessionData
	{
		public bool IsCheating
		{
			get
			{
				return this.OdysseyChecker.IsInOdyssey || this.RaceChecker.IsInRace || this.CoopChecker.IsInPublicCoop;
			}
		}
		public OdysseyHandler OdysseyChecker { get; set; } = new OdysseyHandler();
		public RaceHandler RaceChecker { get; set; } = new RaceHandler();
		public CoopHandler CoopChecker { get; set; } = new CoopHandler();
		public bool MenuRan = false;
		public static SessionData CurrentSession
		{
			get
			{
				return SessionData.currentSession;
			}
			set
			{
				SessionData.currentSession = value;
			}
		}
		public void ResetCheatStatus()
		{
			this.RaceChecker.IsInRace = false;
			this.OdysseyChecker.IsInOdyssey = false;
			this.CoopChecker.IsInPublicCoop = false;
		}
		private static SessionData currentSession = new SessionData();
	}
	[HarmonyPatch(typeof(CoopQuickMatchScreen), "Open")]
	internal class CoopButtonChecker_OnClick
	{
		[HarmonyPostfix]
		internal static void Postfix()
		{
			SessionData.CurrentSession.CoopChecker.IsInPublicCoop = true;
		}
	}
	[HarmonyPatch(typeof(MainMenuEventPanel), "OpenRaceEventScreen")]
	internal class MainMenuEventPanel_OpenRaceEventScreen
	{
		[HarmonyPostfix]
		internal static void Postfix()
		{
			SessionData.CurrentSession.RaceChecker.IsInRace = true;
		}
	}
	[HarmonyPatch(typeof(OdysseyEventScreen), "Update")]
	internal class OdysseyEventScreen_Update
	{
		[HarmonyPostfix]
		internal static void Postfix()
		{
			bool flag = !SessionData.CurrentSession.OdysseyChecker.IsInOdyssey;
			if (flag)
			{
				SessionData.CurrentSession.OdysseyChecker.IsInOdyssey = true;
			}
		}
	}
	[HarmonyPatch(typeof(MainMenu), "OnEnable")]
	internal class MainMenu_OnEnable
	{
		[HarmonyPostfix]
		internal static void Postfix()
		{
			bool isCheating = SessionData.CurrentSession.IsCheating;
			if (isCheating)
			{
				SessionData.CurrentSession.ResetCheatStatus();
			}
			if (!SessionData.CurrentSession.MenuRan)
            {
				if (!Directory.Exists(Environment.CurrentDirectory + "\\Mods\\Double Projectiles"))
				{
					MelonLogger.Log("Directory Made");
					Directory.CreateDirectory(Environment.CurrentDirectory + "\\Mods\\Double Projectiles");
					Class1.ignoredtowers.Add("TackShooter-4");
					Class1.ignoredtowers.Add("TackShooter-5");
					Class1.ignoredtowers.Add("DartlingGunner-4");
					Class1.ignoredtowers.Add("DartlingGunner-5");
					Class1.ignoredtowers.Add("BananaFarm-03");
					Class1.ignoredtowers.Add("BananaFarm-04");
					Class1.ignoredtowers.Add("BananaFarm-05");
					Class1.ignoredtowers.Add("BananaFarm-13");
					Class1.ignoredtowers.Add("BananaFarm-23");
					Class1.ignoredtowers.Add("BananaFarm-14");
					Class1.ignoredtowers.Add("BananaFarm-24");
					Class1.ignoredtowers.Add("BananaFarm-15");
					Class1.ignoredtowers.Add("BananaFarm-25");
					Class1.ignoredtowers.Add("BananaFarm-13");
					Class1.ignoredtowers.Add("SuperMonkey-5");
					Class1.ignoredtowers.Add("SuperMonkey-4");
					Settings settings = Settings.LoadedSettings;
					settings.IgnoredTowers = Class1.ignoredtowers;
					Settings.LoadedSettings.Save();
				}
				else
				{

					Settings settings = Settings.LoadedSettings;
					Class1.ignoredtowers = settings.IgnoredTowers;
				}
				Class1.CreateAllTowersFile();
				MelonLogger.Log("Ignored Towers: ");
				foreach (string s in Class1.ignoredtowers)
				{
					MelonLogger.Log(s);
				}
				SessionData.CurrentSession.MenuRan = true;
			}
		}
	}
}
