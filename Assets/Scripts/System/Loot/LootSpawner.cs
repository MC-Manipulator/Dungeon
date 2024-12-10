using UnityEngine;

namespace LootSystem
{
    public class LootSpawner : MonoBehaviour
    {
        public GameObject lootPrefab;  // ս��Ʒ��Ԥ����

        // �ڹ�������ʱ���ã�����ս��Ʒ
        public void SpawnLoot(Vector3 position, string itemName, string itemType, int quantity)
        {
            GameObject loot = Instantiate(lootPrefab, position, Quaternion.identity);  // ��ָ��λ������ս��Ʒ
            LootItem lootItem = loot.GetComponent<LootItem>();

            // ����ս��Ʒ��������Ի򴫵ݸ���������
            lootItem.ItemName = itemName;
            lootItem.ItemType = itemType;
            lootItem.Quantity = quantity;

            lootItem.OnLootDropped();  // ����������Ϊ
        }
    }
}
