using System.Collections;
using UnityEngine;

public class PlaneMouseHit : MonoBehaviour
{
    Vector3 clickPosition;
    public CharacterManager characterManager; // Reference to CharacterManager
    public float movementSpeed = 2f; // Speed of the player's movement
    public float waitTime = 1f; // Wait time between movements
    private bool isMoving = false; // Flag to track if the player is currently moving
    public int moveAmount;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    clickPosition = hit.point;
                    Debug.Log("Clicked on the plane at: " + clickPosition);
                    string boardPosition = GetBoardPosition(clickPosition);
                    Debug.Log("Board Position: " + boardPosition);
                    Vector3 boardCenter = GetBoardCenter(boardPosition);
                    Debug.Log("Center of Board Position: " + boardCenter);

                    // Start moving the active character
                    Vector3 currentBoardCenter = characterManager.GetActiveCharacterComponent().transform.position;
                    StartCoroutine(MovePlayer(currentBoardCenter, boardCenter));
                }
            }
        }
    }

    IEnumerator MovePlayer(Vector3 currentBoardCenter, Vector3 targetBoardCenter)
    {
        isMoving = true; // Set the moving flag to true
        GameObject activeCharacter = characterManager.GetActiveCharacterComponent().gameObject;

        // Normalize the coordinates to ensure consistent row and column calculations
        float normalizedCurrentX = currentBoardCenter.x + 6;
        float normalizedCurrentZ = -currentBoardCenter.z + 6;

        float normalizedTargetX = targetBoardCenter.x + 6;
        float normalizedTargetZ = -targetBoardCenter.z + 6;

        // Calculate the difference in rows and columns
        int currentRow = Mathf.FloorToInt(normalizedCurrentX / 2) + 1;
        int currentColumn = Mathf.FloorToInt(normalizedCurrentZ / 2) + 1;

        int targetRow = Mathf.FloorToInt(normalizedTargetX / 2) + 1;
        int targetColumn = Mathf.FloorToInt(normalizedTargetZ / 2) + 1;

        int rowDiff = Mathf.Abs(targetRow - currentRow);
        int columnDiff = Mathf.Abs(targetColumn - currentColumn);

        // Check if the move is valid
        if (rowDiff + columnDiff > moveAmount)
        {
            Debug.Log("Invalid move: Cannot move more than " + moveAmount + " spaces.");
            isMoving = false; // Reset the moving flag
            yield break;
        }

        // Move step by step
        Vector3 intermediateTarget = currentBoardCenter;

        // Handle row movement first
        while (currentRow != targetRow)
        {
            intermediateTarget.x += targetRow > currentRow ? 2 : -2;
            currentRow += targetRow > currentRow ? 1 : -1;
            yield return StartCoroutine(MoveToPosition(activeCharacter, intermediateTarget));
        }

        // Handle column movement next
        while (currentColumn != targetColumn)
        {
            intermediateTarget.z += targetColumn > currentColumn ? -2 : 2;
            currentColumn += targetColumn > currentColumn ? 1 : -1;
            yield return StartCoroutine(MoveToPosition(activeCharacter, intermediateTarget));
        }

        isMoving = false; // Reset the moving flag
    }

    IEnumerator MoveToPosition(GameObject character, Vector3 targetPosition)
    {
        while (Vector3.Distance(character.transform.position, targetPosition) > 0.1f)
        {
            character.transform.position = Vector3.MoveTowards(character.transform.position, targetPosition, Time.deltaTime * movementSpeed);
            yield return null;
        }

        character.transform.position = targetPosition;
        yield return new WaitForSeconds(waitTime);
    }

    string GetBoardPosition(Vector3 position)
    {
        int column = Mathf.Clamp(Mathf.FloorToInt((-position.z + 6) / 2) + 1, 1, 6);
        int row = Mathf.Clamp(Mathf.FloorToInt((position.x + 6) / 2) + 1, 1, 6); 
        return $"R{row}C{column}";
    }

    Vector3 GetBoardCenter(string boardPosition)
    {
        int row = int.Parse(boardPosition.Substring(1, 1)); // Extract row from "R{row}C{column}"
        int column = int.Parse(boardPosition.Substring(3, 1)); // Extract column from "R{row}C{column}"

        float centerX = -6 + (row - 1) * 2 + 1; // Calculate the center X coordinate
        float centerZ = 6 - (column - 1) * 2 - 1; // Calculate the center Z coordinate

        return new Vector3(centerX, 0, centerZ); // Return the center position
    }
}
