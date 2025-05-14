using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class DebugController : MonoBehaviour
{
    public static CharacterManager charManager;
    private bool showConsole;
    private string input;
    public static DebugCommand KILL_ALL;
    public static DebugCommand SPAWN_ALL;
    public List<object> commandList;

    public void OnToggleDebug(InputValue value) {
        showConsole = !showConsole;
        Debug.Log("Toggling Console");
    }

    public void OnReturn(InputValue value) {
        if(!showConsole) {
            return;
        }
        HandleInput();
        input = "";
    }
    
    private void Awake()
    {
        if (charManager == null)
        {
            charManager = FindFirstObjectByType<CharacterManager>();
            if (charManager == null)
            {
                Debug.Log("CharacterManager is not assigned and could not be found in the scene.");
            }
        }

        KILL_ALL = new DebugCommand("kill_all", "Removes all characters from plane.", "kill_all", () => {
            charManager.KillAllCharacters();
        });
        SPAWN_ALL = new DebugCommand("spawn_all", "Spawns all characters in predetermined debug list", "kill_all", () => {
            charManager.SpawnCharacters();
        });

        commandList = new List<object> {
            KILL_ALL,
            SPAWN_ALL,
        };
    }

    private void OnGUI()
    {
        if (!showConsole) {
            return;
        }
        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        GUI.SetNextControlName("console");
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
        GUI.FocusControl("console");
    }
    private void HandleInput() {
        for (int i = 0; i < commandList.Count; i++) {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            // EX: kill_all
            if (input.Contains(commandBase.commandId)) {
                if (commandList[i] as DebugCommand != null) {
                    (commandList[i] as DebugCommand).Invoke();
                }
            }
        }
    }
}
