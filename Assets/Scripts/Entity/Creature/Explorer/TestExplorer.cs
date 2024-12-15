using UnityEngine;

public class TestExplorer : AbstractEntity
{
    private void Start()
    {
        entityType = EntityType.Explorer; // 设置实体类型为探险者
        currentHealth = 100;
        maxHealth = 100;
        attack = 15;
        defence = 5;
        // speed 可通过 Unity 编辑器调整
    }

    protected override float Damage()
    {
        return attack; // 返回探险者的攻击力
    }

    protected override void Die()
    {
        Debug.Log($"{name}（探险者）死亡！");
    }

    public override float Hurt(float damage)
    {
        float actualDamage = Mathf.Max(damage - defence, 0); // 防御减免伤害
        currentHealth -= actualDamage;
        Debug.Log($"{name}（探险者）受到 {actualDamage} 点伤害，剩余生命值：{currentHealth}");
        if (currentHealth <= 0) Die();
        return actualDamage;
    }

    protected override void Action()
    {
        Debug.Log($"{name}（探险者）正在探索...");
    }

    public override float GetActionSpeedCost()
    {
        return 1.2f; // 探险者行动速度消耗
    }

    public override void Attack(AbstractEntity target)
    {
        Debug.Log($"{name}（探险者）攻击了 {target.name}！");
        target.Hurt(Damage());
    }
}
