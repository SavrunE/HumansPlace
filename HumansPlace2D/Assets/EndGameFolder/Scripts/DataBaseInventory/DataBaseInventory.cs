using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBaseInventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
}
[System.Serializable]
public class Item
{
    public int ID;
    public string name;
    public Sprite img;
}