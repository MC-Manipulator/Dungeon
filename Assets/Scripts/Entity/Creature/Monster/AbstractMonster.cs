using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMonster : AbstractCreature
{
    private void Start()
    {
        entityType = EntityType.Monster;
    }

    protected override void Action()
    {
        Debug.Log($"{name} 怪物正在攻击...");
    }

    public override float GetActionSpeedCost()
    {
        return 1.5f; // 每次行动的速度消耗
    }

    protected override float Damage()
    {
        return attack;
    }

    protected override void Die()
    {
        Debug.Log($"{name} 怪物已死亡");
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
        Debug.Log($"{name} 攻击 {target.name}");
        target.Hurt(Damage());
    }
}