using BTD_Mod_Helper.Api.Data;
using BTD_Mod_Helper.Api.ModOptions;

namespace TempleBug
{
    public class Settings : ModSettings
    {
        public static readonly ModSettingBool ModEnabled = new(true)
        {
            description = "While enabled, in CHIMPS mode temples & tsg with atleast $4,001 support sacrifice will have their 1.5x BonusCashZoneModel active. Requires being active when joining the match."
        };
    }
}
