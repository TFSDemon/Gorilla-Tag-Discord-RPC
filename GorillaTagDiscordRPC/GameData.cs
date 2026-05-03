using Photon.Pun;
using GorillaNetworking;
using UnityEngine;

namespace GorillaDiscordRPC
{
    public class GameData
    {
        public string Map;
        public string Mode;
        public string Room;
        public int Players;
        public string TagState;
        public string RoomCode;

        public static GameData Get()
        {
            GameData d = new GameData();

            d.Map = PhotonNetworkController.Instance?.currentJoinTrigger?.networkZone ?? "Unknown";
            d.Mode = GorillaComputer.instance?.currentGameMode?.Value ?? "Loading";

            d.Room = "Not in Room";
            d.RoomCode = "";
            d.Players = 0;

            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom != null)
            {
                var room = PhotonNetwork.CurrentRoom;

                d.Players = room.PlayerCount;

                d.Room = PhotonNetworkController.Instance != null && PhotonNetworkController.Instance.isPrivate
                    ? "Private"
                    : "Public";

                string roomName = room.Name;

                if (!string.IsNullOrEmpty(roomName) &&
                    roomName != "Public" &&
                    roomName != "Private")
                {
                    d.RoomCode = roomName;
                }
            }

            var rig = GorillaTagger.Instance?.offlineVRRig;

            if (rig != null)
            {
                Color bodyColor = rig.mainSkin.material.color;

                d.TagState =
                    bodyColor.r > 0.8f && bodyColor.g < 0.4f
                        ? "Tagged"
                        : "Untagged";
            }
            else
            {
                d.TagState = "Unknown";
            }

            return d;
        }
    }
}