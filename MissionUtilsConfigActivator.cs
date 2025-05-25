using System.Collections.Generic;
using BepInEx.Configuration;
using Unity.VisualScripting.FullSerializer;

namespace MissionUtils;

class MissionUtilsConfigActivator
{
    public static MissionUtilsConfigActivator PencilConfig { get; private set; } = null!;
    readonly ConfigEntry<int> timeBetweenMissionDisplays;
    public int TimeBetweenMissionDisplays => timeBetweenMissionDisplays.Value;

    readonly ConfigEntry<bool> deadPlayersCanSeeMissions;
    public bool DeadPlayersCanSeeMissions => deadPlayersCanSeeMissions.Value;

    public MissionUtilsConfigActivator(ConfigFile cfg)
    {
        timeBetweenMissionDisplays = cfg.Bind<int>(
            "MissionUtils",
            "Time Between Mission Displays",
            2,
            "How long to wait between displaying missions in the queue?\nThe default is 2 seconds.\n"
        );
        deadPlayersCanSeeMissions = cfg.Bind<bool>(
            "MissionUtils",
            "Dead Players Can See Missions",
            false,
            "Should dead players be able to see missions?\nThe default is false.\n"
        );

    }
    public static void Initialize(MissionUtilsConfigActivator config)
    {
        PencilConfig = config;
    }
}