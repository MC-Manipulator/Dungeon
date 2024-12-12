using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractMonster : AbstractCreature
{
    // 确保字段只定义一次
    public Vector2 homePosition;



    protected override void Action()
    {
        if (currentSan == 0)    //若san值为零，则重置san值并跳过回合
        {
            currentSan = maxSan;
            return;
        }
        AbstractCreature[] explorers = GetCollidingCreature(CreatureType.Explorer);
        if (explorers.Length > 0)   //若检测到探险者，则进行攻击
        {
            //计算根据攻击距离可以到探险者的坐标
            Vector2 normal = (mapPosition - explorers[0].mapPosition).normalized;
            Vector2 targetPos = attackRange * normal + explorers[0].mapPosition;

            //计算移动后的新坐标
            Vector2 newPos = Move(targetPos);
            mapPosition = newPos;

            //判断是否攻击
            if (Vector2.Distance(newPos, explorers[0].mapPosition) <= attackRange)
                Attack(explorers[0]);
        }
        else  //否则返回家
        {
            Vector2 newPos = Move(homePosition);
            mapPosition = newPos;
        }
    }
}