namespace LootSystem
{
    // ս��Ʒ�ӿڣ���������ս��Ʒ��ͨ�õ���Ϊ
    public interface ILoot
    {
        string ItemName { get; set; }     // ��Ʒ����
        string ItemType { get; set; }     // ��Ʒ���ͣ����磺���������ϵȣ�
        int Quantity { get; set; }        // ��Ʒ����
        bool IsPickedUp { get; set; }     // ��Ʒ�Ƿ�ʰȡ

        void OnLootDropped();             // ��Ʒ����ʱ�Ĵ���
        void OnLootPickedUp();            // ��Ʒ��ʰȡʱ�Ĵ���
    }
}
