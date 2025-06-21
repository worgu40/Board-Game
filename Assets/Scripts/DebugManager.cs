using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public CharacterManager characterManager;
    private bool showConsole; // Hold state for if console is shown.
    private bool showHelp;

    private string input; // Typed out message.

    public static DebugCommand KILL_ALL;
    public static DebugCommand<string> SPAWN;
    public static DebugCommand HELP;
    public static DebugCommand<string> KILL;

    public List<object> commandList;

    void Awake()
    {
        HELP = new DebugCommand("help", "Displays help menu", "help", () => {
            showHelp = true;
        });
        KILL_ALL = new DebugCommand("kill_all", "Kills all characters.", "kill_all", () =>
        {
            // Run method to kill all later.
        });
        SPAWN = new DebugCommand<string>("spawn", "Spawns given character", "spawn (args)", (x) =>
        {
            Debug.Log("Called with arguments " + x);
            characterManager.SpawnCharacter(x);
        });
        KILL = new DebugCommand<string>("kill", "Kills given character", "kill (args)", (x) =>
        {
            characterManager.KillCharacter(x);
        });

        commandList = new List<object>
        {
            HELP,
            KILL_ALL,
            SPAWN,
            KILL,
        };
    }
    public void OnToggleDebug(InputValue value) // Check Unity Input Manager.
    {
        showConsole = !showConsole;
        Debug.Log("Toggled Console");
    }
    public void OnReturn(InputValue value) // Check Unity Input Manager.
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }
    }
    Vector2 scroll;
    private void OnGUI() // Unity Method called for rendering and handling GUI events.
    {
        if (!showConsole)
        {
            return;
        }
        ShowHelp();
        CreateGUI(0);

    }

    



    
    private void CreateGUI(float y)
    {
        GUI.Box(new Rect(0, y, Screen.width, 30), ""); // Creates a box with specified size with an empty string.
        GUI.backgroundColor = new Color(0, 0, 0, 0); // Sets the color for the background.

        GUI.SetNextControlName("console"); // Set the text field name to console
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input); // Creates text field at top of screen
        GUI.FocusControl("console"); // focuses on console, see 2 lines above.
    }

    private void ShowHelp()
    {
        float y = 0f;
        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;

                string label = $"{command.commandFormat} - {command.commandDescription}"; // EX: spawn (arg) - Spawns selected character.

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20); // Drops the y level of the label for every other command.

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();
            y += 100f;
        }
    }

    private void HandleInput()
    {
        string[] properties = input.Split(' ');
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandId))
            {
                // DEFAULT COMMANDS.
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                // HANDLER FOR STRINGS.
                else if (commandList[i] as DebugCommand<string> != null)
                {
                    (commandList[i] as DebugCommand<string>).Invoke(properties[1]);
                }
            }
        }
    }
}
