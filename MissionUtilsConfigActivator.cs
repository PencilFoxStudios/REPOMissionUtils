using System.Collections.Generic;
using BepInEx.Configuration;
using Unity.VisualScripting.FullSerializer;

namespace MissionUtils;

class MissionUtilsConfigActivator
{
    public static MissionUtilsConfigActivator PencilConfig { get; private set; } = null!;
    readonly ConfigEntry<int> timeBetweenMissionDisplays;
    public int TimeBetweenMissionDisplays => timeBetweenMissionDisplays.Value;

    public MissionUtilsConfigActivator(ConfigFile cfg)
    {
        timeBetweenMissionDisplays = cfg.Bind<int>(
            "MissionUtils",
            "Time Between Mission Displays",
            2,
            "How long to wait between displaying missions in the queue?\nThe default is 2 seconds.\n"
        );

    }
    public static void Initialize(MissionUtilsConfigActivator config)
    {
        PencilConfig = config;
    }
}