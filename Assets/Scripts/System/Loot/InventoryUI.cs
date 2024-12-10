using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LootSystem
{
    public class InventoryUI : MonoBehaviour
    {
        public GameObject inventoryPanel;  // ������ʾ�ֿ�����
        public GameObject itemPrefab;      // ������ʾ��Ʒ��UIԤ����
        public Transform itemsParent;      // �����Ʒ����������ScrollView�е�Content��

        private List<ILoot> playerInventory = new List<ILoot>();  // ��Ҳֿ��е���Ʒ�б�

        // ��ʾ�ֿ�UI
        public void OpenInventory()
        {
            inventoryPanel.SetActive(true);
            UpdateInventoryUI();
        }

        // �رղֿ�UI
        public void CloseInventory()
        {
            inventoryPanel.SetActive(false);
        }

        // ���²ֿ�UI
        private void UpdateInventoryUI()
        {
            // ���������Ʒ
            foreach (Transform child in itemsParent)
            {
                Destroy(child.gameObject);
            }

            // ������Ҳֿ��е���Ʒ��̬����UI
            foreach (ILoot loot in playerInventory)
            {
                GameObject itemUI = Instantiate(itemPrefab, itemsParent);
                ItemUI item = itemUI.GetComponent<ItemUI>();

                // ������Ʒ��Ϣ
                item.SetItemInfo(loot);
            }
        }

        // �����Ʒ���ֿ�
        public void AddItemToInventory(ILoot item)
        {
            playerInventory.Add(item);
            UpdateInventoryUI();
        }

        // ������Ʒ
        public void DiscardItem(ILoot item)
        {
            playerInventory.Remove(item);
            UpdateInventoryUI();
        }
    }
}
