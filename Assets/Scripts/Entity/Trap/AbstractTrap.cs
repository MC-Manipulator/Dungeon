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
}