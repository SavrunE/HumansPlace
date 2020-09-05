using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour
{
    [SerializeField] KeyCode KeyToUseInventory;
    public DataBaseInventory Data;

    public List<ItemInventory> Items = new List<ItemInventory>();

    public GameObject ItemShow;

    public GameObject InventoryMain;

    public int MaxCount;
    public int MaxItemInOneCell;//кол-во предметов в 1 ячейке, нужно будет задать количество в зависимости от типа предмета.

    public Camera MainCamera;
    public EventSystem eventSystem;

    public int currentID;
    public ItemInventory currentItem;

    public RectTransform movingObject;
    public Vector3 offset;

    public GameObject backGround;


    private void Start()
    {
        if (Items.Count == 0)
        {
            AddGraphics();
        }
        for(int i = 0; i < MaxCount;i++)//test
        {
            AddItem(i, Data.Items[Random.Range(0, Data.Items.Count)], Random.Range(1, 99));
        }
        UpdateInventory();
    }
    public void Update()
    {
        if (currentID != -1)
        {
            MoveObject();
        }
        if (Input.GetKeyDown(KeyToUseInventory))
        {
            backGround.SetActive(!backGround.activeSelf);
            if (backGround.activeSelf)
            {
                UpdateInventory();
            }
        }
    }

    public void SearchForSameItem(Item item, int count)
    {
        for (int i = 0; i < MaxCount; i++)
        {
            if (Items[i].ID == item.ID)
            {
                if (Items[i].Count < MaxItemInOneCell)
                {
                    Items[i].Count += count;
                    if (Items[i].Count > MaxItemInOneCell)
                    {
                        count = Items[i].Count - MaxItemInOneCell;//128
                        Items[i].Count = MaxItemInOneCell / 2; //64;//чеего блять
                    }
                    else
                    {
                        count = 0;
                        i = MaxCount;
                    }
                }
            }
        }

        if (count > 0)
            for (int i = 0; i < MaxCount; i++)
            {
                if (Items[i].ID == 0)
                {
                    AddItem(i, item, count);
                    i = MaxCount;
                }
            }
    }

    public void AddItem(int id, Item item, int count)
    {
        Items[id].ID = item.ID; 
        Items[id].Count = count;
        Items[id].Item.GetComponent<Image>().sprite = item.Image;

        if (count > 1 && item.ID != 0)
        {
            Items[id].Item.GetComponentInChildren<Text>().text = count.ToString();  
        }
        else
        {
            Items[id].Item.GetComponentInChildren<Text>().text = "";
        }
    }
    //попробую скомпоновать 2 эти ↕ методы в 1 через var
    public void AddInventoryItem(int id, ItemInventory item)
    {
        Items[id].ID = item.ID; 
        Items[id].Count = item.Count;
        Items[id].Item.GetComponent<Image>().sprite = Data.Items[item.ID].Image;

        if (item.Count > 1 && item.ID != 0)
        {
            Items[id].Item.GetComponentInChildren<Text>().text = item.Count.ToString();  
        }
        else
        {
            Items[id].Item.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddGraphics()
    {
        for (int i = 0; i < MaxCount; i++)
        {
            GameObject newItem = Instantiate(ItemShow, InventoryMain.transform) as GameObject;
            newItem.name = i.ToString();

            ItemInventory itemInventory = new ItemInventory();
            itemInventory.Item = newItem;

            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0,0,0);
            rectTransform.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            Items.Add(itemInventory);

        }
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < MaxCount; i++)
        {
            if (Items[i].ID != 0 && Items[i].Count > 1)
                Items[i].Item.GetComponentInChildren<Text>().text = Items[i].Count.ToString();
            else
                Items[i].Item.GetComponentInChildren<Text>().text = "";
            Items[i].Item.GetComponent<Image>().sprite = Data.Items[Items[i].ID].Image;
        }
        
    }

    public void SelectObject()
    {
        if(currentID == -1)
        {
            currentID =  int.Parse(eventSystem.currentSelectedGameObject.name);
            currentItem = CopyInventoryItem(Items[currentID]);
            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = Data.Items[currentItem.ID].Image;

            AddItem(currentID,Data.Items[0],0);
        }
        else
        {
            ItemInventory II = Items[int.Parse(eventSystem.currentSelectedGameObject.name)];

            if (currentItem.ID != II.ID)
            {
                AddInventoryItem(currentID, II);

                AddInventoryItem(int.Parse(eventSystem.currentSelectedGameObject.name), currentItem);
            }
            else
            {
                if(II.Count + currentItem.Count <= MaxItemInOneCell)
                {
                    II.Count += currentItem.Count;
                }
                else
                {
                    AddItem(currentID, Data.Items[II.ID], II.Count + currentItem.Count - MaxItemInOneCell);
                    II.Count = MaxItemInOneCell;
                }
                II.Item.GetComponentInChildren<Text>().text = II.Count.ToString();
            }
            currentID = -1;
            movingObject.gameObject.SetActive(false);
        }
    }
    public void MoveObject()
    {
        Vector3 positionOffset = Input.mousePosition + offset;
        positionOffset.z = InventoryMain.GetComponent<RectTransform>().position.z;
        movingObject.position = MainCamera.ScreenToWorldPoint(positionOffset);
    }

    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();
        New.ID = old.ID;
        New.Item = old.Item;
        New.Count = old.Count;

        return New;
    }
}

[System.Serializable]
public class ItemInventory
{
    public int ID;
    public GameObject Item;

    public int Count;
}