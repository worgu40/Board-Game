using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DebugController : MonoBehaviour
{
    private bool showConsole;
    private string input;
    public static DebugCommand KILL_ALL;
    public List<object> commandList;

    public void OnToggleDebug(InputValue value) {
        showConsole = !showConsole;
        Debug.Log("Toggling Console");
    }
    private void Awake()
    {
        KILL_ALL = new DebugCommand("kill_all", "Removes all characters from plane.", "kill_all", () => {
            // Add in code soon to kill all characters, referenced in character clones list. I'm going to bed gng lmao
        });
    }

    private void OnGUI()
    {
        if (!showConsole) {
            return;
        }
        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }
}
