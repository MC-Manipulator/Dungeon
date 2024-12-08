using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractCreature : AbstractEntity
{
    public new EntityType entityType = EntityType.Creature;
    public CreatureType creatureType;   //  ��������
    public float movableDistance;       //  ���ƶ�����
    public float currentSan;            //  sanֵ��Ӱ����ߡ�
    public float maxSan;                //  ���sanֵ��
    public float res;                   //  ���ԣ�Ӱ�챻����ʱsanֵ�ĵֿ��̶ȡ�

    protected Vector2 Move(Vector2 targat)
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
        // �����˺�ֵ
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

    protected AbstractCreature[] GetCollidingCreature(CreatureType type)
    {
        AbstractEntity[] entitys = GetCollidingColliders();
        List<ValueTuple<AbstractCreature, float>> explorers = new();
        foreach (AbstractEntity entity in entitys)
        {
            if (entity.entityType == EntityType.Creature)
            {
                AbstractCreature creature = (AbstractCreature)entity;
                if (creature.creatureType == type)
                {
                    float distance = Vector2.Distance(mapPosition, creature.mapPosition);
                    explorers.Add((creature, distance));
                }
            }
        }
        explorers.Sort((x, y) => x.Item2.CompareTo(y.Item2));

        return explorers.Select(x => x.Item1).ToArray();
    }
}

public enum CreatureType
{
    Monster,
    Explorer,
    Netural
}