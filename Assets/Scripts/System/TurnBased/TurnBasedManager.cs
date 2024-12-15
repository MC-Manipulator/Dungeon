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
        //展示效果，可删除
        Debug.Log("回合初始化完成：");
        foreach (var entity in actionQueue)
        {
            Debug.Log($"{entity.name} | 类型: {entity.entityType} | 速度: {entity.speed}");
        }
    }

    public void StartTurnCycle()
    {
        StartCoroutine(TurnCycle()); //启动回合循环（协程）
    }

    private IEnumerator TurnCycle()
    {
        Debug.Log("开始回合制流程");
        while (actionQueue.Count > 0)
        {
            AbstractEntity currentEntity = actionQueue[0]; //获取当前队列中的第一个实体（速度值最高）

            // 当前实体执行行动
            Debug.Log($"当前行动者: {currentEntity.name} | 类型: {currentEntity.entityType} | 剩余速度: {currentEntity.speed}");
            currentEntity.PerformAction();

            // 计算速度消耗并更新队列
            currentEntity.speed -= currentEntity.GetActionSpeedCost();
            if (currentEntity.currentHealth <= 0)
            {
                // 从队列中移除死亡实体
                Debug.Log($"{currentEntity.name} 已死亡，移出队列");
                actionQueue.Remove(currentEntity);
            }

            // 重新排序队列
            actionQueue = actionQueue.Where(e => e.currentHealth > 0)
                                     .OrderByDescending(e => e.speed)
                                     .ToList();

            // 等待一帧（模拟回合间隙）
            yield return null;
            Debug.Log("回合制流程结束");
        }
    }
}


