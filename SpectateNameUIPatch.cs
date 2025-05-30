using HarmonyLib;
using MissionUtils;

[HarmonyPatch(typeof(SpectateNameUI))]
public class SpectateNameUIPatch
{
    private static string playerName = "";

    [HarmonyPatch("SetName")]
    [HarmonyPrefix]
    static void SetName_Prefix(SpectateNameUI __instance, ref string name)
    {
        playerName = name;
        string missionText = MissionUI.instance.Text.text;
        if (MissionUtilsConfigActivator.PencilConfig.DeadPlayersCanSeeMissions && !string.IsNullOrEmpty(missionText))
        {
            name = $"{name}<br><size=12>{missionText}</size>";
        }
        __instance.Text.text = name;
        
    }

    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    static void Update_Postfix(SpectateNameUI __instance)
    {
        if (MissionUtilsConfigActivator.PencilConfig.DeadPlayersCanSeeMissions && MissionUI.instance?.Text != null)
        {
            // Update the mission text if it's not empty
            if (!string.IsNullOrEmpty(MissionUI.instance.Text.text))
            {
                __instance.Text.text = $"{playerName}<br><size=12>{MissionUI.instance.Text.text}</size>";
            }
        }
    }
}
