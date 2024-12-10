namespace LootSystem
{
    public class LootFactory
    {
        // 根据类型生成不同的战利品
        public static ILoot CreateLoot(string itemType)
        {
            ILoot loot = null;

            switch (itemType)
            {
                case "Potion":
                    loot = new PotionLoot();
                    break;
                case "Weapon":
                    loot = new WeaponLoot();
                    break;
                default:
                    loot = new GenericLoot();
                    break;
            }

            return loot;
        }
    }

    // 继承ILoot接口的具体战利品类
    public class PotionLoot : ILoot
    {
        public string ItemName { get; set; } = "Potion";
        public string ItemType { get; set; } = "Consumable";
        public int Quantity { get; set; } = 1;
        public bool IsPickedUp { get; set; } = false;

        public void OnLootDropped() { Debug.Log("Potion dropped!"); }
        public void OnLootPickedUp() { Debug.Log("Potion picked up!"); }
    }

    public class WeaponLoot : ILoot
    {
        public string ItemName { get; set; } = "Sword";
        public string ItemType { get; set; } = "Weapon";
        public int Quantity { get; set; } = 1;
        public bool IsPickedUp { get; set; } = false;

        public void OnLootDropped() { Debug.Log("Weapon dropped!"); }
        public void OnLootPickedUp() { Debug.Log("Weapon picked up!"); }
    }

    public class GenericLoot : ILoot
    {
        public string ItemName { get; set; } = "Generic Item";
        public string ItemType { get; set; } = "Miscellaneous";
        public int Quantity { get; set; } = 1;
        public bool IsPickedUp { get; set; } = false;

        public void OnLootDropped() { Debug.Log("Generic item dropped!"); }
        public void OnLootPickedUp() { Debug.Log("Generic item picked up!"); }
    }
}
