using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* *** *** *** */
class Weapons : MonoBehaviour
{

    void Start()
    {

        var dagger = new Dagger();
        var curse = new Curse();

        DebugPrint.PrintAnyDamageSubject(dagger); // dagger damage info
        DebugPrint.PrintAnyDamageSubject(curse); // curse damage info

    }

}
public interface IDamage
{

    float MinDamage { get; }
    float MaxDamage { get; }
    float MinRange { get; }
    float MaxRange { get; }

}
public abstract class Weapon : IDamage
{

    protected float minDamage = 0f;
    protected float minRange = 1f;
    protected float maxRange = 2f;

    public float MinDamage { get { return minDamage; } }
    public float MaxDamage { get { return minDamage * 100f; } }
    public float MinRange { get { return minRange; } }
    public float MaxRange { get { return maxRange; } }

    public TypeOfWeapon WeaponType { get; }

    public Weapon(TypeOfWeapon weaponType)
    {
        WeaponType = weaponType;
    }

}

public enum TypeOfWeapon
{
    Sword,
    Axe,
    Hammer,
    Dagger
}

public abstract class Spell : IDamage
{

    protected float minDamage = 0f;
    protected float minRange = 1f;
    protected float maxRange = 2f;
 
    public float MinDamage { get { return minDamage; } }
    public float MaxDamage { get { return minDamage * 100f; } }
    public float MinRange { get { return minRange; } }
    public float MaxRange { get { return maxRange; } }

    public Spell() { }

}

/* *** *** *** */

public class Dagger : Weapon
{

    public Dagger() : base(TypeOfWeapon.Dagger)
    {
        minDamage = 25f;
        maxRange = 1.5f;
    }

}

public class Curse : Spell
{

    public Curse() : base()
    {
        minDamage = 15f;
        maxRange = 30f;
    }

}

/* *** *** *** */

public static class DebugPrint
{

    public static void PrintAnyDamageSubject(IDamage anyIDamageSubject)
    {
        Debug.Log($"Min Damage: {anyIDamageSubject.MinDamage}\tMax Damage: {anyIDamageSubject.MaxDamage }\tMin Range: {anyIDamageSubject.MinRange }\tMax Range: {anyIDamageSubject.MaxRange }");
    }

}
