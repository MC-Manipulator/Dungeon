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
        // 锟斤拷锟斤拷锟剿猴拷值
        float damageValue = Damage();
        if (damageValue > 0)
            target.Hurt(damageValue);  // 锟剿猴拷目锟斤拷
    }

    protected Collider[] GetCollidingColliders()
    {
        // 使用Physics.OverlapBox来获取与当前碰撞体接触的所有碰撞体
        // 注意：这个例子使用的是BoxCollider，如果你使用的是其他类型的碰撞体，你可能需要调整这个方法
        if (_collider == null)
        {
            Debug.LogError("没有找到BoxCollider组件，请确保你的游戏对象上挂载了BoxCollider");
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
            Debug.LogError("没有找到碰撞体组件！");
        }
    }
}

public enum EntityType
{
    Creature,
    Trap
}