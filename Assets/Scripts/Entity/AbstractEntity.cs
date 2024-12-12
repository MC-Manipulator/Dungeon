using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
    private Collider _collider;     // 当前碰撞体
    public EntityType entityType;   // 实体类型
    public float currentHealth;     // 当前健康值
    public float maxHealth;         // 最大健康值
    public float attack;            // 攻击力
    public float defence;           // 防御力
    public float speed;             // 速度
    public Vector2 mapPosition;     // 地图坐标
    public float attackRange;       // 攻击范围

    protected abstract float Damage();

    protected abstract void Die();

    public abstract float Hurt(float damage);

    //提供一个公共代理方法
    protected abstract void Action();
    public void PerformAction()
    {
        Action();
    }

    //速度消耗方法
    public abstract float GetActionSpeedCost();

    public abstract void Attack(AbstractEntity target);

    //  获取与当前碰撞体接触的所有AbstractEntity
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

    //  获取与当前碰撞体接触的AbstractCreature
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
        // 获取当前碰撞体
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