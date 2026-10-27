using System;
using System.Reflection;
using GorillaGameModes;
using GorillaNetworking;
using Photon.Pun;
using UnityEngine;

namespace GorillaDiscordRPC
{
    public sealed class GameData
    {
        const string NA = "Unavailable When On Quest";

        public string Mode = "Loading";
        public string Queue = "Default";
        public string Map = "Unknown";
        public string Room = "Not in Room";
        public string RoomCode = "";
        public string Status = "";
        public int Players;
        public bool Ranked;
        public bool InRoom;

        public static GameData Capture() => Settings.QuestMode ? Quest() : Pc();

        static GameData Pc()
        {
            var d = new GameData { Queue = ReadQueue() };
            if (!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom == null)
                return d;

            var room = PhotonNetwork.CurrentRoom;
            d.InRoom = true;
            d.Players = room.PlayerCount;
            d.Room = PhotonNetworkController.Instance != null && PhotonNetworkController.Instance.isPrivate
                ? "Private" : "Public";
            if (!string.IsNullOrEmpty(room.Name) && room.Name != "Public" && room.Name != "Private")
                d.RoomCode = room.Name;

            ApplyGameMode(d, ReadRoomGameMode(room));
            if (d.Map == "Unknown") d.Map = ReadZone();
            d.Ranked = d.Ranked || Has(d.Map, "RANKED") || Has(d.Mode, "RANKED");
            d.Status = ReadStatus();
            return d;
        }

        static GameData Quest()
        {
            var d = new GameData { Mode = NA, Queue = NA, Map = NA, Status = NA };

            try
            {
                var list = FriendBackendController.Instance?.FriendsList;
                FriendBackendController.FriendPresence p = null;
                if (list != null)
                {
                    foreach (var f in list)
                    {
                        if (f?.Presence == null || string.IsNullOrEmpty(f.Presence.RoomId)) continue;
                        p = f.Presence;
                        break;
                    }
                    if (p == null && list.Count == 1)
                        p = list[0]?.Presence;
                }

                if (p == null || string.IsNullOrEmpty(p.RoomId))
                    return d;

                d.InRoom = true;
                d.RoomCode = p.RoomId;
                d.Room = p.IsPublic == true ? "Public" : p.IsPublic == false ? "Private" : "Friend Room";
                d.Players = 1;
                if (!string.IsNullOrEmpty(p.Zone)) d.Map = ZoneLabel(p.Zone);
                if (!string.IsNullOrEmpty(p.UserName)) d.Status = p.UserName;

                string lobbyMode = RoomModeLookup.GetMode(p.RoomId);
                if (!string.IsNullOrEmpty(lobbyMode))
                    ApplyGameMode(d, lobbyMode);

                if (d.Map == "Unknown" || d.Map == NA)
                    d.Map = string.IsNullOrEmpty(p.Zone) ? NA : ZoneLabel(p.Zone);
                if (string.IsNullOrEmpty(d.Mode) || d.Mode == "Loading") d.Mode = NA;
                if (string.IsNullOrEmpty(d.Queue) || d.Queue == "Default") d.Queue = NA;
                if (string.IsNullOrEmpty(d.Status)) d.Status = NA;
            }
            catch { }

            return d;
        }

        static string ReadRoomGameMode(Photon.Realtime.Room room)
        {
            try
            {
                string ns = NetworkSystem.Instance?.GameModeString;
                if (!string.IsNullOrEmpty(ns)) return ns;

                var props = room.CustomProperties;
                if (props == null) return null;
                if (props.TryGetValue(RoomConfig.Room_GameModePropKey, out object v) && v != null)
                    return v.ToString();
                if (props.TryGetValue("gameMode", out v) && v != null)
                    return v.ToString();
            }
            catch { }
            return null;
        }

