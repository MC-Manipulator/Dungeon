using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTrap : AbstractEntity
{
    public new EntityType entityType = EntityType.Trap;

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
        float damageValue = Damage();
        target.Hurt(damageValue);
    }

    protected override void Action()
    {
        AbstractCreature[] explorers = GetCollidingCreature(CreatureType.Explorer);
        foreach (AbstractCreature explorer in explorers)
            Attack(explorer);
    }

    public override float Hurt(float damage)
    {
        float hurt = damage * defence;
        if (hurt < 0)
            hurt = 0;
        currentHealth -= hurt;
        return hurt;
    }
}