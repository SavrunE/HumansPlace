using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponIcon", fileName = "WeaponView")]
public class WeaponData : ScriptableObject
{
    public Sprite Icon;
    [SerializeField]
    private float price = 500;
    public float Price
    {
        get { return price; }
        set { }
    }
    [SerializeField]
    private float damage = 2f;
    public float Damage
    {
        get { return damage; }
        set { }
    }
}
