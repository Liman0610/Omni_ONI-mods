using HarmonyLib;
using TUNING;
using KMod;
using UTILS;

namespace LiquidSublimator
{
    public class Patches : UserMod2
    {
        [HarmonyPatch(typeof(GeneratedBuildings))]
        [HarmonyPatch("LoadGeneratedBuildings")]
        public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
        {
            public static void Prefix()
            {
                StringUtils.AddBuildingStrings("PollutedWaterSublimator", "Polluted Water Sublimator", "Turns the nasty water into its polluted oxygen form all for the cost of some power!", PollutedWaterSublimatorConfig.Effect);
                BuildingUtils.AddBuildingToPlanScreen((HashedString)"Oxygen", "PollutedWaterSublimator");
            }
        }

        [HarmonyPatch(typeof(Db))]
        [HarmonyPatch("Initialize")]
        public static class Db_Initialize_Patch
        {
            public static void Postfix() => BuildingUtils.AddBuildingToTechnology("DirectedAirStreams", "PollutedWaterSublimator");
        }
    }
}
