using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] InventoryIcon targetInventory;
    [SerializeField] RectTransform itemsPanel;
    private void Start()
    {
       
    }
    void Redraw()
    {
        for (int i = 0; i < targetInventory.inventoryWeapon.Count; i++)
        {
            var item = targetInventory.inventoryWeapon[i];
            var icon = new GameObject("Icon");
            icon.AddComponent<Image>().sprite = item.Icon;
        }
    }
}
