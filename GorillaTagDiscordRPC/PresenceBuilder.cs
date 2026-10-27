using System.Collections.Generic;
using Discord;

namespace GorillaDiscordRPC
{
    public static class PresenceBuilder
    {
        public static Activity Build(GameData d, long start)
        {
            string room = d.Room;
            if (!Settings.HideRoomCode && !string.IsNullOrEmpty(d.RoomCode))
                room += " [" + d.RoomCode + "]";

            string details = d.InRoom
                ? room + " • " + d.Players + (d.Players == 1 ? " player" : " players")
                : "Not in a room";

            var parts = new List<string>();
            if (!d.InRoom)
            {
                parts.Add("Queue: " + d.Queue);
            }
            else
            {
                if (!string.IsNullOrEmpty(d.Status)) parts.Add(d.Status);
                if (d.Ranked)
                    parts.Add(d.Mode != "Ranked" && !string.IsNullOrEmpty(d.Mode) ? "Ranked " + d.Mode : "Ranked");
                else if (!string.IsNullOrEmpty(d.Mode) && d.Mode != "Unknown")
                    parts.Add(d.Mode);
                if (!string.IsNullOrEmpty(d.Map) && d.Map != "Unknown")
                    parts.Add(d.Map);
                if (!string.IsNullOrEmpty(d.Queue) && d.Queue != "Default")
                    parts.Add(d.Queue);
            }

            return new Activity
            {
                Type = ActivityType.Playing,
                Details = details,
                State = parts.Count > 0 ? string.Join(" • ", parts) : "Gorilla Tag",
                Instance = true,
                Timestamps = { Start = start },
                Assets = { LargeImage = "icon", LargeText = string.IsNullOrEmpty(d.Status) ? "Gorilla Tag" : d.Status }
            };
        }
    }
}
