using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public List<Item> Items;
    public Item item;
    public Button RemoveButton;

    public Text ItemNum;

    public void Awake(){
        ItemNum = gameObject.transform.Find("ItemNum").GetComponent<Text>();
    }
    public void RemoveItem(){
        Items.RemoveAt(Items.Count - 1);
        updateNum();
        if(Items.Count == 0){
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
        }
    }

    public void AddItem(List<Item> items){
            Items = items;
            item = Items[0];
            updateNum();
    }
    public void UseItem(){
        switch(item.itemType){
            case Item.ItemType.Health:
                Player.Instance.IncreaseHealth(item.value);
                break;
            case Item.ItemType.Book:
                Player.Instance.IncreaseExp(item.value);
                break;
            case Item.ItemType.Consumption:
                break;
            case Item.ItemType.Weapon:
                break;
        }
        RemoveItem();
    }

    public void updateNum(){
          if (ItemNum != null)
        {
            ItemNum.text = Items.Count.ToString();
        }
        else
        {
            UnityEngine.Debug.LogWarning("ItemNum Text component is not assigned.");
        }
    }
}
