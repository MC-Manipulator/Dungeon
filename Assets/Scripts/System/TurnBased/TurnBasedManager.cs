using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnBasedManager : MonoBehaviour
{
    private List<AbstractEntity> actionQueue = new();

    public void Initialize(List<AbstractEntity> entities)
    {
        // 初始化队列，按速度排序
        actionQueue = entities.Where(e => e.currentHealth > 0)
                              .OrderByDescending(e => e.speed)
                              .ToList();
    }

    public void StartTurnCycle()
    {
        StartCoroutine(TurnCycle()); //启动回合循环（协程）
    }

    private IEnumerator TurnCycle()
    {
        while (actionQueue.Count > 0)
        {
            AbstractEntity currentEntity = actionQueue[0]; //获取当前队列中的第一个实体（速度值最高）

            // 当前实体执行行动
            currentEntity.PerformAction();

            // 计算速度消耗并更新队列（现在默认消耗5f点）
            currentEntity.speed -= currentEntity.GetActionSpeedCost();
            if (currentEntity.currentHealth <= 0)
            {
                // 从队列中移除死亡实体
                actionQueue.Remove(currentEntity);
            }

            // 重新排序队列
            actionQueue = actionQueue.Where(e => e.currentHealth > 0)
                                     .OrderByDescending(e => e.speed)
                                     .ToList();

            // 等待一帧（模拟回合间隙）
            yield return null;
        }
    }
}

