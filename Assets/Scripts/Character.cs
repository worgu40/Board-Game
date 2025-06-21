using JetBrains.Annotations;
using UnityEngine;

public class Character
{
    public string characterName;
    public int health;
    public int throwRange;
    public string weight;
    public int movementRange;
    public float damage;

    public Character(string name, int hp, int throwDist, string w, int moveDist, float dmg)
    {
        characterName = name;
        health = hp;
        throwRange = throwDist;
        weight = w;
        movementRange = moveDist;
        damage = dmg;
    }
}

public class AverageHero : Character
{
    public AverageHero() : base("AverageHero", 15, 2, "Medium", 2, 1)
    { 
        
    }
}

public class VeryNormalMan : Character
{
    public VeryNormalMan() : base("VeryNormalMan", 18, 1, "Large", 3, 1)
    { 
        
    }
}
public class Zombie : Character
{
    public Zombie() : base("Zombie", 10, 1, "Medium", 1, 5)
    { 
        
    }
}
