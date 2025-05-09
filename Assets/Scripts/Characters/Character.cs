using UnityEngine;

public class Character
{
    public string characterName;
    public int health;
    public int throwRange;
    public string weight;
    public int movementRange;
    public float damage;

    // Constructor to initialize character attributes
    public Character(string name, int hp, int throwDist, string w, int moveDist, float dmg)
    {
        characterName = name;
        health = hp;
        throwRange = throwDist;
        weight = w;
        movementRange = moveDist;
        damage = dmg;
    }

    public virtual void DisplayStats()
    {
        Debug.Log($" Name: {characterName} \n Health: {health} \n Throw Range: {throwRange} \n Weight: {weight} \n Movement Range: {movementRange}");
    }
}
public class AverageHero : Character
{
    public AverageHero() : base("AverageHero", 15, 2, "Medium", 2, 1) { }
}

public class VeryNormalMan : Character
{
    public VeryNormalMan() : base("VeryNormalMan", 18, 1, "Large", 3, 1) { }
}
public class Zombie : Character
{
    public Zombie() : base("Zombie", 10, 1, "Medium", 1, 5) { }
}