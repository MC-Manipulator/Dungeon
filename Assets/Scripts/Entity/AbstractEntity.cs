using UnityEngine;

public abstract class AbstractEntity : MonoBehaviour
{
    public EntityType entityType;   // ʵ������
    public float currentHealth;     // ��ǰ����ֵ
    public float maxHealth;         // ��󽡿�ֵ
    public float attack;            // ������
    public float defence;           // ������
    public float speed = 5;         // ��ǰ�ٶ�
    private float initialSpeed;     // ��ʼ�ٶ�
    public Vector2 mapPosition;     // ��ͼ����
    public float attackRange;       // ������Χ

    protected abstract float Damage();
    protected abstract void Die();
    public abstract float Hurt(float damage);
    protected abstract void Action();
    public void PerformAction()
    {
        Debug.Log($"{name} ����ִ���ж�...");
        Action();
    }

    public abstract float GetActionSpeedCost();

    public abstract void Attack(AbstractEntity target);

    private void Awake()
    {
        // �� Awake �м�¼��ʼ�ٶ�
        initialSpeed = speed;
    }
    public void ResetSpeed()
    {
        speed = initialSpeed;
        Debug.Log($"{name} ���ٶ�����Ϊ��ʼֵ: {initialSpeed}");
    }

    public enum EntityType
    {
        Explorer, // ̽����
        Monster,  // ����
        Trap      // ����
    }

}
