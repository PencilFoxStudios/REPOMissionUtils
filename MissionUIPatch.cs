using System;
using HarmonyLib;
using TMPro;
using UnityEngine;
using static MissionUtils.MissionUtils;

namespace MissionUtils;

[HarmonyPatch(typeof(MissionUI))]
public class MissionUIPatch
{
    public static string _previousMissionText = "";
    public static bool isCurrentlyDisplaying = false;
    // First we have to grab the original method before we overwrite it
    // with our own version. This is done by using the HarmonyReversePatch

    [HarmonyReversePatch]
    [HarmonyPatch(typeof(MissionUI), "MissionText")]
    public static void MissionText(MissionUI __instance, string message, Color colorMain, Color colorFlash, float desiredTime = 3f)
    {
        // its a stub so it has no initial content
        throw new NotImplementedException("It's a stub");
    }


    // Now we can create our own version of the method
    // that will be called instead of the original one.
    [HarmonyPatch(typeof(MissionUI))]
    [HarmonyPatch("MissionText")]
    [HarmonyPrefix]
    private static void MissionText_Prefix(MissionUI __instance, string message, Color colorMain, Color colorFlash, float time = 3f)
    {
        if (_previousMissionText == message)
        {
            // If the message is the same as the previous one, we don't need to display it again.
            return;
        }
        // If it's currently displaying a mission, we force it to display the new one.
        // This is preventable by setting Config.TimeBetweenMissionDisplays to a higher value.
        _previousMissionText = message;
        
        MissionOptions mission = MissionOptions.Create(message, colorMain, colorFlash, time);
        bool broadcast = mission.msg.StartsWith("%broadcast%");
        // If broadcast is true, send the mission to all players INCLUDING the master client
        MissionUtils.Logger.LogInfo($"Broadcast: {mission.msg} ({broadcast})");
        if (broadcast)
        {
            // Remove the %broadcast% prefix from the message
            mission.msg = message.Replace("%broadcast%", "");

            MissionUtils.Logger.LogInfo($"Broadcasting mission: {mission.msg}");
            PencilNetwork.SendMission(mission);
        }
        else
        {
            MissionUtils.Logger.LogInfo($"Displaying Mission: {mission.msg}");
            // If broadcast is false, just add it to the queue
            MissionQueue.Enqueue(mission);
        }
        
        return;
    }
}