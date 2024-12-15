using UnityEngine;

public class TestExplorer : AbstractEntity
{
    private void Start()
    {
        entityType = EntityType.Explorer; // ����ʵ������Ϊ̽����
        currentHealth = 100;
        maxHealth = 100;
        attack = 15;
        defence = 5;
        // speed ��ͨ�� Unity �༭������
    }

    protected override float Damage()
    {
        return attack; // ����̽���ߵĹ�����
    }

    protected override void Die()
    {
        Debug.Log($"{name}��̽���ߣ�������");
    }

    public override float Hurt(float damage)
    {
        float actualDamage = Mathf.Max(damage - defence, 0); // ���������˺�
        currentHealth -= actualDamage;
        Debug.Log($"{name}��̽���ߣ��ܵ� {actualDamage} ���˺���ʣ������ֵ��{currentHealth}");
        if (currentHealth <= 0) Die();
        return actualDamage;
    }

    protected override void Action()
    {
        Debug.Log($"{name}��̽���ߣ�����̽��...");
    }

    public override float GetActionSpeedCost()
    {
        return 1.2f; // ̽�����ж��ٶ�����
    }

    public override void Attack(AbstractEntity target)
    {
        Debug.Log($"{name}��̽���ߣ������� {target.name}��");
        target.Hurt(Damage());
    }
}
