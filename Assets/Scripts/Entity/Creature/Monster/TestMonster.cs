using UnityEngine;

public class TestMonster : AbstractEntity
{
    private void Start()
    {
        entityType = EntityType.Monster; // ����ʵ������Ϊ����
        currentHealth = 120;
        maxHealth = 120;
        attack = 20;
        defence = 8;
        // speed ��ͨ�� Unity �༭������
    }

    protected override float Damage()
    {
        return attack; // ���ع���Ĺ�����
    }

    protected override void Die()
    {
        Debug.Log($"{name}�����������");
    }

    public override float Hurt(float damage)
    {
        float actualDamage = Mathf.Max(damage - defence, 0); // ���������˺�
        currentHealth -= actualDamage;
        Debug.Log($"{name}������ܵ� {actualDamage} ���˺���ʣ������ֵ��{currentHealth}");
        if (currentHealth <= 0) Die();
        return actualDamage;
    }

    protected override void Action()
    {
        Debug.Log($"{name}���������׼������...");
    }

    public override float GetActionSpeedCost()
    {
        return 1.5f; // �����ж��ٶ�����
    }

    public override void Attack(AbstractEntity target)
    {
        Debug.Log($"{name}����������� {target.name}��");
        target.Hurt(Damage());
    }
}
