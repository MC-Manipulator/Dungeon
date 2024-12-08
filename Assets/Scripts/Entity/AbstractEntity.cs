using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class AbstractEntity : MonoBehaviour
{
    private Collider _collider;     // µ±Ç°Åö×²Ìå
    public EntityType entityType;   // ÊµÌåÀàÐÍ
    public float currentHealth;     // µ±Ç°½¡¿µÖµ
    public float maxHealth;         // ×î´ó½¡¿µÖµ
    public float attack;            // ¹¥»÷Á¦
    public float defence;           // ·ÀÓùÁ¦
    public float speed;             // ËÙ¶È
    public Vector2 mapPosition;     // µØÍ¼×ø±ê
    public float attackRange;       // ¹¥»÷·¶Î§

    protected abstract float Damage();

    protected abstract void Die();

    private void Awake()
    {
        PreAwake();
        MidAwake();
        PostAwake();
    }

    private void Update()
    {
        PreUpdate();
        MidUpdate();
        PostUpdate();
    }

    protected abstract void PreAwake();
    protected abstract void MidAwake();
    protected abstract void PostAwake();

    protected abstract void PreUpdate();
    protected abstract void MidUpdate();
    protected abstract void PostUpdate();

    public abstract float Hurt(float damage);

    protected abstract void Action();

    public abstract void Attack(AbstractEntity target);

    //  »ñÈ¡Óëµ±Ç°Åö×²Ìå½Ó´¥µÄËùÓÐAbstractEntity
    protected AbstractEntity[] GetCollidingColliders()
    {
        // Ê¹ï¿½ï¿½Physics.OverlapBoxï¿½ï¿½ï¿½ï¿½È¡ï¿½ëµ±Ç°ï¿½ï¿½×²ï¿½ï¿½Ó´ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½×²ï¿½ï¿½
        // ×¢ï¿½â£ºï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ê¹ï¿½Ãµï¿½ï¿½ï¿½BoxColliderï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ê¹ï¿½Ãµï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Íµï¿½ï¿½ï¿½×²ï¿½å£¬ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Òªï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½
        if (_collider == null)
        {
            Debug.LogError("Ã»ï¿½ï¿½ï¿½Òµï¿½BoxColliderï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½È·ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ï·ï¿½ï¿½ï¿½ï¿½ï¿½Ï¹ï¿½ï¿½ï¿½ï¿½ï¿½BoxCollider");
            return new AbstractEntity[0];
        }

        Collider[] colliders = Physics.OverlapBox(_collider.bounds.center, _collider.bounds.extents);

        AbstractEntity[] entities = new AbstractEntity[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
            entities[i] = colliders[i].GetComponent<AbstractEntity>();

        return entities;
    }

    //  »ñÈ¡Óëµ±Ç°Åö×²Ìå½Ó´¥µÄAbstractCreature
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
        // »ñÈ¡µ±Ç°Åö×²Ìå
        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.LogError("Ã»ï¿½ï¿½ï¿½Òµï¿½ï¿½ï¿½×²ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½");
        }
    }
}

public enum EntityType
{
    Creature,
    Trap
}