        static void ApplyGameMode(GameData d, string raw)
        {
            if (string.IsNullOrEmpty(raw))
            {
                try
                {
                    string name = GameMode.ActiveGameMode?.GameModeName();
                    if (!string.IsNullOrEmpty(name)) { d.Mode = Title(name); return; }
                }
                catch { }
                d.Mode = Title(GorillaComputer.instance?.currentGameMode?.Value ?? "Unknown");
                return;
            }

            d.Ranked = Has(raw, "RANKED");
            try
            {
                var gms = GameModeString.FromString(raw);
                if (gms != null)
                {
                    if (!string.IsNullOrEmpty(gms.gameType)) d.Mode = Title(gms.gameType);
                    if (!string.IsNullOrEmpty(gms.zone)) d.Map = ZoneLabel(gms.zone);
                    if (!string.IsNullOrEmpty(gms.queue)) d.Queue = Title(gms.queue);
                    return;
                }
            }
            catch { }

            d.Mode = Title(raw);
            string z = MatchZone(raw);
            if (z != null) d.Map = z;
        }

        static string ReadStatus()
        {
            try
            {
                var tagger = GorillaTagger.Instance;
                if (tagger != null && tagger.currentStatus != GorillaTagger.StatusEffect.None)
                    return Title(tagger.currentStatus.ToString());

                var local = NetworkSystem.Instance?.LocalPlayer;
                if (local == null) return "";

                var active = GameMode.ActiveGameMode;
                if (active == null) return "";

                var paint = active as GorillaPaintbrawlManager;
                if (paint?.playerStatusDict != null
                    && paint.playerStatusDict.TryGetValue(local.ActorNumber, out var st))
                {
                    string s = st.ToString();
                    if (!string.IsNullOrEmpty(s)
                        && !s.Equals("None", StringComparison.OrdinalIgnoreCase)
                        && !s.Equals("Normal", StringComparison.OrdinalIgnoreCase))
                        return Title(s);
                }

                bool tagged = false;
                try { tagged = GameMode.LocalIsTagged(local); } catch { }
                if (!tagged) try { tagged = active.LocalIsTagged(local); } catch { }
                if (tagged) return "Tagged";
            }
            catch { }
            return "";
        }

        static string ReadQueue()
        {
            try
            {
                var c = GorillaComputer.instance;
                if (c == null) return "Default";
                var f = c.GetType().GetField("currentQueue",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                object v = f?.GetValue(c);
                return v != null ? Title(v.ToString()) : "Default";
            }
            catch { return "Default"; }
        }

        static string ReadZone()
        {
            try
            {
                var zm = ZoneManagement.instance;
                var zones = zm?.activeZones;
                if (zones == null) return "Unknown";
                foreach (var z in zones)
                {
                    if (z == GTZone.none) continue;
                    try
                    {
                        string scene = zm.GetSceneNameForZone(z);
                        if (!string.IsNullOrEmpty(scene)) return Title(scene);
                    }
                    catch { }
                    return Title(z.ToString());
                }
            }
            catch { }
            return "Unknown";
        }

        static string ZoneLabel(string token)
        {
            if (string.IsNullOrEmpty(token)) return "Unknown";
            if (Enum.TryParse(token, true, out GTZone z) && z != GTZone.none)
                return Title(z.ToString());
            return MatchZone(token) ?? Title(token);
        }

        static string MatchZone(string raw)
        {
            if (string.IsNullOrEmpty(raw)) return null;
            GTZone best = GTZone.none;
            int bestLen = 0;
            foreach (GTZone z in Enum.GetValues(typeof(GTZone)))
            {
                if (z == GTZone.none) continue;
                string name = z.ToString();
                if (raw.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0 && name.Length > bestLen)
                {
                    best = z;
                    bestLen = name.Length;
                }
            }
            return best != GTZone.none ? Title(best.ToString()) : null;
        }

        static string Title(string s)
        {
            if (string.IsNullOrEmpty(s)) return "Unknown";
            s = s.Replace('_', ' ').Trim();
            return s.Length == 0 ? "Unknown" : char.ToUpperInvariant(s[0]) + (s.Length > 1 ? s.Substring(1) : "");
        }

        static bool Has(string s, string token) =>
            !string.IsNullOrEmpty(s) && s.IndexOf(token, StringComparison.OrdinalIgnoreCase) >= 0;
    }
}
