using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LootSystem
{
    public class InventoryUI : MonoBehaviour
    {
        public GameObject inventoryPanel;  // 用于显示仓库的面板
        public GameObject itemPrefab;      // 用于显示物品的UI预制体
        public Transform itemsParent;      // 存放物品的容器（如ScrollView中的Content）

        private List<ILoot> playerInventory = new List<ILoot>();  // 玩家仓库中的物品列表

        // 显示仓库UI
        public void OpenInventory()
        {
            inventoryPanel.SetActive(true);
            UpdateInventoryUI();
        }

        // 关闭仓库UI
        public void CloseInventory()
        {
            inventoryPanel.SetActive(false);
        }

        // 更新仓库UI
        private void UpdateInventoryUI()
        {
            // 清空现有物品
            foreach (Transform child in itemsParent)
            {
                Destroy(child.gameObject);
            }

            // 根据玩家仓库中的物品动态创建UI
            foreach (ILoot loot in playerInventory)
            {
                GameObject itemUI = Instantiate(itemPrefab, itemsParent);
                ItemUI item = itemUI.GetComponent<ItemUI>();

                // 设置物品信息
                item.SetItemInfo(loot);
            }
        }

        // 添加物品到仓库
        public void AddItemToInventory(ILoot item)
        {
            playerInventory.Add(item);
            UpdateInventoryUI();
        }

        // 丢弃物品
        public void DiscardItem(ILoot item)
        {
            playerInventory.Remove(item);
            UpdateInventoryUI();
        }
    }
}
