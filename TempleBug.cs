using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Mutators;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using MelonLoader;
using TempleBug;

[assembly: MelonInfo(typeof(TempleBug.TempleBug), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TempleBug
{
    public class TempleBug : BloonsTD6Mod
    {
        private const string CHIMPS_MODE_ID = "Clicks";
        private const float CASH_MULTIPLIER = 1.5f;

        public override void OnApplicationStart()
        {
            ModHelper.Msg<TempleBug>("TempleBug loaded!");
        }

        public override void OnNewGameModel(GameModel result)
        {
            base.OnNewGameModel(result);

            if (!Settings.ModEnabled || InGameData.CurrentGame.selectedMode != CHIMPS_MODE_ID)
                return;

            ModHelper.Msg<TempleBug>($"Applying TempleBug - Setting temple BonusCashZoneModel multiplier to {CASH_MULTIPLIER}x");

            var superMonkeys = result.GetTowersWithBaseId("SuperMonkey");

            foreach (var tower in superMonkeys.ToList())
            {
                if (tower.tiers[0] == 4)
                {
                    var tierOneModel = tower.GetBehaviors<TempleTowerMutatorGroupTierOneModel>().Find(m => m.name == "TempleTowerMutatorGroupTierOneModel_Support_25001_");
                    if (tierOneModel == null) continue;

                    foreach (var mut in tierOneModel.mutators)
                    {
                        var addModel = mut.TryCast<AddBehaviorToTowerMutatorModel>();
                        if (addModel == null) continue;

                        foreach (var behavior in addModel.behaviors)
                        {
                            var bonusCashModel = behavior.TryCast<BonusCashZoneModel>();
                            if (bonusCashModel != null)
                            {
                                bonusCashModel.multiplier = CASH_MULTIPLIER;
                                break;
                            }
                        }
                    }
                } else if (tower.tiers[0] == 5)
                {
                    var tierTwoModel = tower.GetBehaviors<TempleTowerMutatorGroupTierTwoModel>().Find(m => m.name == "TempleTowerMutatorGroupTierTwoModel_Support_25001_");
                    if (tierTwoModel == null) continue;

                    foreach (var mut in tierTwoModel.mutators)
                    {
                        var addModel = mut.TryCast<AddBehaviorToTowerMutatorModel>();
                        if (addModel == null) continue;

                        foreach (var behavior in addModel.behaviors)
                        {
                            var bonusCashModel = behavior.TryCast<BonusCashZoneModel>();
                            if (bonusCashModel != null)
                            {
                                bonusCashModel.multiplier = CASH_MULTIPLIER;
                                break;
                            }
                        }
                    }
                }           
            }
        }
    }
}