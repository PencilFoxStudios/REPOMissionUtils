using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace MissionUtils;

[HarmonyPatch(typeof(SemiFunc))]
public class SemiFuncPatch
{
    [HarmonyPatch(typeof(SemiFunc))]
    [HarmonyPatch("HUDSpectateSetName")]
    [HarmonyPrefix]
    static void HUDSpectateSetName_Prefix(ref string name)
    {

    }
}
