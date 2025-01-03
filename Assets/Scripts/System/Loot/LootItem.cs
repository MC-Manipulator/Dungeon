using UnityEngine;

namespace LootSystem
{
    // 战利品接口，定义所有战利品类通用的行为
    public interface ILoot
    {
        string ItemName { get; set; }     // 物品名称
        string ItemType { get; set; }     // 物品类型（例如：武器、材料等）
        int Quantity { get; set; }        // 物品数量
        bool IsPickedUp { get; set; }     // 物品是否被拾取

        void OnLootDropped();             // 物品掉落时的处理
        void OnLootPickedUp();            // 物品被拾取时的处理
    }

    // 实现ILoot接口的具体战利品类
    public class LootItem : MonoBehaviour, ILoot
    {
        // 战利品的基本属性
        public string itemName;     // 物品名称
        public string itemType;     // 物品类型（例如：武器、材料等）
        public int quantity;        // 物品数量
        public bool isPickedUp = false; // 物品是否已被拾取

        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }

        public string ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public bool IsPickedUp
        {
            get { return isPickedUp; }
            set { isPickedUp = value; }
        }

        // 物品掉落时的显示（可扩展为动画、特效等）
        public void OnLootDropped()
        {
            // 在这里可以做物品掉落的效果
            Debug.Log("Loot dropped: " + itemName);
        }

        // 物品被拾取时的处理
        public void OnLootPickedUp()
        {
            if (isPickedUp) return;  // 防止重复拾取

            isPickedUp = true;
            Debug.Log(itemName + " picked up!");

            // 物品被拾取后销毁，或者将物品存入仓库
            Destroy(gameObject); // 摧毁物品对象
        }

        // 触发器检测碰撞，判断是否是玩家
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))  // 检查是否是玩家
            {
                OnLootPickedUp(); // 调用拾取方法
            }
        }
    }
}
