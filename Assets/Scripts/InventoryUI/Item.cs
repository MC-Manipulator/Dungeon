using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public int num;
    public Sprite icon;
    public ItemType itemType;
    public int stackSize = 99;
    public bool Stackable = true;//物品默认可堆叠

    public ItemValue itemValue;

    public enum ItemType{
        Book,
        Health,
        Consumption,
        Helmet,
        Armor,
        Accessory
    }

    public enum ItemValue{
        Common = 1,//color:black
        Rare = 2,//color:green
        Epic = 3,//color:purple
        Legendary = 4//color:red
    }

}
