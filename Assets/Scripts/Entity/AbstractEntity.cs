using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

    protected abstract void Hurt(float damage);

    public void Attack(AbstractEntity target)
    {
        // �����˺�ֵ
        float damageValue = Damage();
        if (damageValue > 0)
            target.Hurt(damageValue);  // �˺�Ŀ��
    }

    protected Collider[] GetCollidingColliders()
    {
        // ʹ��Physics.OverlapBox����ȡ�뵱ǰ��ײ��Ӵ���������ײ��
        // ע�⣺�������ʹ�õ���BoxCollider�������ʹ�õ����������͵���ײ�壬�������Ҫ�����������
        if (_collider == null)
        {
            Debug.LogError("û���ҵ�BoxCollider�������ȷ�������Ϸ�����Ϲ�����BoxCollider");
            return new Collider[0];
        }

        Collider[] colliders = Physics.OverlapBox(_collider.bounds.center, _collider.bounds.extents);

        return colliders;
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