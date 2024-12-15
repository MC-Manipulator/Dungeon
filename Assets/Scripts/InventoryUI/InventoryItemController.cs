using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Linq;

public class InventoryItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public List<Item> Items;
    public Item item;
    public UnityEngine.UI.Button RemoveButton;

    public Text ItemNum;
    public Text ItemName;

    private Vector3 startPosition;
    private Transform startParent;
    private bool isDragging = false;
    public RectTransform targetCanvasRectTransform;
    public RectTransform fastInventoryRectTransform;
    public GameObject canvasObject;

    public void Awake()
    {
        ItemNum = gameObject.transform.Find("ItemNum").GetComponent<Text>();
        ItemName = gameObject.transform.Find("ItemName").GetComponent<Text>();

        canvasObject = GameObject.FindGameObjectWithTag("Canvas");
        targetCanvasRectTransform = canvasObject.GetComponent<RectTransform>();

        fastInventoryRectTransform = GameObject.FindGameObjectWithTag("FastInventory").GetComponent<RectTransform>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {   
        isDragging = true;
        startPosition = transform.position;
        startParent = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {    
        isDragging = false;
        transform.SetParent(startParent);
        transform.position = startPosition;

        if(fastInventoryRectTransform != null && RectTransformUtility.RectangleContainsScreenPoint(fastInventoryRectTransform, Input.mousePosition))
        {
           
            FastItemController targetFastSlot = GetTargetFastSlot(eventData.position);

            if(targetFastSlot!= null)
            {
                //获取到目标快捷栏格子后继续进行操作
                string  slotName = targetFastSlot.gameObject.name;
                UnityEngine.Debug.Log("找到了目标格子"+slotName);
                targetFastSlot.item = item;
                targetFastSlot.UpdateSlot();
                PlayerInventoryManager.Instance.fastInventory.AddItem(targetFastSlot,slotName.Last(c => char.IsDigit(c)) - '0');
    
                RemoveItem();
            }
            else
            {
                UnityEngine.Debug.Log("Pointer not over any Slot within FastInventoryPanel.");
            }
        } // 检查鼠标位置是否在目标 Canvas 区域
        else if (targetCanvasRectTransform != null && RectTransformUtility.RectangleContainsScreenPoint(targetCanvasRectTransform, Input.mousePosition))
        {
            DropItem();
        }
        else 
            return;
        
    }



    public void RemoveItem()
    {

        if(item.Stackable)
        {
            Items.RemoveAt(Items.Count - 1);
            updateNum();
            if (Items.Count == 0)
            {
                InventoryManager.Instance.Remove(item);
                Destroy(gameObject);
            }
        }

        if(!item.Stackable)
        {
            InventoryManager.Instance.Remove(item);
            Destroy(gameObject);
        }
    }
    
    public void AddItem(List<Item> items)
    {
        Items = items;
        item = Items[0];
        updateNum();
    }

    public void AddUnStackableItem(Item item)
    {
        this.item = item;
        ItemNum.gameObject.SetActive(false);//
        
    }

    public void UseItem()
    {
        if(isDragging) 
            return;

        switch(item.itemType)
        {
            case Item.ItemType.Health:
                Player.Instance.IncreaseHealth(item.value);
                break;

            case Item.ItemType.Book:
                Player.Instance.IncreaseExp(item.value);
                break;

            case Item.ItemType.Consumption:
                break;

            case Item.ItemType.Armor://以胸甲为例子
                PlayerInventoryManager.Instance.EquippedWeapon(item);
                break;
        }

        RemoveItem();
    }

    public void updateNum()
    {
          
        if (ItemNum != null)
        {
            ItemNum.text = Items.Count.ToString();
            SetItemValue();
        }
        else
        {
            UnityEngine.Debug.LogWarning("ItemNum Text component is not assigned.");
        }
    }

    public void SetItemValue()
    {
        switch(item.itemValue)
        {
            case Item.ItemValue.Common:
                ItemName.color = Color.black;
                break;
            case Item.ItemValue.Rare:
                ItemName.color = Color.green;
                break;
            case Item.ItemValue.Epic:
                ItemName.color = new Color(0.5f, 0f, 0.5f); // 紫色
                break;
            case Item.ItemValue.Legendary:
                ItemName.color = Color.red;
                break;
        }
        
    }

    public void DropItem()
    {
        //先将物品移除
        RemoveItem();
        // UnityEngine.Debug.Log(item.itemName);
        // 输出调试信息，确认路径和 itemName 是否正确
        string path = "Prefabs/Entity/Item/" + item.itemName;
        // UnityEngine.Debug.Log("Trying to load prefab at path: " + path);
        // 尝试加载预制件
        GameObject itemPrefab = Resources.Load<GameObject>(path);
        if (itemPrefab == null)
        {
            // UnityEngine.Debug.LogError("Prefab not found at path: " + path);
            return;
        }
        // 实例化预制件
        GameObject instantiatedPrefab = Instantiate(itemPrefab);
        instantiatedPrefab.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        instantiatedPrefab.transform.SetParent(null);
    }

    private FastItemController GetTargetFastSlot(Vector2 mousePosition)
    {
        GameObject fastInventory = fastInventoryRectTransform.gameObject;
        Transform[] fastItemTransforms = fastInventory.GetComponentsInChildren<Transform>();
        
        foreach(Transform child in fastItemTransforms)
        {
            FastItemController fastItemController = child.GetComponent<FastItemController>();
            if(fastItemController!= null && RectTransformUtility.RectangleContainsScreenPoint(child.GetComponent<RectTransform>(), mousePosition))
            {
                return fastItemController;
            }
        }

        return null;
    }
}
