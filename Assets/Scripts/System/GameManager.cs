using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
<<<<<<< HEAD:Assets/Scripts/Entity/Creature/Monster/TestMonster.cs
public class TestMonster : AbstractMonster
=======
public class GameManager : MonoBehaviour
>>>>>>> parent of d6f89e6 (Merge branch 'main' into JianyongHuang):Assets/Scripts/System/GameManager.cs
{
    public new CreatureType creatureType = CreatureType.Monster;

    // 实现抽象方法 GetActionSpeedCost
    public override float GetActionSpeedCost()
    {
        // 返回该怪物行动所需的速度成本，可以根据需要调整
        return 5; // 示例值
    }
    // 确保访问修饰符为 protected
    protected override void Action()
    {
        Debug.Log($"{name} 正在进行怪物的行动。");

        // 示例：攻击最近的探险者
        AbstractCreature[] explorers = GetCollidingCreature(CreatureType.Explorer);
        if (explorers.Length > 0)
        {
            Debug.Log($"{name} 攻击了 {explorers[0].name}！");
        }
        else
        {
            Debug.Log($"{name} 没有找到探险者，正在巡逻。");
        }
=======
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
>>>>>>> parent of d6f89e6 (Merge branch 'main' into JianyongHuang)
    }
}
