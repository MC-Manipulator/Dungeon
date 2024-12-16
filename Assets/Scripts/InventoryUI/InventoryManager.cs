using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Dictionary<int,List<Item>> Items = new Dictionary<int,List<Item>>();
    public List<Item> unstackableItems = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;
    public List<InventoryItemController> InventoryItems;
    // public List<InventoryItemController> InventoryItems;
    public bool Sorted = false;
    
    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        //可堆叠
        if (item.Stackable)
        {
            if (!Items.ContainsKey(item.id))
            {
                Items[item.id] = new List<Item>();
            }
            Items[item.id].Add(item);
        }

        //不可堆叠
        if(!item.Stackable)
        {
            unstackableItems.Add(item);
        }
        ListItems();
    }

    public void Remove(Item item)
    {
        if(item.Stackable)
        {
            Items.Remove(item.id);
            
        }

        if(!item.Stackable)
        {
            unstackableItems.Remove(item);
        }
    }
    public void ListItems()
    {
        //清理掉层级中物品栏的子对象
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
            ClearInventoryItems();
        }

        var allStackableItems = Items.Values.ToList();

        // 提取所有不可堆叠物品
        var allUnstackableItems = unstackableItems.ToList();

        if (Sorted)
        {
            // 按照价值排序
            allStackableItems.Sort((a, b) => a[0].itemValue.CompareTo(b[0].itemValue));
            allUnstackableItems.Sort((a, b) => a.itemValue.CompareTo(b.itemValue));
            // 添加可堆叠物品
            foreach (var itemList in allStackableItems)
            {
                GameObject obj = Instantiate(InventoryItem, ItemContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
                var ITC = obj.GetComponent<InventoryItemController>();
                ITC.AddItem(itemList);

                // 加入到 InventoryItems 列表中，方便后面对 item 的操作
                InventoryItems.Add(ITC);

                // 使用第一个物品的信息来设置 UI
                Item firstItem = itemList[0];
                itemName.text = firstItem.itemName;
                itemIcon.preserveAspect = true;
                itemIcon.sprite = firstItem.icon;

                if (EnableRemove.isOn)
                {
                    removeButton.gameObject.SetActive(true);
                }
            }

            // 添加不可堆叠物品
            foreach (var item in unstackableItems)
            {
                GameObject obj = Instantiate(InventoryItem, ItemContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
                var ITC = obj.GetComponent<InventoryItemController>();
                ITC.AddUnStackableItem(item);

                // 加入到 InventoryItems 列表中，方便后面对 item 的操作
                InventoryItems.Add(ITC);

                itemName.text = item.itemName;
                itemIcon.preserveAspect = true;
                itemIcon.sprite = item.icon;

                if (EnableRemove.isOn)
                {
                    removeButton.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            // 按照获取顺序排序（无需额外排序）
            //添加可堆叠物品
            foreach (var item in Items)
            {
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

                if(EnableRemove.isOn)
                {
                    removeButton.gameObject.SetActive(true);
                }
            }
            //添加不可堆叠物品
            foreach(var item in unstackableItems)
            {
                GameObject obj = Instantiate(InventoryItem,ItemContent);
                var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
                var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
                var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
                var ITC = obj.GetComponent<InventoryItemController>();
                ITC.AddUnStackableItem(item);
                // 加入到InventoryItems列表中，方便后面对item的操作
                InventoryItems.Add(ITC);
            
                itemName.text = item.itemName;
                itemIcon.preserveAspect = true;
                itemIcon.sprite = item.icon;

                if(EnableRemove.isOn)
                {
                    removeButton.gameObject.SetActive(true);
                }
            }
        // SetInventoryItem();
        }
    }

    
    public void EnableItemsRemove()
    {
        if(EnableRemove.isOn)
        {//控制删除按钮的显示
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    // public void SetInventoryItem(){
    //     int index = 0;
    //     InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();
    //         InventoryItems[index++].AddItem(Items[index++]);
    // }

    public void ClearInventoryItems()
    {
        InventoryItems.Clear();
    }

    public void setSorted()
    {
        Sorted = !Sorted;
        ListItems();
    }

    //外部接口，用于系统调用
    //1、用于查找是否存在某对象
    public bool itemIsExisted(int itemId)
    {
        if(Items.ContainsKey(itemId) || unstackableItems.Exists(x => x.id == itemId))
        {
            //存在
            UnityEngine.Debug.Log("Item Exists");
            return true;
        }
        //默认不存在
        UnityEngine.Debug.Log("Item Not Exists");
        return false;
    }
    //2、用于获取某item
    public Item getItem(int itemId)
    {
        // 合并检查，避免重复
        if (Items.ContainsKey(itemId))
        {
        // 确保 Items[itemId] 不为空
            if (Items[itemId].Count > 0)
            {
                return Items[itemId][0];
            }
        } 
        else if (unstackableItems.Exists(x => x.id == itemId))
        {
            return unstackableItems.Find(x => x.id == itemId);
        }
        // 只在必要时记录日志
        UnityEngine.Debug.LogWarning("Item Not Exists, cant get it");
        return null; // 默认返回一个空的Item信息
    }
}
