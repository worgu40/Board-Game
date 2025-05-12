using UnityEngine;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;

public class CharacterManager : MonoBehaviour
{
    public GameObject character;
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public PlaneMouseHit planeMouseHit; // Reference to PlaneMouseHit
    public List<GameObject> characterClones = new();
    private int activeCharacterIndex = 0;
    private bool characterSpawned;

    void Start()
    {
    }

    void Update()
    {
        if (!characterSpawned) {
            SpawnCharacters();
        }
        
        // Cycle through characters with the '.' key
        if (Input.GetKeyDown(KeyCode.Period))
        {
            activeCharacterIndex = (activeCharacterIndex + 1) % characterClones.Count;
            Debug.Log($"Active character: {GetActiveCharacterName()}");
        }

        // Decrease health of the active character with '-' key
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            CharacterComponent activeCharacter = GetActiveCharacterComponent();
            activeCharacter.ModifyHealth(-1); // Decrease health by 1
            Debug.Log($"{activeCharacter.CharacterName}'s health decreased to {activeCharacter.Health}");
        }

        // Increase health of the active character with '+' key
        if (Input.GetKeyDown(KeyCode.Equals)) // KeyCode.Equals corresponds to the '+' key
        {
            CharacterComponent activeCharacter = GetActiveCharacterComponent();
            activeCharacter.ModifyHealth(1); // Increase health by 1
            Debug.Log($"{activeCharacter.CharacterName}'s health increased to {activeCharacter.Health}");
        }

        // Forward weapon assignment and cycling to the active character
        for (int i = 0; i < characterClones.Count; i++)
        {
            if (characterClones[i] == null) {
                return;
            }
            CharacterComponent characterComponent = characterClones[i].GetComponent<CharacterComponent>();
            if (characterComponent != null)
            {
                characterComponent.UpdateWeapon(i == activeCharacterIndex); // Pass true only for the active character
            }
        }
    }

    public CharacterComponent GetActiveCharacterComponent()
    {
        if (characterClones.Count == 0)
        {
        }
        CharacterComponent component = characterClones[activeCharacterIndex].GetComponent<CharacterComponent>();
        if (component == null)
        {
        }
        return component;
    }

    private string GetActiveCharacterName()
    {
        return GetActiveCharacterComponent().CharacterName;
    }

    private void SpawnCharacters()
    {
        Character avgHero = new AverageHero();
        Character normalMan = new VeryNormalMan();
        Character Zombie = new Zombie();

        // Clone and position the characters
        CloneCharacter(character, avgHero, new Vector3(5, 0, 5), false);
        CloneCharacter(character, normalMan, new Vector3(-5, 0, -5), false);
        CloneCharacter(enemyPrefab, Zombie, new Vector3(5, 0, -5), true);
        characterSpawned = true;
    }
    private void CloneCharacter(GameObject characterObject, Character characterComponent, Vector3 charPosition, bool isEnemy) {
        GameObject clonedCharacter = Instantiate(characterObject);
        clonedCharacter.transform.position = charPosition;
        clonedCharacter.GetComponent<CharacterComponent>().AssignCharacter(characterComponent);
        characterClones.Add(clonedCharacter);
        if (!isEnemy) {
            return;
        }
        clonedCharacter.tag = "Enemy";

    }
    public void KillAllCharacters() {
        foreach (GameObject character in characterClones) {
            if (character.CompareTag("Enemy")) {
                return;
            }
            Debug.Log("Killed: " + character);
            Destroy(character);
        }
    }
}
