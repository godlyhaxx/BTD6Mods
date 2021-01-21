using Assets.Scripts.Models;
using Assets.Scripts.Models.Bloons;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Upgrades;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Bridge;
using Assets.Scripts.Unity.Map;
using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.UI_New.Popups;
using MelonLoader;
using NKHook6.Api.Events;
using NKHook6.Api.Extensions;
using System;
using System.Collections.Generic;

namespace CheatHotKeys
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            EventRegistry.instance.listen(typeof(Main));
            MelonLogger.Log("using this mod will probably hackerpool you");
        }
        public static bool UpgradesLock = false;
        public static bool free = false;
        public static string Held = "";
        [EventAttribute("KeyHeldEvent")]
        public static void onHoldEvent(KeyEvent e)
        {
            string key = e.key + "";
            if (key == "LeftShift")
            {
                Held = "LeftShift";
            }
        }
        [EventAttribute("KeyReleaseEvent")]
        public static void onReleaseEvent(KeyEvent e)
        {
            string key = e.key + "";
            if (key == "LeftShift")
            {
                Held = "";
            }
        }
        [NKHook6.Api.Events.Event("KeyPressEvent")]
        public static void onEvent(KeyEvent e)
        {
            string key = e.key + "";
            if (Held != "")
            {
                key = Held + key;
                HeldDown(key);
                return;
            }
            if (key == "F1")
            {
                GameExt.getBtd6Player(Game.instance).GainTrophies(500, "race");
            }
            if (key == "F2")
            {
                GameExt.getBtd6Player(Game.instance).debugUnlockAllUpgrades = true;
                GameExt.getBtd6Player(Game.instance).debugUnlockAllTowers = true;
            }
            if (key == "F2")
            {
                GameExt.getBtd6Player(Game.instance).debugUnlockAllModes = true;
                
            }
            if (key == "F3")
            {
                GameExt.getBtd6Player(Game.instance).GainMonkeyMoney(2500, "race");
            }
            InGame game = InGame.instance;
            if (game != null)
            {
                if (key == "F5")
                {
                    game.setCash(game.getCash() + 5000);
                }
                if (key == "F6")
                {
                    game.setHealth(game.getHealth() + 500);
                }
                if (key == "F7")
                {
                    float Value = 0;
                    foreach (BloonToSimulation bloon in InGameExt.getBloons(game))
                    {
                        if (bloon != null)
                        {
                            Value += bloon.Def.maxHealth;
                            if (bloon.Def.childBloonModels != null)
                            {
                                foreach (BloonModel bloon2 in bloon.Def.childBloonModels)
                                {
                                    if (bloon2 != null)
                                    {
                                        if (bloon2.childBloonModels != null)
                                        {
                                            Value += bloon2.maxHealth;
                                            foreach (BloonModel bloon3 in bloon2.childBloonModels)
                                            {
                                                if (bloon3 != null)
                                                {
                                                    if (bloon3.childBloonModels != null)
                                                    {
                                                        Value += bloon3.maxHealth;
                                                        foreach (BloonModel bloon4 in bloon3.childBloonModels)
                                                        {
                                                            if (bloon4 != null)
                                                            {
                                                                if (bloon4.childBloonModels != null)
                                                                {
                                                                    Value += bloon4.maxHealth;
                                                                    foreach (BloonModel bloon5 in bloon4.childBloonModels)
                                                                    {
                                                                        if (bloon5 != null)
                                                                        {
                                                                            if (bloon5.childBloonModels != null)
                                                                            {
                                                                                Value += bloon5.maxHealth;
                                                                                foreach (BloonModel bloon6 in bloon5.childBloonModels)
                                                                                {
                                                                                    if (bloon6 != null)
                                                                                    {
                                                                                        Value += bloon6.maxHealth;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            game.addCash(Value * 1.85f);
                        }
                    }
                    game.DeleteAllBloons();
                }
                if (key == "F8")
                {
                    game.lockHeroPurchases = false;
                    game.lockTowerPurchases = false;
                    game.lockTowerUpgrades = false;
                }
                if (key == "F9")
                {
                    foreach (AbilityToSimulation ability in InGameExt.getAbilities(game))
                    {
                        ability.ResetCooldown();
                    }
                }
            }
            if (game == null)
            {
                if (key == "F4")
                {
                    if (!free)
                    {
                        free = true;
                        MelonLogger.Log("Towers & upgrades are gonna be free");
                    }
                    else if (free)
                    {
                        free = false;
                        MelonLogger.Log("Towers & upgrades are no longer gonna be free");
                    }
                    free = true;
                }
            }
            if (key == "F10")
            {
                Help(1);
            }
            if (key == "F11")
            {
                Help(2);
            }
            if (key == "F12")
            {
                Help(3);
            }
        }
        public static void Help(int type)
        {
            string monkname = "";
            System.Action<string> action = delegate (string s)
            {
                monkname = s;
            };
            switch (type)
            {
                case 1:
                    PopupScreen.instance.ShowSetNamePopup("Help (outside of game)", "F1 = 500 trophies\nF2 = unlock all game modes\nF3 = 2500 Monkey money\nF4 = Toggle free upgrades and towers\nDont fill box", action, "");
                    PopupScreen.instance.GetFirstActivePopup().background = PopupScreen.BackGround.Grey;
                    break;
                case 2:
                    PopupScreen.instance.ShowSetNamePopup("Help (inside of game)", "F5 = 5000 cash\nF6 = 500 health\nF7 = pop all balloons\nF8 = unlock all upgrades and towers temporarily\nF9 = Resets Cooldowns\nDont fill box", action, "");
                    PopupScreen.instance.GetFirstActivePopup().background = PopupScreen.BackGround.Grey;
                    break;
                case 3:
                    PopupScreen.instance.ShowSetNamePopup("Help (While Holding Left Shift)", "F1 = 1 KnowledgePoints\nF2 = 1 Random Insta Tower\nF3 = gives 1 - 15 random insta towers\nDont fill box", action, "");
                    PopupScreen.instance.GetFirstActivePopup().background = PopupScreen.BackGround.Grey;
                    break;
            }
        }
        public static void HeldDown(string key)
        {
            Random random = new Random();
            InGame Ingame = InGame.instance;
            Game game = Game.instance;
            if (key.Contains("F1"))
            {
                GameExt.getProfileModel(game).KnowledgePoints += 1;
            }
            if (key.Contains("F2"))
            {
                string AddInsta = GetRandTower();
                int[] ranks = GetInstaRanks(random.Next(3));
                if (AddInsta == "")
                    AddInsta = GetRandTower();
                game.getBtd6Player().AddInstaTower(AddInsta, ranks, 1);
                MelonLogger.Log(AddInsta + "-" + $"{ranks[0]}-{ranks[1]}-{ranks[2]}");
            }
            if (key.Contains("F3"))
            {
                string prev = "";
                for (int i = 0; i < random.Next(5, 15); i++)
                {
                    int num = random.Next(1, 15);
                    bool r = random.Next(2) == 0;
                    bool r2 = random.Next(2) == 0;
                    string AddInsta = GetRandTower();
                    if (r)
                        AddInsta = GetRandTower();
                    int[] ranks = GetInstaRanks(random.Next(3));
                    if (r2)
                        ranks = GetInstaRanks(random.Next(3));
                    if (AddInsta == "" || AddInsta == prev)
                        AddInsta = GetRandTower();
                    prev = AddInsta;
                    game.getBtd6Player().AddInstaTower(AddInsta, ranks, num);
                    MelonLogger.Log(AddInsta + "-" + $"{ranks[0]}-{ranks[1]}-{ranks[2]} x{num}");
                }
            }
        }
        public static int[] GetInstaRanks(int num)
        {
            Random rand1 = new Random();
            Random rand2 = new Random();
            Random rand3 = new Random();
            int T1 = 0;
            int T2 = 0;
            int T3 = 0;
            if (num == 0)
            {
                T1 = rand1.Next(6);
                if (T1 > 2)
                {
                    bool r = rand2.Next(2) == 0;
                    if (r)
                    {
                        T2 = rand3.Next(3);
                    }
                    else
                    {
                        T3 = rand1.Next(3);
                    }
                }
                else
                {
                    bool r = rand2.Next(2) == 0;
                    if (r)
                    {
                        T2 = rand3.Next(3);
                    }
                    else
                    {
                        T3 = rand1.Next(3);
                    }
                }
            }
            else if (num == 1)
            {
                T2 = rand2.Next(6);
                if (T2 > 2)
                {
                    bool r = rand3.Next(2) == 0;
                    if (r)
                    {
                        T1 = rand1.Next(3);
                    }
                    else
                    {
                        T3 = rand2.Next(3);
                    }
                }
                else
                {
                    bool r = rand3.Next(2) == 0;
                    if (r)
                    {
                        T1 = rand1.Next(3);
                    }
                    else
                    {
                        T3 = rand2.Next(3);
                    }
                }
            }
            else
            {
                T3 = rand3.Next(6);
                if (T3 > 2)
                {
                    bool r = rand1.Next(2) == 0;
                    if (r)
                    {
                        T2 = rand2.Next(3);
                    }
                    else
                    {
                        T1 = rand3.Next(3);
                    }
                }
                else
                {
                    bool r = rand1.Next(2) == 0;
                    if (r)
                    {
                        T1 = rand2.Next(3);
                    }
                    else
                    {
                        T2 = rand3.Next(3);
                    }
                }
            }
            int[] res = { T1, T2, T3};
            return res;
        }
        public static string GetRandTower()
        {
            Random rand = new Random();
            string result = "";
            switch (rand.Next(23))
            {
                case 0:
                    result = "EngineerMonkey";
                    break;
                case 1:
                    result = "DartMonkey";
                    break;
                case 2:
                    result = "BoomerangMonkey";
                    break;
                case 3:
                    result = "BombShooter";
                    break;
                case 4:
                    result = "TackShooter";
                    break;
                case 5:
                    result = "IceMonkey";
                    break;
                case 6:
                    result = "GlueGunner";
                    break;
                case 7:
                    result = "SniperMonkey";
                    break;
                case 8:
                    result = "MonkeySub";
                    break;
                case 9:
                    result = "MonkeyBuccaneer";
                    break;
                case 10:
                    result = "MonkeyAce";
                    break;
                case 11:
                    result = "HeliPilot";
                    break;
                case 12:
                    result = "MortarMonkey";
                    break;
                case 13:
                    result = "DartlingGunner";
                    break;
                case 14:
                    result = "WizardMonkey";
                    break;
                case 15:
                    result = "SuperMonkey";
                    break;
                case 16:
                    result = "NinjaMonkey";
                    break;
                case 17:
                    result = "Alchemist";
                    break;
                case 18:
                    result = "Druid";
                    break;
                case 19:
                    result = "BananaFarm";
                    break;
                case 20:
                    result = "SpikeFactory";
                    break;
                case 21:
                    result = "MonkeyVillage";
                    break;
            }
            return result;
        }
        [Harmony.HarmonyPatch(typeof(MapLoader), "Load")]
        public class MapLoader_Path
        {
            [Harmony.HarmonyPostfix]
            public static void Postfix()
            {
                if (free)
                {
                    foreach (TowerModel tower in Game.instance.model.towers)
                    {
                        tower.cost = 0;
                    }
                    foreach (UpgradeModel upgradeModel in Game.instance.model.upgrades)
                    {
                        upgradeModel.cost = 0;
                    }
                }
            }
        }
    }
}
