using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
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
        {
            target.Hurt(damageValue);  // 伤害目标
        }
    }
}

public enum EntityType
{
    Creature,
    Trap
}