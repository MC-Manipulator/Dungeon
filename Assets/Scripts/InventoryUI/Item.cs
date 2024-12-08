using System.Collections;
using System.Collections.Generic;
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
    public bool Stackable = false;

    public enum ItemType{
        Book,
        Health,
        Weapon,
        Consumption
    }

}
