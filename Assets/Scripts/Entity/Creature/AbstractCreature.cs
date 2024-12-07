using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCreature : AbstractEntity
{
    public new EntityType entityType = EntityType.Creature;
    public CreatureType creatureType;
    public float currentSan;    //san值，影响决策。
    public float maxSan;        //最大san值。
    public float res;           //抗性，影响被攻击时san值的抵抗程度。

    protected abstract void Move();

    protected abstract void Action();
}

public enum CreatureType
{
    Monster,
    Explorer,
    Netural
}