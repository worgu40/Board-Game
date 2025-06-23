using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterManager : MonoBehaviour
{
    public GameObject characterPrefab;

    public Dictionary<string, Character> characterDict = new();
    public GameObject AVeryNormalMan;
    public GameObject AverageHero;

    public Dictionary<string, GameObject> modelDict = new();

    public Dictionary<string, GameObject> spawnedCharacters = new();

    public GameObject characterOptionsPanelPrefab; // Used for whole UI design when character clicked.
    private GameObject currentPanel;

    private bool awaitingPlacement = false;

    private GameObject currentSelectedCharacter;
    CharacterComponent currentSelectedCharacterComp;
    
    void Start()
    {
        Character avgHero = new AverageHero();
        Character vNormMan = new VeryNormalMan();
        Character zombie = new Zombie();
        characterDict.Add("avgHero", avgHero);
        characterDict.Add("vNormMan", vNormMan);
        characterDict.Add("zombie", zombie);

        modelDict.Add("avgHero", AverageHero);
        modelDict.Add("vNormMan", AVeryNormalMan);
    }

    void Update()
    {
        CastRayForGrid();
        ClickedCharacterOptions();
        if (awaitingPlacement)
        {
            PlaceCharacter();
        }
    }





    private void ShowUIPanelAtClick(Vector3 screenPosition)
    {
        // Destroy previous panel if it exists
        if (currentPanel != null) {
            Destroy(currentPanel);
        }
        
        {
            // Instantiate the panel as a child of the Canvas
            Canvas canvas = FindFirstObjectByType<Canvas>();
            currentPanel = Instantiate(characterOptionsPanelPrefab, canvas.transform);

            UnityEngine.UI.Button placeButton = currentPanel.GetComponentInChildren<UnityEngine.UI.Button>();
            if (placeButton != null)
            {
                placeButton.onClick.RemoveAllListeners();
                placeButton.onClick.AddListener(PlaceButtonClick);
            }

            // Convert screen position to Canvas (UI) position
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                screenPosition,
                canvas.worldCamera,
                out Vector2 localPoint
            );

            // Set the panel's anchored position
            RectTransform panelRect = currentPanel.GetComponent<RectTransform>();
            panelRect.anchoredPosition = localPoint;
        }
    }
    private void ClickedCharacterOptions()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject hitObject = CheckForHit("Character");
            if (hitObject == null)
            {
                return;
            }

            currentSelectedCharacter = hitObject;
            currentSelectedCharacterComp = hitObject.GetComponent<CharacterComponent>();
            Debug.Log(currentSelectedCharacterComp);
            Debug.Log(currentSelectedCharacterComp.placed);

            if (!currentSelectedCharacterComp.placed)
            {
                ShowUIPanelAtClick(Input.mousePosition);
            }
        }
    }
    public void PlaceButtonClick()
    {
        awaitingPlacement = true;
        Debug.Log("Waiting to be placed...");
    }
    private void PlaceCharacter()
    {
        Debug.Log("Placement method called");

        GameObject hitGrid = null;
        if (Input.GetMouseButtonDown(0))
        {
            hitGrid = CheckForHit("Grid");
        }
        
        if (hitGrid != null)
        {
            awaitingPlacement = false;
            currentSelectedCharacterComp.placed = true;
            currentSelectedCharacterComp = null;

            currentSelectedCharacter.transform.position = hitGrid.transform.position;
            Debug.Log("Placement would be placed at " + hitGrid.transform.position);
        }
    }
    private void MoveCharacter(Vector3 endPos)
    {
    }
    private void CastRayForGrid()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject hitObject = CheckForHit("Grid");
            if (hitObject == null)
            {
                return;
            }

            {
                Debug.Log(hitObject.name); // Log name of the clicked object, can use name to find position.
                Debug.Log(GetGridPosition(hitObject)); // Returns the Vector3 of the clicked object.
            }
        }
    }
    private GameObject CheckForHit(string comparedTag) { // Stored game object passes the specified game object to gather info about the HIT game object
        Ray hitRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (!Physics.Raycast(hitRay, out hitInfo)) // Checking if something is hit.
        {
            return null;
        }

        GameObject hitObject = hitInfo.collider.gameObject; // Object that was hit by the ray.
        if (hitObject.CompareTag(comparedTag))
        {
            return hitObject;
        }

        return null;
    }
    public void SpawnCharacter(string character)
    {
        Character spawnChar = characterDict[character]; // Character CLASS (attributes)
        if (spawnChar == null)
        {
            return;
        }
        else
        {
            Debug.Log(spawnChar);
            GameObject spawnedCharacter = Instantiate(characterPrefab);
            Debug.Log("this" + spawnedCharacter);
            spawnedCharacter.GetComponent<CharacterComponent>().AssignCharacter(character);

            GameObject playerModel = Instantiate(modelDict[character]);
            playerModel.AddComponent<CharacterComponent>();

            playerModel.transform.position = spawnedCharacter.transform.position;
            CharacterComponent playerModelComponent = playerModel.GetComponent<CharacterComponent>();
            CharacterComponent spawnedCharacterComponent = spawnedCharacter.GetComponent<CharacterComponent>();

            playerModelComponent.characterName = spawnedCharacterComponent.characterName;
            playerModelComponent.health = spawnedCharacterComponent.health;
            playerModelComponent.throwRange = spawnedCharacterComponent.throwRange;
            playerModelComponent.weight = spawnedCharacterComponent.weight;
            playerModelComponent.movementRange = spawnedCharacterComponent.movementRange;
            playerModelComponent.damage = spawnedCharacterComponent.damage;

            Vector3 modelPos = playerModel.transform.position;
            modelPos.y = spawnedCharacter.transform.position.y - 1f;
            playerModel.transform.position = modelPos;

            playerModel.tag = "Character";
            spawnedCharacters.Add(character, playerModel);
            Destroy(spawnedCharacter);
        }
    }
    public void KillCharacter(string character)
    {
        Destroy(spawnedCharacters[character]);
    }
    private Vector3 GetGridPosition(GameObject gridObject)
    {
        return gridObject.transform.position;
    }
}
