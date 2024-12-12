using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractCreature : AbstractEntity
{
    //public new EntityType entityType = EntityType.Creature; 不需要重新定义entityType，只需要赋值
    public CreatureType creatureType;   //  生物类型
    public float movableDistance;       //  可移动距离
    public float currentSan;            //  san值，影响决策。
    public float maxSan;                //  最大san值。
    public float res;                   //  抗性，影响被攻击时san值的抵抗程度。

    protected AbstractCreature()
    {
        entityType = EntityType.Creature; // 直接使用父类字段
    }

    protected Vector2 Move(Vector2 target)
    {
        throw new System.NotImplementedException();
    }

    protected override float Damage()
    {
        return attack;
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void Attack(AbstractEntity target)
    {
        // 计算伤害值
        float damageValue = Damage();
        if (damageValue > 0)
        {
            float hurt = target.Hurt(damageValue);
            currentSan += hurt;
            currentSan = Mathf.Clamp(currentSan, 0, maxSan);
        }
    }

    public override float Hurt(float damage)
    {
        float hurt = damage * defence;
        if (hurt < 0)
            hurt = 0;
        currentHealth -= hurt;
        currentSan -= hurt * res;
        currentSan = Mathf.Clamp(currentSan, 0, maxSan);
        if (currentHealth <= 0)
            Die();
        return hurt;
    }
}

public enum CreatureType
{
    Monster,
    Explorer,
    Netural
}