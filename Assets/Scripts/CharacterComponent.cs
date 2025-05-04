using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    private Character character;

    public void AssignCharacter(Character newCharacter)
    {
        character = newCharacter;
        Debug.Log($"Assigned character: {character.characterName}");
    }

    public string CharacterName;
    public int Health;
    public int ThrowRange;
    public string Weight;
    public int MovementRange;

    void Start()
    {
        if (character != null)
        {
            CharacterName = character.characterName;
            Health = character.health;
            ThrowRange = character.throwRange;
            Weight = character.weight;
            MovementRange = character.movementRange;
        }
    }

    void Update()
    {
        // Removed health update logic to prevent affecting all clones simultaneously
    }

    public void UpdateStatsFromCharacter()
    {
        if (character != null)
        {
            CharacterName = character.characterName;
            Health = character.health;
            ThrowRange = character.throwRange;
            Weight = character.weight;
            MovementRange = character.movementRange;
        }
    }

    public void ModifyHealth(int amount)
    {
        if (character != null)
        {
            character.health = Mathf.Max(0, character.health + amount); // Prevent health from going below 0
            Health = character.health; // Update the local Health property
        }
    }
}
