using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnBasedManager : MonoBehaviour
{
    private List<AbstractEntity> actionQueue = new(); // 动作队列
    void Start()
    {
        // 从场景中获取所有 TestExplorer 和 TestMonster 实例
        List<AbstractEntity> entities = new()
        {
            GameObject.Find("Explorer1").GetComponent<AbstractEntity>(),
            GameObject.Find("Explorer2").GetComponent<AbstractEntity>(),
            GameObject.Find("Monster1").GetComponent<AbstractEntity>(),
            //GameObject.Find("Monster2").GetComponent<AbstractEntity>()
        };

        // 初始化回合队列
        Initialize(entities);

        // 启动回合循环
        StartTurnCycle();
    }
    /// <summary>
    /// 初始化回合队列
    /// </summary>
    /// <param name="entities">所有参与的实体</param>
    public void Initialize(List<AbstractEntity> entities)
    {
        // 初始化队列，按速度排序（仅包含生命值大于 0 的实体）
        actionQueue = entities.Where(e => e.currentHealth > 0)
                              .OrderByDescending(e => e.speed)
                              .ToList();

        Debug.Log("回合初始化完成：");
        foreach (var entity in actionQueue)
        {
            Debug.Log($"{entity.name} | 类型: {entity.entityType} | 速度: {entity.speed}");
        }
    }

    /// <summary>
    /// 开始回合循环
    /// </summary>
    public void StartTurnCycle()
    {
        if (actionQueue.Count == 0)
        {
            Debug.LogWarning("回合队列为空，无法开始回合制流程！");
            return;
        }

        StartCoroutine(TurnCycle());
    }

    /// <summary>
    /// 回合循环协程
    /// </summary>
    private IEnumerator TurnCycle()
    {
        Debug.Log("开始回合制流程");

        while (actionQueue.Count > 0)
        {
            // 获取当前队列中的第一个实体（速度最高）
            AbstractEntity currentEntity = actionQueue[0];

            Debug.Log($"当前行动者: {currentEntity.name} | 类型: {currentEntity.entityType} | 当前速度: {currentEntity.speed} | 当前生命: {currentEntity.currentHealth}");

            // 执行实体的行动
            currentEntity.PerformAction();

            // 计算速度消耗
            currentEntity.speed -= currentEntity.GetActionSpeedCost();

            // 检查速度是否小于等于 0，若是则重置为初始速度
            if (currentEntity.speed <= 0)
            {
                currentEntity.ResetSpeed();
            }

            if (currentEntity.currentHealth <= 0)
            {
                Debug.Log($"{currentEntity.name} 已死亡，移出队列");
                actionQueue.RemoveAt(0); // 直接移除队首元素
            }
            else
            {
                // 如果未死亡，将实体重新插入队列（依据速度排序）
                actionQueue = actionQueue.Where(e => e.currentHealth > 0)
                                         .OrderByDescending(e => e.speed)
                                         .ToList();
            }

            yield return new WaitForSeconds(0.5f); // 等待 0.5 秒，模拟回合间隔
        }

        Debug.Log("所有实体行动完成！");
    }
}
