using UnityEngine;

public class InputHandler : MonoBehaviour
{
    internal static bool MouseLeftDown;
    internal static bool MouseLeft;
    internal static bool MouseLeftUp;
    internal static bool ResetKey;
    internal static bool NextLevelKey;
    internal static bool PauseKey;

    private void Awake()
    {
    }

    private void Update()
    {
        MouseLeftDown = Input.GetMouseButtonDown(0);
        MouseLeft = Input.GetMouseButton(0);
        MouseLeftUp = Input.GetMouseButtonUp(0);
        ResetKey = Input.GetKeyDown(KeyCode.R);
        NextLevelKey = Input.GetKeyDown(KeyCode.E);
        PauseKey = Input.GetKeyDown(KeyCode.Escape);
    }
}