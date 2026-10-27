using System;
using Discord;
using UnityEngine;

namespace GorillaDiscordRPC
{
    public class DiscordManager : MonoBehaviour
    {
        const long ClientId = 1500593925121900564;

        Discord.Discord discord;
        ActivityManager activities;
        long start;
        string fingerprint = "";
        bool lastHide, lastQuest;

        void Awake() => DontDestroyOnLoad(gameObject);

        void Start()
        {
            try
            {
                discord = new Discord.Discord(ClientId, (ulong)CreateFlags.Default);
                activities = discord.GetActivityManager();
                start = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                lastHide = Settings.HideRoomCode;
                lastQuest = Settings.QuestMode;
                Push(true);
            }
            catch (Exception e)
            {
                Debug.LogError("[RPC] " + e.Message);
            }
        }

        void Update()
        {
            if (discord == null) return;
            discord.RunCallbacks();

            if (Settings.HideRoomCode != lastHide || Settings.QuestMode != lastQuest)
            {
                lastHide = Settings.HideRoomCode;
                lastQuest = Settings.QuestMode;
                Push(true);
            }

            if (Time.frameCount % 150 == 0)
                Push();
        }

        void Push(bool force = false)
        {
            if (activities == null) return;

            var data = GameData.Capture();
            string fp = data.Room + data.RoomCode + data.Mode + data.Map + data.Players
                + data.Status + Settings.HideRoomCode + Settings.QuestMode;
            if (!force && fp == fingerprint) return;
            fingerprint = fp;

            activities.UpdateActivity(PresenceBuilder.Build(data, start), _ => { });
        }

        void OnDestroy() => discord?.Dispose();
    }
}
