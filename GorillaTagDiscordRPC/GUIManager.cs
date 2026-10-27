using UnityEngine;
using UnityEngine.InputSystem;

namespace GorillaDiscordRPC
{
    public class GUIManager : MonoBehaviour
    {
        bool show;
        Rect rect = new Rect(20, 20, 240, 100);

        void Update()
        {
            if (Keyboard.current != null && Keyboard.current.hKey.wasPressedThisFrame)
                show = !show;
        }

        void OnGUI()
        {
            if (!show) return;
            rect = GUI.Window(1, rect, _ =>
            {
                Settings.HideRoomCode = GUILayout.Toggle(Settings.HideRoomCode, "Hide room code");
                Settings.QuestMode = GUILayout.Toggle(Settings.QuestMode, "Quest mode");
                GUI.DragWindow();
            }, "RPC");
        }
    }
}
