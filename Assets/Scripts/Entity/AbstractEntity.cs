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
        // 使用Physics.OverlapBox来获取与当前碰撞体接触的所有碰撞体
        // 注意：这个例子使用的是BoxCollider，如果你使用的是其他类型的碰撞体，你可能需要调整这个方法
        if (_collider == null)
        {
            Debug.LogError("没有找到BoxCollider组件，请确保你的游戏对象上挂载了BoxCollider");
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
            Debug.LogError("没有找到碰撞体组件！");
        }
    }
}

public enum EntityType
{
    Creature,
    Trap
}