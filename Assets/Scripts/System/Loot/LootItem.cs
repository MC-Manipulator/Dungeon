using UnityEngine;

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

    // ʵ��ILoot�ӿڵľ���ս��Ʒ��
    public class LootItem : MonoBehaviour, ILoot
    {
        // ս��Ʒ�Ļ�������
        public string itemName;     // ��Ʒ����
        public string itemType;     // ��Ʒ���ͣ����磺���������ϵȣ�
        public int quantity;        // ��Ʒ����
        public bool isPickedUp = false; // ��Ʒ�Ƿ��ѱ�ʰȡ

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

        // ��Ʒ����ʱ����ʾ������չΪ��������Ч�ȣ�
        public void OnLootDropped()
        {
            // �������������Ʒ�����Ч��
            Debug.Log("Loot dropped: " + itemName);
        }

        // ��Ʒ��ʰȡʱ�Ĵ���
        public void OnLootPickedUp()
        {
            if (isPickedUp) return;  // ��ֹ�ظ�ʰȡ

            isPickedUp = true;
            Debug.Log(itemName + " picked up!");

            // ��Ʒ��ʰȡ�����٣����߽���Ʒ����ֿ�
            Destroy(gameObject); // �ݻ���Ʒ����
        }

        // �����������ײ���ж��Ƿ������
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))  // ����Ƿ������
            {
                OnLootPickedUp(); // ����ʰȡ����
            }
        }
    }
}
