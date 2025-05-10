using UnityEngine;
using System.Collections.Generic;

public class CharacterComponent : MonoBehaviour
{
    // Reference to the Character class
    private Character character; // STORES ATTRIBUTES, INITIALIZED FOR EVERY CHARACTER IN THIS COMPONENT.
    private List<Weapon> weapons = new(); // Each character has their own weapon inventory
    private int currentWeaponIndex = 0;

    public void AssignCharacter(Character newCharacter)
    {
        character = newCharacter;
        Debug.Log($"Assigned character: {character.characterName}");

        // Set the name of the GameObject to the character's name
        transform.parent.name = character.characterName;
        
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
    void Awake()
    {
        
    }

    public void UpdateWeapon(bool isActiveCharacter)
    {
        if (!isActiveCharacter) return; // Only process input for the active character

        // Assign weapons based on key presses
        if (Input.GetKeyDown(KeyCode.Alpha1)) AssignWeapon(new Boomerang());
        if (Input.GetKeyDown(KeyCode.Alpha2)) AssignWeapon(new Katana());
        if (Input.GetKeyDown(KeyCode.Alpha3)) AssignWeapon(new SpaceBane());
        if (Input.GetKeyDown(KeyCode.Alpha4)) AssignWeapon(new OlReliable());

        // Cycle through weapons with the F key
        if (Input.GetKeyDown(KeyCode.F) && weapons.Count > 0)
        {
            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Count;
            Debug.Log($"Current weapon: {weapons[currentWeaponIndex].weaponName}");
        }
    }

    public void AssignWeapon(Weapon newWeapon)
    {
        if (weapons.Count >= 3)
        {
            Debug.Log("Cannot equip more than 3 weapons.");
            return;
        }

        weapons.Add(newWeapon);
        Debug.Log($"Equipped weapon: {newWeapon.weaponName}");
    }

    public Weapon GetCurrentWeapon()
    {
        if (weapons.Count == 0) return null;
        return weapons[currentWeaponIndex];
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
