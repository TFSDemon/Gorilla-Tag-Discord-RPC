using UnityEngine;

namespace GorillaDiscordRPC
{
    public class GUIManager : MonoBehaviour
    {
        private bool show = true;
        private Rect rect = new Rect(20, 20, 260, 160);

        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.H))
            {
                show = !show;
                Debug.Log("[GUI] Show = " + show);
            }
        }

        void OnGUI()
        {
            if (!show) return;

            rect = GUI.Window(1, rect, Draw, "RPC Settings");
        }

        void Draw(int id)
        {
            Settings.HideRoomCode = GUILayout.Toggle(Settings.HideRoomCode, "Hide Room Code");
            GUI.DragWindow();
        }
    }
}