using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Mutators;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
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

            foreach (var tower in superMonkeys)
            {
                if (tower.tiers[0] != 4 && tower.tiers[0] != 5)
                    continue;

                string mutatorModelName = tower.tiers[0] == 4
                    ? "TempleTowerMutatorGroupTierOneModel_Support_4001_"
                    : "TempleTowerMutatorGroupTierTwoModel_Support_4001_";

                var model = tower.GetBehaviors<TempleTowerMutatorGroupTierOneModel>()
                    .Find(model => model.name == mutatorModelName);

                if (model == null)
                    continue;

                int mutatorIndex = model.mutators.FindIndex(m => m.name == "AddBehaviorToTowerMutatorModel_4001_");
                if (mutatorIndex == -1)
                    continue;

                var mutator = model.mutators[mutatorIndex].TryCast<AddBehaviorToTowerMutatorModel>();
                if (mutator == null) continue;

                foreach (var behaviour in mutator.behaviors)
                {
                    var bonusCashModel = behaviour.TryCast<BonusCashZoneModel>();
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