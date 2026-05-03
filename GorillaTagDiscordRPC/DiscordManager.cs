using UnityEngine;
using Discord;
using System;

namespace GorillaDiscordRPC
{
    public class DiscordManager : MonoBehaviour
    {
        private Discord.Discord discord;
        private ActivityManager activityManager;
        private long startTime;
        private float timer;

        private const long ClientId = 1500593925121900564;

        private bool lastHideRoomState;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            try
            {
                Debug.Log("[RPC] Init starting...");

                discord = new Discord.Discord(ClientId, (ulong)CreateFlags.Default);
                activityManager = discord.GetActivityManager();

                startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                Debug.Log("[RPC] Initialized SUCCESS");

                SetPresence();
                lastHideRoomState = Settings.HideRoomCode;
            }
            catch (Exception e)
            {
                Debug.LogError("[RPC] INIT FAILED: " + e);
            }
        }

        void Update()
        {
            if (discord == null) return;

            discord.RunCallbacks();

            // instant refresh when toggle changes
            if (Settings.HideRoomCode != lastHideRoomState)
            {
                lastHideRoomState = Settings.HideRoomCode;
                SetPresence();
            }

            // safety refresh
            if (Time.frameCount % 300 == 0)
            {
                SetPresence();
            }
        }

        void SetPresence()
        {
            var data = GameData.Get();

            // ROOM DISPLAY
            string roomDisplay = data.Room;

            if (!Settings.HideRoomCode && !string.IsNullOrEmpty(data.RoomCode))
            {
                roomDisplay = $"{data.Room} [{data.RoomCode}]";
            }

            if (string.IsNullOrEmpty(roomDisplay))
                roomDisplay = "Unknown";

            // PLAYER COUNT
            string playerDisplay =
                $"{data.Players} Player{(data.Players == 1 ? "" : "s")}";

            // FINAL DETAILS LINE
            string details = $"{roomDisplay} • {playerDisplay}";

            Activity activity = new Activity
            {
                Type = ActivityType.Playing,
                State = data.Mode,
                Details = details,

                Instance = true,

                Timestamps =
                {
                    Start = startTime
                },

                Assets =
                {
                    LargeImage = "icon",
                    LargeText = "Gorilla Tag"
                }
            };

           

            activityManager.UpdateActivity(activity, result =>
            {
                if (result != Result.Ok)
                {
                    Debug.LogWarning("[RPC] Update failed: " + result);
                }
                else
                {
                    Debug.Log("[RPC] Updated successfully");
                }
            });
        }

        void OnDestroy()
        {
            discord?.Dispose();
        }
    }
}