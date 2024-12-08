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
}

public enum CreatureType
{
    Monster,
    Explorer,
    Netural
}