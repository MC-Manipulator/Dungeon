using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class FastInventoryManager : MonoBehaviour
{
    
    public Dictionary<int,FastItemController> slotItems;

    public void AddItem(FastItemController newItem,int index){
        slotItems = new Dictionary<int, FastItemController>();
        slotItems.Add(index,newItem);
    }

    public void RemoveItem(int index){
        slotItems.Remove(index);
        slotItems[index].RemoveItem();
    }

    //外部接口，用于获取快捷栏物品
    public Item GetItem(int index)
    {
        return slotItems[index].item;
    }
    public bool itemExist(int index){
        if(slotItems.ContainsKey(index)){
            return true;
        }
        return false;
    }
}
