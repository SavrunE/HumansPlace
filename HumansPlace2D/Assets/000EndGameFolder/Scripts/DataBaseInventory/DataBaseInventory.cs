using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseInventory : MonoBehaviour
{
    public List<Item> Items = new List<Item>();
}
[System.Serializable]
public class Item
{
    public int ID;
    public string Name;
    public Sprite Image;
}