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

    public override float Hurt(float damage)
    {
        float hurt = damage * defence;
        if (hurt < 0)
            hurt = 0;
        currentHealth -= hurt;
        return hurt;
    }
}