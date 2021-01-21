using MelonLoader;
using Harmony;
using System;
using Assets.Scripts.Simulation.Towers.Weapons;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.Bridge;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using System.Linq;
using Assets.Scripts.Models;
using UnhollowerBaseLib;
using Il2CppSystem.Runtime.InteropServices;
using UnhollowerBaseLib.Runtime;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Simulation.SMath;
using Assets.Scripts.Unity;
using System.Collections.Generic;
using Assets.Scripts.Unity.UI_New.Popups;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;

namespace ConfigHelper
{
	internal class Serializer
	{
		public static T LoadFromFile<T>(string filePath) where T : class
		{
			bool flag = !Serializer.IsPathValid(filePath);
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				string value = File.ReadAllText(filePath);
				bool flag2 = string.IsNullOrEmpty(value);
				if (flag2)
				{
					result = default(T);
				}
				else
				{
					result = JsonConvert.DeserializeObject<T>(value);
				}
			}
			return result;
		}
		private static bool IsPathValid(string filePath)
		{
			Guard.ThrowIfStringIsNull(filePath, "Can't load file, path is null");
			bool flag = !File.Exists(filePath);
			return !flag;
		}
		public static void SaveToFile<T>(T jsonObject, string savePath, bool overwriteExisting = true) where T : class
		{
			Guard.ThrowIfStringIsNull(savePath, "Can't save file, save path is null");
			Serializer.CreateDirIfNotFound(savePath);
			string value = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
			bool append = !overwriteExisting;
			StreamWriter streamWriter = new StreamWriter(savePath, append);
			streamWriter.Write(value);
			streamWriter.Close();
		}
		private static void CreateDirIfNotFound(string dir)
		{
			FileInfo fileInfo = new FileInfo(dir);
			Directory.CreateDirectory(fileInfo.Directory.FullName);
		}
	}
}
