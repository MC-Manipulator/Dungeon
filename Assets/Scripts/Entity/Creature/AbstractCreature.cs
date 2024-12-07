using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCreature : AbstractEntity
{
    public new EntityType entityType = EntityType.Creature;
    public CreatureType creatureType;
    public float currentSan;    //sanֵ��Ӱ����ߡ�
    public float maxSan;        //���sanֵ��
    public float res;           //���ԣ�Ӱ�챻����ʱsanֵ�ĵֿ��̶ȡ�

    protected abstract void Move();

    protected abstract void Action();
}

public enum CreatureType
{
    Monster,
    Explorer,
    Netural
}