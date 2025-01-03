using UnityEngine;
using UnityEngine.UI;

namespace LootSystem
{
    public class ItemUI : MonoBehaviour
    {
        public Text itemNameText;      // ��ʾ��Ʒ����
        public Text itemQuantityText;  // ��ʾ��Ʒ����
        public Button discardButton;   // ������ť

        private ILoot loot;  // ��ǰ��Ʒ

        // ������ƷUI����ʾ����
        public void SetItemInfo(ILoot item)
        {
            loot = item;
            itemNameText.text = loot.ItemName;
            itemQuantityText.text = "x" + loot.Quantity.ToString();

            // ���ö�����ť���¼�
            discardButton.onClick.AddListener(DiscardItem);
        }

        // ������Ʒ
        private void DiscardItem()
        {
            // ��InventoryUI�ж�����Ʒ
            FindObjectOfType<InventoryUI>().DiscardItem(loot);
        }
    }
}
