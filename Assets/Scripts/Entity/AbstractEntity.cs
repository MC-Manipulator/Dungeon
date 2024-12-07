using System.Collections;
using System.Collections.Generic;
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

    protected abstract float Damage();

    protected abstract void Die();

    public abstract float Hurt(float damage);

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