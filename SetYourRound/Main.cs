using MelonLoader;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.Bridge;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using System.Linq;
using Assets.Scripts.Models;
using UnhollowerBaseLib;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Unity;
using System.Collections.Generic;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.GenericBehaviors;
using NKHook6.Api.Extensions;
using NKHook6.Api.Events;
using Assets.Scripts.Unity.UI_New.Popups;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Unity.Map;
using TMPro;
using Assets.Scripts.Models.Store;
using System;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Simulation.Towers.Projectiles;
using Assets.Scripts.Models.Bloons;
using MelonLoader.ICSharpCode.SharpZipLib.Tar;

namespace Free5KTrophies
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            EventRegistry.instance.listen(typeof(Main));
            MelonLogger.Log("using this mod will probably hackerpool you");
            MelonLogger.Log("Press F1 to get 5000 trophies");
        }
        [NKHook6.Api.Events.Event("KeyPressEvent")]
        public static void onEvent(KeyEvent e)
        {
            string key = e.key + "";
            if (key == "F1")
            GameExt.getBtd6Player(Game.instance).GainTrophies(5000, "race");
        }
    }
}
