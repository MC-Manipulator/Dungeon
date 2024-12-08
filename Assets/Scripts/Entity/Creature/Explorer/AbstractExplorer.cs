using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractExplorer : AbstractCreature
{
    public Vector2 target;
    public new CreatureType creatureType = CreatureType.Explorer;

    protected override void Action()
    {
        if (currentSan == 0)    //若san值为零，则重置san值并跳过回合
        {
            currentSan = maxSan;
            return;
        }
        AbstractCreature[] monsters = GetCollidingCreature(CreatureType.Monster);
        if (monsters.Length > 0)   //若检测到怪物，则攻击怪物
        {
            //计算攻击方向
            Vector2 normal = (mapPosition - monsters[0].mapPosition).normalized;
            Vector2 targetPos = attackRange * normal + monsters[0].mapPosition;

            //计算移动后的新坐标
            Vector2 newPos = Move(targetPos);
            mapPosition = newPos;

            //若到达攻击距离，则攻击怪物
            if (Vector2.Distance(newPos, monsters[0].mapPosition) <= attackRange)
                Attack(monsters[0]);
        }
        else  //若未检测到怪物，则移动到目标位置
        {
            mapPosition = Move(target);
        }
    }
}