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
        //չʾЧ������ɾ��
        Debug.Log("�غϳ�ʼ����ɣ�");
        foreach (var entity in actionQueue)
        {
            Debug.Log($"{entity.name} | ����: {entity.entityType} | �ٶ�: {entity.speed}");
        }
    }

    public void StartTurnCycle()
    {
        StartCoroutine(TurnCycle()); //�����غ�ѭ����Э�̣�
    }

    private IEnumerator TurnCycle()
    {
        Debug.Log("��ʼ�غ�������");
        while (actionQueue.Count > 0)
        {
            AbstractEntity currentEntity = actionQueue[0]; //��ȡ��ǰ�����еĵ�һ��ʵ�壨�ٶ�ֵ��ߣ�

            // ��ǰʵ��ִ���ж�
            Debug.Log($"��ǰ�ж���: {currentEntity.name} | ����: {currentEntity.entityType} | ʣ���ٶ�: {currentEntity.speed}");
            currentEntity.PerformAction();

            // �����ٶ����Ĳ����¶���
            currentEntity.speed -= currentEntity.GetActionSpeedCost();
            if (currentEntity.currentHealth <= 0)
            {
                // �Ӷ������Ƴ�����ʵ��
                Debug.Log($"{currentEntity.name} ���������Ƴ�����");
                actionQueue.Remove(currentEntity);
            }

            // �����������
            actionQueue = actionQueue.Where(e => e.currentHealth > 0)
                                     .OrderByDescending(e => e.speed)
                                     .ToList();

            // �ȴ�һ֡��ģ��غϼ�϶��
            yield return null;
            Debug.Log("�غ������̽���");
        }
    }
}


