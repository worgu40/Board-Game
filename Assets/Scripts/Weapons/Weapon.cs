using UnityEngine;

public class Weapon
{
    public string weaponName;
    public string weight;
    public int range;
    public int damage;
    public int minRoll;

    public Weapon(string name, string w, int slashRange, int dmg, int roll) {
        weaponName = name;
        weight = w;
        range = slashRange;
        damage = dmg;
        minRoll = roll;
    }
}

public class Boomerang : Weapon {
    public Boomerang() : base("Boomerang", "Medium", 1, 4, 5) { }
}
public class Katana : Weapon {
    public Katana() : base("Katana", "Medium", 2, 6, 6) { } 
}
public class SpaceBane : Weapon {
    public SpaceBane() : base("Space Bane", "Large", 1, 7, 6) { } 
}
public class OlReliable : Weapon {
    public OlReliable() : base("Ol' Reliable", "Medium", 1, 4, 6) { } 
}
    