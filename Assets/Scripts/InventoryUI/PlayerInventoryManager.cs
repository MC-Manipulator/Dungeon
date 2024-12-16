using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[DefaultExecutionOrder(-100)]

public class PlayerInventoryManager : MonoBehaviour
{   
    public static PlayerInventoryManager Instance;
    public List<Item> Items;
    public Transform ItemContent;
    public GameObject InventoryItem;
    public PlayerItemController Helmet;
    public PlayerItemController Armor;
    public PlayerItemController Accessory;
    public FastInventoryManager fastInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 防止场景切换时被销毁
            Debug.Log("PlayerInventoryManager initialized.");
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 确保只有一个实例存在
        }
    }

    public void RemoveWeapon(PlayerItemController toRemove)
    {
        if(toRemove.item == null) 
            return;

        GameObject obj = toRemove.gameObject;
        Image slotImage = obj.transform.Find("SlotImage").GetComponent<Image>();
        slotImage.sprite = null;
        Color newColor = slotImage.color;
        newColor.a = 0.3f;
        slotImage.color = newColor;

        InventoryManager.Instance.Add(toRemove.item);
        toRemove.item = null;
        InventoryManager.Instance.ListItems();
    }
    
    public void EquippedWeapon(Item item)
    {
        switch(item.itemType)
        {
            case Item.ItemType.Helmet:
                SetItem(item, Helmet);
                break;

            case Item.ItemType.Armor:
                SetItem(item, Armor);
                break;

            case Item.ItemType.Accessory:
                SetItem(item, Accessory);
                break;
        }
    }
    
    public void SetItem(Item item, PlayerItemController toDo)
    {
        toDo.item = item;
        GameObject obj = toDo.gameObject;
        Image slotImage = obj.transform.Find("SlotImage").GetComponent<Image>();
        slotImage.sprite = item.icon;

        // 设置透明度
        Color newColor = slotImage.color;
        newColor.a = 1f; // 设置透明度
        slotImage.color = newColor;
    }

    //外部接口
    public Item GetHelmet()
    {
        return Helmet.item;
    }

    public Item GetArmor()
    {
        return Armor.item;
    }

    public Item GetAccessory()
    {
        return Accessory.item;
    }

    public bool IsHelmetExist()
    {
        return Helmet.item != null;
    }

    public bool IsArmorExist()
    {
        return Armor.item != null;
    }

    public bool IsAccessoryExist()
    {
        return Accessory.item != null;
    }
}
