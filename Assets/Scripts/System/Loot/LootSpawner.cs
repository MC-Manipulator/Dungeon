using UnityEngine;

namespace LootSystem
{
    public class LootSpawner : MonoBehaviour
    {
        public GameObject lootPrefab;  // 战利品的预制体

        // 在怪物死亡时调用，生成战利品
        public void SpawnLoot(Vector3 position, string itemName, string itemType, int quantity)
        {
            GameObject loot = Instantiate(lootPrefab, position, Quaternion.identity);  // 在指定位置生成战利品
            LootItem lootItem = loot.GetComponent<LootItem>();

            // 设置战利品的随机属性或传递给定的属性
            lootItem.ItemName = itemName;
            lootItem.ItemType = itemType;
            lootItem.Quantity = quantity;

            lootItem.OnLootDropped();  // 触发掉落行为
        }
    }
}
