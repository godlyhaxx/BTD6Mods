using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Unity.Bridge;
using Assets.Scripts.Unity.UI_New.InGame;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;

namespace BuffersNoAttack
{
    public class Main : BloonsTD6Mod
    {
        public override void OnGameModelLoaded(GameModel model)
        {
            base.OnGameModelLoaded(model);
            AttackModel a = null;
            bool b = false;
            foreach (TowerModel t in model.towers)
            {
                if (t.name.Contains("Village"))
                    if (t.tiers[1] == 5)
                        if (!b)
                            foreach (Model m in t.behaviors)
                                if (m.TryCast<AttackModel>() != null)
                                {
                                    a = m.TryCast<AttackModel>();
                                    b = true;
                                    break;
                                }
            }
            foreach (TowerModel t in model.towers)
            {
                if (t.name.Contains("Village") || t.name.Contains("Super"))
                    if (t.tiers[0] == 5)
                    {
                        t.RemoveBehaviors<AttackModel>();
                        t.AddBehavior(a);
                    }
                if (t.name.Contains("Pat"))
                {
                    t.RemoveBehaviors<AttackModel>();
                    t.AddBehavior(a);
                }
            }
        }
        public override void OnUpdate()
        {
            if (InGame.Bridge != null)
                foreach (AbilityToSimulation a in InGame.Bridge.GetAllAbilities(true))
                {
                    TowerModel m = a.Tower.tower.towerModel;
                    if (m.name.Contains("Engi") || m.name.Contains("Pat") || m.name.Contains("Village") || m.name.Contains("Benji"))
                    {
                        a.ability.RefreshCooldown();
                        a.ability.ClearCooldown();
                    }
                }
            base.OnUpdate();
        }
    }
}
