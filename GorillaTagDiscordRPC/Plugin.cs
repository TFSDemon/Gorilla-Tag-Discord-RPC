using BepInEx;

namespace GorillaDiscordRPC
{
    [BepInPlugin("emily.tfsdemon.discordrpc", "Gorilla Discord RPC", "3.1.1")]
    public class Plugin : BaseUnityPlugin
    {
        void Start()
        {
            gameObject.AddComponent<RoomModeLookup>();
            gameObject.AddComponent<DiscordManager>();
            gameObject.AddComponent<GUIManager>();
        }
    }
}
