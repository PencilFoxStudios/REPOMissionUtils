using HarmonyLib;

namespace MissionUtils;

[HarmonyPatch(typeof(MissionUI))]
public class SpectateCameraPatch
{ 
    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    private static void Update_Postfix(MissionUI __instance)
    {
        if (MissionUtilsConfigActivator.PencilConfig.DeadPlayersCanSeeMissions)
        {
            __instance.Show();
        }
    }
}