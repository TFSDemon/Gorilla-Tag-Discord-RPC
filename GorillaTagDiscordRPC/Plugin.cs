using BepInEx;
using UnityEngine;

namespace GorillaDiscordRPC
{
    [BepInPlugin("emily.tfsdemon.discordrpc", "Gorilla Discord RPC", "2.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        void Start()
        {
            gameObject.AddComponent<DiscordManager>();
            gameObject.AddComponent<GUIManager>();
        }
    }
}