using Discord;

namespace GorillaDiscordRPC
{
    public static class PresenceBuilder
    {
        public static Activity Build(GameData d, long startTime)
        {
            string roomDisplay = d.Room;

            if (!Settings.HideRoomCode && !string.IsNullOrEmpty(d.RoomCode))
                roomDisplay += $" [{d.RoomCode}]";

            var activity = new Activity
            {
                
                State = $"{roomDisplay} ({d.Players} players)",
                Timestamps = new ActivityTimestamps
                {
                    Start = startTime
                }
            };

           
            return activity;
        }
    }
}
