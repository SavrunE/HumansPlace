using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryIcon : MonoBehaviour
{
    [SerializeField] List<WeaponData> StartWeapons = new List<WeaponData>();
    public List<WeaponData> inventoryWeapon = new List<WeaponData>();
    void Start()
    {
        for (int i = 0; i < StartWeapons.Count; i++)
            AddItem(StartWeapons[i]);

    }
    void Update()
    {

    }
    void AddItem(WeaponData item)
    {
        inventoryWeapon.Add(item);
    }
}
