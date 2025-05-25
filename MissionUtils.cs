using System.Collections;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace MissionUtils;

[BepInPlugin("PencilFoxStudios.MissionUtils", "MissionUtils", "1.0.0")]
public class MissionUtils : BaseUnityPlugin
{
    internal static MissionUtils Instance { get; private set; } = null!;
    public new static ManualLogSource Logger => Instance._logger;
    private ManualLogSource _logger => base.Logger;
    internal Harmony? Harmony { get; set; }
    private CancellationTokenSource cts = new CancellationTokenSource();
    private void Awake()
    {
        Instance = this;
        MissionUtilsConfigActivator.Initialize(new MissionUtilsConfigActivator(Config));
        Logger.LogInfo($"Loading {Info.Metadata.GUID} v{Info.Metadata.Version}...");
        Logger.LogInfo($"Config file path: {Config.ConfigFilePath}");
        this.gameObject.transform.parent = null;
        this.gameObject.hideFlags = HideFlags.HideAndDontSave;

        Patch();
        Logger.LogInfo($"Patching {Info.Metadata.GUID} v{Info.Metadata.Version}...");

        Logger.LogInfo($"{Info.Metadata.GUID} v{Info.Metadata.Version} has loaded!");

        // Start the queue listener
        Task.Run(() => MissionQueue.QueueListener(cts.Token));

    }

    private void Start()
    {
        // Initialize networking
        PencilNetwork.InitNetworking();
    }

    internal void Patch()
    {
        Harmony ??= new Harmony(Info.Metadata.GUID);
        Harmony.PatchAll();
    }

    internal void Unpatch()
    {
        Harmony?.UnpatchSelf();
    }

    private void Update()
    {
        // Code that runs every frame goes here
    }
    public class MissionOptions
    {
        public Color color1 { get; set; }
        public Color color2 { get; set; }
        public string msg { get; set; }
        public float time { get; set; }

        public static byte[] Serialize(object customObject)
        {
            MissionOptions options = (MissionOptions)customObject;
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(options.msg);
                    writer.Write(options.color1.r);
                    writer.Write(options.color1.g);
                    writer.Write(options.color1.b);
                    writer.Write(options.color2.r);
                    writer.Write(options.color2.g);
                    writer.Write(options.color2.b);
                    writer.Write(options.time);
                }
                return stream.ToArray();
            }
        }
        public static object Deserialize(byte[] serializedCustomObject)
        {
            using (MemoryStream stream = new MemoryStream(serializedCustomObject))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                MissionOptions options = new MissionOptions();
                options.msg = reader.ReadString();
                options.color1 = new Color(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                options.color2 = new Color(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                options.time = reader.ReadSingle();
                return options;
            }
        }
    

        public static MissionOptions Create(string message, Color color1, Color color2, float time)
        {
            return new MissionOptions
            {
                msg = message,
                color1 = color1,
                color2 = color2,
                time = time
            };
        }

    }
}