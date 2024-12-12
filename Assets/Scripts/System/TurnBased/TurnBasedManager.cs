using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnBasedManager : MonoBehaviour
{
    private List<AbstractEntity> actionQueue = new();

    public void Initialize(List<AbstractEntity> entities)
    {
        // ��ʼ�����У����ٶ�����
        actionQueue = entities.Where(e => e.currentHealth > 0)
                              .OrderByDescending(e => e.speed)
                              .ToList();
    }

    public void StartTurnCycle()
    {
        StartCoroutine(TurnCycle()); //�����غ�ѭ����Э�̣�
    }

    private IEnumerator TurnCycle()
    {
        while (actionQueue.Count > 0)
        {
            AbstractEntity currentEntity = actionQueue[0]; //��ȡ��ǰ�����еĵ�һ��ʵ�壨�ٶ�ֵ��ߣ�

            // ��ǰʵ��ִ���ж�
            currentEntity.PerformAction();

            // �����ٶ����Ĳ����¶��У�����Ĭ������5f�㣩
            currentEntity.speed -= currentEntity.GetActionSpeedCost();
            if (currentEntity.currentHealth <= 0)
            {
                // �Ӷ������Ƴ�����ʵ��
                actionQueue.Remove(currentEntity);
            }

            // �����������
            actionQueue = actionQueue.Where(e => e.currentHealth > 0)
                                     .OrderByDescending(e => e.speed)
                                     .ToList();

            // �ȴ�һ֡��ģ��غϼ�϶��
            yield return null;
        }
    }
}

