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
}
