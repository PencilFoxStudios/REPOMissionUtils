using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static MissionUtils.MissionUtils;
namespace MissionUtils;

public class MissionQueue
{
    private static Queue<MissionOptions> _queue = new Queue<MissionOptions>();

    public static void Enqueue(MissionOptions mission)
    {
        _queue.Enqueue(mission);
    }

    public static bool IsEmpty()
    {
        return _queue.Count == 0;
    }

    public static MissionOptions Dequeue()
    {
        return _queue.Dequeue();
    }
    public static void QueueListener(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                if (IsEmpty())
                {
                    // If the queue is empty, wait for a bit before checking again
                    Thread.Sleep(100);
                    continue;
                }
                else
                {
                    MissionUI missionUI = MissionUI.instance;
                    if (missionUI == null)
                    {
                        // If MissionUI is not available, wait for a bit before checking again
                        MissionUtils.Logger.LogWarning("MissionUI instance is null, waiting...");
                        Thread.Sleep(100);
                        return;
                    }
                    MissionOptions mission = Dequeue();
                    
                    if (mission != null)
                    {
                        // Call the original method with the mission options
                        missionUI.messageTimer = 0f;
                        MissionUIPatch.MissionText(missionUI, mission.msg, mission.color1, mission.color2, mission.time);
                    }

                }
                Thread.Sleep(MissionUtilsConfigActivator.PencilConfig.TimeBetweenMissionDisplays * 1000); // Wait for the specified time before processing the next mission
            }
        }
        catch (Exception ex)
        {
            MissionUtils.Logger.LogError($"Error in ListenLoop: {ex}");
        }
    }
}