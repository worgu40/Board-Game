using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    public string characterName;
    public int health;
    public int throwRange;
    public string weight;
    public int movementRange;
    public float damage;
    public CharacterManager charManager;
    [HideInInspector]
    public bool placed = false;

    void Start()
    {
        charManager = GameObject.FindGameObjectWithTag("charManager").GetComponent<CharacterManager>();
    }

    void Update()
    {

    }
    public void AssignCharacter(string character)
    {
        charManager = GameObject.FindGameObjectWithTag("charManager").GetComponent<CharacterManager>();
        Character assignedChar = charManager.characterDict[character];
        characterName = assignedChar.characterName;
        health = assignedChar.health;
        throwRange = assignedChar.throwRange;
        weight = assignedChar.weight;
        movementRange = assignedChar.movementRange;
        damage = assignedChar.damage;
    }
}
