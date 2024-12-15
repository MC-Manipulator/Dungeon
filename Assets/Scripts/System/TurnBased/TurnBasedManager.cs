using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnBasedManager : MonoBehaviour
{
    private List<AbstractEntity> actionQueue = new(); // ��������
    void Start()
    {
        // �ӳ����л�ȡ���� TestExplorer �� TestMonster ʵ��
        List<AbstractEntity> entities = new()
        {
            GameObject.Find("Explorer1").GetComponent<AbstractEntity>(),
            GameObject.Find("Explorer2").GetComponent<AbstractEntity>(),
            GameObject.Find("Monster1").GetComponent<AbstractEntity>(),
            //GameObject.Find("Monster2").GetComponent<AbstractEntity>()
        };

        // ��ʼ���غ϶���
        Initialize(entities);

        // �����غ�ѭ��
        StartTurnCycle();
    }
    /// <summary>
    /// ��ʼ���غ϶���
    /// </summary>
    /// <param name="entities">���в����ʵ��</param>
    public void Initialize(List<AbstractEntity> entities)
    {
        // ��ʼ�����У����ٶ����򣨽���������ֵ���� 0 ��ʵ�壩
        actionQueue = entities.Where(e => e.currentHealth > 0)
                              .OrderByDescending(e => e.speed)
                              .ToList();

        Debug.Log("�غϳ�ʼ����ɣ�");
        foreach (var entity in actionQueue)
        {
            Debug.Log($"{entity.name} | ����: {entity.entityType} | �ٶ�: {entity.speed}");
        }
    }

    /// <summary>
    /// ��ʼ�غ�ѭ��
    /// </summary>
    public void StartTurnCycle()
    {
        if (actionQueue.Count == 0)
        {
            Debug.LogWarning("�غ϶���Ϊ�գ��޷���ʼ�غ������̣�");
            return;
        }

        StartCoroutine(TurnCycle());
    }

    /// <summary>
    /// �غ�ѭ��Э��
    /// </summary>
    private IEnumerator TurnCycle()
    {
        Debug.Log("��ʼ�غ�������");

        while (actionQueue.Count > 0)
        {
            // ��ȡ��ǰ�����еĵ�һ��ʵ�壨�ٶ���ߣ�
            AbstractEntity currentEntity = actionQueue[0];

            Debug.Log($"��ǰ�ж���: {currentEntity.name} | ����: {currentEntity.entityType} | ��ǰ�ٶ�: {currentEntity.speed} | ��ǰ����: {currentEntity.currentHealth}");

            // ִ��ʵ����ж�
            currentEntity.PerformAction();

            // �����ٶ�����
            currentEntity.speed -= currentEntity.GetActionSpeedCost();

            // ����ٶ��Ƿ�С�ڵ��� 0������������Ϊ��ʼ�ٶ�
            if (currentEntity.speed <= 0)
            {
                currentEntity.ResetSpeed();
            }

            if (currentEntity.currentHealth <= 0)
            {
                Debug.Log($"{currentEntity.name} ���������Ƴ�����");
                actionQueue.RemoveAt(0); // ֱ���Ƴ�����Ԫ��
            }
            else
            {
                // ���δ��������ʵ�����²�����У������ٶ�����
                actionQueue = actionQueue.Where(e => e.currentHealth > 0)
                                         .OrderByDescending(e => e.speed)
                                         .ToList();
            }

            yield return new WaitForSeconds(0.5f); // �ȴ� 0.5 �룬ģ��غϼ��
        }

        Debug.Log("����ʵ���ж���ɣ�");
    }
}
