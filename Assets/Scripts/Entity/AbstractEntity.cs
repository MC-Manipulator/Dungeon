using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
    private Collider _collider;
    public EntityType entityType;
    public float currentHealth;
    public float maxHealth;
    public float attack;
    public float defence;
    public float speed;
    public Vector2 mapPosition;
    public float attackRange;

    protected abstract float Damage();

    protected abstract void Die();

    public abstract float Hurt(float damage);

    protected abstract void Action();

    public abstract void Attack(AbstractEntity target);

    protected AbstractEntity[] GetCollidingColliders()
    {
        // ʹ��Physics.OverlapBox����ȡ�뵱ǰ��ײ��Ӵ���������ײ��
        // ע�⣺�������ʹ�õ���BoxCollider�������ʹ�õ����������͵���ײ�壬�������Ҫ�����������
        if (_collider == null)
        {
            Debug.LogError("û���ҵ�BoxCollider�������ȷ�������Ϸ�����Ϲ�����BoxCollider");
            return new AbstractEntity[0];
        }

        Collider[] colliders = Physics.OverlapBox(_collider.bounds.center, _collider.bounds.extents);

        AbstractEntity[] entities = new AbstractEntity[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
            entities[i] = colliders[i].GetComponent<AbstractEntity>();

        return entities;
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

    private void Start()
    {
        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.LogError("û���ҵ���ײ�������");
        }
    }
}

public enum EntityType
{
    Creature,
    Trap
}