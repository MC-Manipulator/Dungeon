using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractExplorer : AbstractCreature
{
    public Vector2 target;
    public new CreatureType creatureType = CreatureType.Explorer;

    protected override void Action()
    {
        if (currentSan == 0)    //��sanֵΪ�㣬������sanֵ�������غ�
        {
            currentSan = maxSan;
            return;
        }
        AbstractCreature[] monsters = GetCollidingCreature(CreatureType.Monster);
        if (monsters.Length > 0)   //����⵽����򹥻�����
        {
            //���㹥������
            Vector2 normal = (mapPosition - monsters[0].mapPosition).normalized;
            Vector2 targetPos = attackRange * normal + monsters[0].mapPosition;

            //�����ƶ����������
            Vector2 newPos = Move(targetPos);
            mapPosition = newPos;

            //�����﹥�����룬�򹥻�����
            if (Vector2.Distance(newPos, monsters[0].mapPosition) <= attackRange)
                Attack(monsters[0]);
        }
        else  //��δ��⵽������ƶ���Ŀ��λ��
        {
            mapPosition = Move(target);
        }
    }
}