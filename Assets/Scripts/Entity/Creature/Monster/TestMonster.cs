using UnityEngine;

public class TestMonster : AbstractEntity
{
    private void Start()
    {
        entityType = EntityType.Monster; // 设置实体类型为怪物
        currentHealth = 120;
        maxHealth = 120;
        attack = 20;
        defence = 8;
        // speed 可通过 Unity 编辑器调整
    }

    protected override float Damage()
    {
        return attack; // 返回怪物的攻击力
    }

    protected override void Die()
    {
        Debug.Log($"{name}（怪物）死亡！");
    }

    public override float Hurt(float damage)
    {
        float actualDamage = Mathf.Max(damage - defence, 0); // 防御减免伤害
        currentHealth -= actualDamage;
        Debug.Log($"{name}（怪物）受到 {actualDamage} 点伤害，剩余生命值：{currentHealth}");
        if (currentHealth <= 0) Die();
        return actualDamage;
    }

    protected override void Action()
    {
        Debug.Log($"{name}（怪物）正在准备攻击...");
    }

    public override float GetActionSpeedCost()
    {
        return 1.5f; // 怪物行动速度消耗
    }

    public override void Attack(AbstractEntity target)
    {
        Debug.Log($"{name}（怪物）攻击了 {target.name}！");
        target.Hurt(Damage());
    }
}
