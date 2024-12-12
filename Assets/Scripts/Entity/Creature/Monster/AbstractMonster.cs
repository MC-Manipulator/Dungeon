using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractMonster : AbstractCreature
{
    // ȷ���ֶ�ֻ����һ��
    public Vector2 homePosition;



    protected override void Action()
    {
        if (currentSan == 0)    //��sanֵΪ�㣬������sanֵ�������غ�
        {
            currentSan = maxSan;
            return;
        }
        AbstractCreature[] explorers = GetCollidingCreature(CreatureType.Explorer);
        if (explorers.Length > 0)   //����⵽̽���ߣ�����й���
        {
            //������ݹ���������Ե�̽���ߵ�����
            Vector2 normal = (mapPosition - explorers[0].mapPosition).normalized;
            Vector2 targetPos = attackRange * normal + explorers[0].mapPosition;

            //�����ƶ����������
            Vector2 newPos = Move(targetPos);
            mapPosition = newPos;

            //�ж��Ƿ񹥻�
            if (Vector2.Distance(newPos, explorers[0].mapPosition) <= attackRange)
                Attack(explorers[0]);
        }
        else  //���򷵻ؼ�
        {
            Vector2 newPos = Move(homePosition);
            mapPosition = newPos;
        }
    }
}