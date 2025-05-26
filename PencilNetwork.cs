using ExitGames.Client.Photon;
using REPOLib.Modules;
using MissionUtils;
using Photon.Realtime;
using static MissionUtils.MissionUtils;
namespace MissionUtils;

class PencilNetwork
{
    public static NetworkedEvent? NewBroadcastedMissionEvent;

    public static void InitNetworking()
    {
        NewBroadcastedMissionEvent = new NetworkedEvent("NewBroadcastedMissionEvent", HandleBroadcastedMissionEvent);
        // Register the event
        PhotonPeer.RegisterType(typeof(MissionOptions), 100, MissionOptions.Serialize, MissionOptions.Deserialize);
    }
    private static void HandleBroadcastedMissionEvent(EventData eventData)
    {
        MissionOptions options = (MissionOptions)eventData.CustomData;
        MissionQueue.Enqueue(options);
        MissionUtils.Logger.LogInfo($"Received mission: {options.msg}");
    }

    public static void SendMission(MissionOptions options)
    {
        if (SemiFunc.IsMultiplayer() && SemiFunc.IsMasterClient())
        {
            NewBroadcastedMissionEvent.RaiseEvent(options, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable);
            MissionUtils.Logger.LogInfo($"Sent mission: {options.msg}");
        }
        else
        {
            MissionUtils.Logger.LogError("Cannot send mission, not in multiplayer or not master client.");
        }
    }
}