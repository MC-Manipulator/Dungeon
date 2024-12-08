using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Dictionary<int,List<Item>> Items = new Dictionary<int,List<Item>>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;
    public List<InventoryItemController> InventoryItems;
    // public List<InventoryItemController> InventoryItems;
    
    private void Awake()
    {
        Instance = this;
    }
    public void Add(Item item){
        if (!Items.ContainsKey(item.id)){
        Items[item.id] = new List<Item>();
        }
        Items[item.id].Add(item);
    }
    public void Remove(Item item){
            Items.Remove(item.id);
    }
    public void ListItems(){
        //清理掉层级中物品栏的子对象
        foreach (Transform item in ItemContent){
            Destroy(item.gameObject);
            ClearInventoryItems();
        }

        foreach (var item in Items){
            //生成一个item对象
            GameObject obj = Instantiate(InventoryItem,ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            var ITC = obj.GetComponent<InventoryItemController>();
            ITC.AddItem(item.Value);
            // 加入到InventoryItems列表中，方便后面对item的操作
            InventoryItems.Add(ITC);
            
            itemName.text = item.Value[0].itemName;
            itemIcon.preserveAspect = true;
            itemIcon.sprite = item.Value[0].icon;

            if(EnableRemove.isOn){
                removeButton.gameObject.SetActive(true);
            }
        }
        // SetInventoryItem();
    }
    
    
    public void EnableItemsRemove(){
        if(EnableRemove.isOn){//控制删除按钮的显示
            foreach(Transform item in ItemContent){
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }else{
            foreach(Transform item in ItemContent){
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    // public void SetInventoryItem(){
    //     int index = 0;
    //     InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();
    //         InventoryItems[index++].AddItem(Items[index++]);
    // }

    public void ClearInventoryItems(){
        InventoryItems.Clear();
    }
}
