using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractExplorer : AbstractCreature
{
    private void Start()
    {
        entityType = EntityType.Explorer;
    }

    protected override void Action()
    {
        Debug.Log($"{name} ̽��������̽��...");
    }

    public override float GetActionSpeedCost()
    {
        return 1.0f; // ÿ���ж����ٶ�����
    }

    protected override float Damage()
    {
        return attack;
    }

    protected override void Die()
    {
        Debug.Log($"{name} ̽����������");
    }

    public override float Hurt(float damage)
    {
        float actualDamage = Mathf.Max(0, damage - defence);
        currentHealth -= actualDamage;
        if (currentHealth <= 0)
        {
            Die();
        }
        return actualDamage;
    }

    public override void Attack(AbstractEntity target)
    {
        Debug.Log($"{name} ���� {target.name}");
        target.Hurt(Damage());
    }
}
