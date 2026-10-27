using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace GorillaDiscordRPC
{
    public class RoomModeLookup : MonoBehaviour, ILobbyCallbacks
    {
        static readonly Dictionary<string, string> Cache = new Dictionary<string, string>();
        float nextNudge;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            PhotonNetwork.AddCallbackTarget(this);
        }

        void OnDestroy()
        {
            try { PhotonNetwork.RemoveCallbackTarget(this); } catch { }
        }

        void Update()
        {
            if (!Settings.QuestMode || Time.unscaledTime < nextNudge) return;
            nextNudge = Time.unscaledTime + 8f;
            try
            {
                if (PhotonNetwork.IsConnected && !PhotonNetwork.InRoom && !PhotonNetwork.InLobby)
                    PhotonNetwork.JoinLobby();
            }
            catch { }
        }

        public static string GetMode(string room)
        {
            if (string.IsNullOrEmpty(room)) return null;
            if (Cache.TryGetValue(room, out var mode)) return mode;
            foreach (var kv in Cache)
                if (string.Equals(kv.Key, room, System.StringComparison.OrdinalIgnoreCase))
                    return kv.Value;
            return null;
        }

        public void OnRoomListUpdate(List<RoomInfo> list)
        {
            if (list == null) return;
            foreach (var r in list)
            {
                if (r == null || string.IsNullOrEmpty(r.Name)) continue;
                if (r.RemovedFromList) { Cache.Remove(r.Name); continue; }
                if (r.CustomProperties != null
                    && r.CustomProperties.TryGetValue("gameMode", out object v)
                    && v != null)
                    Cache[r.Name] = v.ToString();
            }
        }

        public void OnJoinedLobby() { }
        public void OnLeftLobby() { }
        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics) { }
    }
}
