using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Emissions;
using Assets.Scripts.Models.Towers.Weapons;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.UI_New.InGame;
using Double_Projectile_Mod.AnitCheat;
using Harmony;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Double_Projectile_Mod
{
    public class Class1 : MelonMod
    {
        public static void CreateAllTowersFile()
        {
            List<string> s = new List<string>();
            foreach (TowerModel tower in Game.instance.model.towers.ToList<TowerModel>())
            {
                Model model = tower.TryCast<Model>();
                if (model != null)
                {
                    s.Add(model.name);
                }
            }
            string ss = "";
            for (int i = 0; i < s.Count; i++)
            {
                ss = ss + s[i] + "\n";
            }
            string newpath = Environment.CurrentDirectory + "\\Mods\\Double Projectiles\\AllTowers.txt";
            File.WriteAllText(newpath, ss);
        }
        public static string path = Environment.CurrentDirectory + "\\Mods\\Double Projectiles\\Config.txt";
        public static List<string> ignoredtowers = new List<string>();
        public override void OnApplicationStart()
        {
            path = Environment.CurrentDirectory + "\\Mods\\Double Projectiles\\Config.txt";
            
            MelonLogger.Log("Double Projectile Mod has loaded");
            MelonLogger.Log("On Upgrade, projectiles will double");
        }
        public static TowerModel IncreaseProj(string name)
        {
            TowerModel towermodel = Game.instance.model.towers.FirstOrDefault((TowerModel t) => t.name.Contains(name)).CloneAs<TowerModel>();
            bool ignoretowerrot = false;
            bool useprojrot = false;
            AttackModel attack;
            if (!name.Contains("MonkeyAce"))
                attack = towermodel.behaviors.FirstOrDefault((Model atk) => atk.name.Contains("AttackModel")).Cast<AttackModel>();
            else
            {
                foreach (Model behav in towermodel.behaviors)
                {
                        AttackModel model = behav.TryCast<AttackModel>();
                    if (model != null)
                    {
                        foreach (WeaponModel weapon in model.weapons)
                        {
                            //MelonLogger.Log(weapon.emission.ToString());
                            if (weapon.emission.TryCast<ArcEmissionModel>() != null)
                            {
                                ArcEmissionModel arc = weapon.emission.TryCast<ArcEmissionModel>();
                                if (name.Contains("-"))
                                {
                                    arc.count *= 2;
                                }
                                else
                                {
                                    arc.count *= 2;
                                }
                                weapon.emission = arc;
                            }
                            if (weapon.emission.TryCast<EmissionWithOffsetsModel>() != null)
                            {
                                EmissionWithOffsetsModel emission = weapon.emission.TryCast<EmissionWithOffsetsModel>();
                                if (name.Contains("-"))
                                {
                                    emission.projectileCount *= 2;
                                }
                                else
                                {
                                    emission.projectileCount *= 2;
                                }
                                weapon.emission = emission;
                            }
                        }
                    }
                    
                }
                return towermodel;
            }
            foreach (Model behav in towermodel.behaviors)
            {
                attack = behav.TryCast<AttackModel>();
                if (attack != null)
                {
                    foreach (WeaponModel weapon in attack.weapons)
                    {
                        if (name.Contains("DartlingGunner"))
                            ignoretowerrot = true;
                        if (!name.Contains("TackShooter") && !name.Contains("DartlingGunner"))
                        {
                            
                            bool hero = false;
                            int projectiles = 1;
                            if (name.Contains(InGame.instance.SelectedHero))
                            {
                                hero = true;
                                string[] herolvl = name.Split(' ');
                                projectiles += Int32.Parse(herolvl[1]);
                            }
                            if (name.Contains("Acadamy") || name.Contains("Pirate King") || name.Contains("Arch God") || name.Contains("BallOfLight") || name.Contains("Big Plane") || name.Contains("Bloon Exterminator") || name.Contains("BuccaneerGreater") || name.Contains("BuccaneerLesser") || name.Contains("CommancheDefenceHeli") || name.Contains("Darkest Legend") || name.Contains("Divine Creation") || name.Contains("Drone") || name.Contains("DroneSwarmDrone") || name.Contains("Explosive Impact") || name.Contains("Lord Phoenix") || name.Contains("Marine") || name.Contains("Pheonix") || name.Contains("Spectre"))
                                projectiles = 7;
                            if (name.Contains("-") && !name.Contains("HeliPilot"))
                            {
                                if (!hero)
                                {
                                    string[] split = name.Split('-');
                                    string tier = split[1];
                                    char[] tiers = tier.ToCharArray();
                                    for (int i = 0; i < 3; i++)
                                    {
                                        string b = tiers[i].ToString();
                                        projectiles += Int32.Parse(b);
                                    }
                                }
                                if (weapon.emission.TryCast<ArcEmissionModel>() != null)
                                {
                                    ArcEmissionModel arc = weapon.emission.Cast<ArcEmissionModel>();
                                    arc.count = projectiles * 2;
                                    arc.angle = arc.count * 10;
                                    arc.name = $"ArcEmission{arc.count}";
                                    weapon.emission = arc;
                                }
                                else
                                {
                                    weapon.emission = new ArcEmissionModel($"ArcEmission{projectiles * 2}", projectiles * 2, 0f, 2 * projectiles * 10f, null, ignoretowerrot, useprojrot);
                                }

                            }
                            else if (!name.Contains("-"))
                            {
                                if (weapon.emission.TryCast<ArcEmissionModel>() != null)
                                {
                                    ArcEmissionModel arc = weapon.emission.Cast<ArcEmissionModel>();
                                    arc.count = projectiles * 2;
                                    arc.angle = arc.count * 10;
                                    arc.name = $"ArcEmission{arc.count}";
                                    weapon.emission = arc;
                                }
                                else
                                {
                                    weapon.emission = new ArcEmissionModel($"ArcEmission{projectiles * 2}", projectiles * 2, 0f, 2 * projectiles * 10f, null, ignoretowerrot, useprojrot);
                                }
                            }
                            else if (name.Contains("HeliPilot"))
                            {
                                EmissionWithOffsetsModel emission = weapon.emission.TryCast<EmissionWithOffsetsModel>();
                                if (emission != null)
                                {
                                    emission.projectileCount = projectiles * 2;
                                    emission.randomRotationCone = 15f;
                                }
                                RandomEmissionModel random = weapon.emission.TryCast<RandomEmissionModel>();
                                if (random != null)
                                {
                                    random.count = projectiles * 2;
                                    random.ejectPointRandomness = 15f;
                                }
                            }
                        }
                        else
                        {
                            int projmultiply = 1;
                            if (name.Contains("-"))
                            {
                                Random random = new Random();
                                string[] split = name.Split('-');
                                string tier = split[1];
                                char[] tiers = tier.ToCharArray();
                                for (int i = 0; i < 3; i++)
                                {
                                    string b = tiers[i].ToString();
                                    projmultiply += Int32.Parse(b);
                                }
                                bool LockedSpread = false;
                                bool disablenewspread = false;
                                if (name.Contains("HeliPilot") || name.Contains("DartlingGunner") || name.Contains("WizardMonkey"))
                                {
                                    LockedSpread = true;
                                }
                                if (name.Contains("TackShooter"))
                                    disablenewspread = true;
                                if (weapon.emission.TryCast<ArcEmissionModel>() != null)
                                {
                                    ArcEmissionModel arc = weapon.emission.Cast<ArcEmissionModel>();
                                    arc.count *= projmultiply;
                                    if (!LockedSpread)
                                        arc.angle = arc.count * 10;
                                    arc.name = $"ArcEmission{arc.count}";
                                    if (!name.Contains("DartlingGunner"))
                                        weapon.emission = arc;
                                    if (name.Contains("DartlingGunner"))
                                        weapon.emission = new RandomArcEmissionModel($"ArcEmission{projmultiply}", projmultiply, 15f, 0f, 0f, -15f, null);
                                }
                                else
                                {
                                    projmultiply *= 6;
                                    float angle = 360;
                                    if (name.Contains("Heli"))
                                        angle = 0;
                                    if (!disablenewspread)
                                        angle = projmultiply * 10f;
                                    if (!name.Contains("DartlingGunner"))
                                        weapon.emission = new ArcEmissionModel($"ArcEmission{projmultiply}", projmultiply, 0f, angle, null, ignoretowerrot, useprojrot);
                                    if (name.Contains("DartlingGunner"))
                                        weapon.emission = new RandomArcEmissionModel($"ArcEmission{projmultiply}", projmultiply, 0f, 0f, 15f, 0f, null);
                                }
                            }
                        }
                    }
                }
            }
            return towermodel;
        }
        [HarmonyPatch(typeof(Tower), "UpdatedModel")]
        internal class Tower_UpdatedModel
        {
            [HarmonyPrefix]
            internal static bool Prefix(ref Model modelToUse)
            {
                if (SessionData.CurrentSession.IsCheating)
                    return true;
                string name = modelToUse.name;
                foreach (string s in ignoredtowers)
                {
                    if (name.Contains(s))
                        return true;
                }
                TowerModel tower = IncreaseProj(name);
                modelToUse = tower.Cast<Model>();
                return true;
            }
        }
    }
    public static class Extension
    {
        public static T CloneAs<T>(this T reference) where T : Model
        {
            return reference.Clone().Cast<T>();
        }
    }
}
