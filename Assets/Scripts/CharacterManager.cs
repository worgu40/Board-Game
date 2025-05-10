using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    public GameObject character;
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public PlaneMouseHit planeMouseHit; // Reference to PlaneMouseHit
    private List<GameObject> characterClones = new();
    private int activeCharacterIndex = 0;
    private bool charactersSpawned = false; // Flag to track if characters are already spawned

    void Start()
    {
        SpawnCharacters();

        Debug.Log($"Active character: {GetActiveCharacterName()}");
    }

    void Update()
    {
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
            Debug.LogError("No characters in characterClones list.");
            return null;
        }

        CharacterComponent component = characterClones[activeCharacterIndex].GetComponent<CharacterComponent>();
        if (component == null)
        {
            Debug.LogError($"Character at index {activeCharacterIndex} is missing a CharacterComponent.");
        }
        return component;
    }

    private string GetActiveCharacterName()
    {
        return GetActiveCharacterComponent().CharacterName;
    }

    private void SpawnCharacters()
    {
        if (charactersSpawned) return; // Stop if characters are already spawned

        charactersSpawned = true; // Set the flag to true after spawning characters

        Character avgHero = new AverageHero();
        Character normalMan = new VeryNormalMan();
        Character Zombie = new Zombie();

        // Clone and position the characters
        GameObject avgHeroClone = Instantiate(character);
        avgHeroClone.transform.position = new Vector3(5, 0, 5);
        avgHeroClone.GetComponent<CharacterComponent>().AssignCharacter(avgHero);
        characterClones.Add(avgHeroClone);

        GameObject normalManClone = Instantiate(character);
        normalManClone.transform.position = new Vector3(-5, 0, -5);
        normalManClone.GetComponent<CharacterComponent>().AssignCharacter(normalMan);
        characterClones.Add(normalManClone);

        // Spawn the enemy and assign it the Zombie character
        GameObject enemyClone = Instantiate(enemyPrefab);
        enemyClone.transform.position = new Vector3(5, 0, -5);
        enemyClone.GetComponent<CharacterComponent>().AssignCharacter(Zombie);
        characterClones.Add(enemyClone);
    }
}
