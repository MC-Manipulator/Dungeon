using UnityEngine;
using UnityEngine.UI;

namespace LootSystem
{
    public class ItemUI : MonoBehaviour
    {
        public Text itemNameText;      // 显示物品名称
        public Text itemQuantityText;  // 显示物品数量
        public Button discardButton;   // 丢弃按钮

        private ILoot loot;  // 当前物品

        // 设置物品UI的显示内容
        public void SetItemInfo(ILoot item)
        {
            loot = item;
            itemNameText.text = loot.ItemName;
            itemQuantityText.text = "x" + loot.Quantity.ToString();

            // 设置丢弃按钮的事件
            discardButton.onClick.AddListener(DiscardItem);
        }

        // 丢弃物品
        private void DiscardItem()
        {
            // 在InventoryUI中丢弃物品
            FindObjectOfType<InventoryUI>().DiscardItem(loot);
        }
    }
}
