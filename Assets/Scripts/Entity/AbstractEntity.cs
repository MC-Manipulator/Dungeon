using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class AbstractEntity : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public float attack;
    public float defence;
    public float speed;
    public Vector2 mapPosition;

    protected abstract void Damage();
    protected abstract void Hurt();
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
}
