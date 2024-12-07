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

    protected abstract void Hurt(float damage);

    public void Attack(AbstractEntity target)
    {
        // 计算伤害值
        float damageValue = Damage();
        if (damageValue > 0)
            target.Hurt(damageValue);  // 伤害目标
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