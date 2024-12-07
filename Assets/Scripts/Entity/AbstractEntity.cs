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
        // �����˺�ֵ
        float damageValue = Damage();
        if (damageValue > 0)
            target.Hurt(damageValue);  // �˺�Ŀ��
    }
}

public enum EntityType
{
    Creature,
    Trap
}