using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
<<<<<<< HEAD:Assets/Scripts/Entity/Creature/Monster/TestMonster.cs
public class TestMonster : AbstractMonster
=======
public class GameManager : MonoBehaviour
>>>>>>> parent of d6f89e6 (Merge branch 'main' into JianyongHuang):Assets/Scripts/System/GameManager.cs
{
    public new CreatureType creatureType = CreatureType.Monster;

    // ʵ�ֳ��󷽷� GetActionSpeedCost
    public override float GetActionSpeedCost()
    {
        // ���ظù����ж�������ٶȳɱ������Ը�����Ҫ����
        return 5; // ʾ��ֵ
    }
    // ȷ���������η�Ϊ protected
    protected override void Action()
    {
        Debug.Log($"{name} ���ڽ��й�����ж���");

        // ʾ�������������̽����
        AbstractCreature[] explorers = GetCollidingCreature(CreatureType.Explorer);
        if (explorers.Length > 0)
        {
            Debug.Log($"{name} ������ {explorers[0].name}��");
        }
        else
        {
            Debug.Log($"{name} û���ҵ�̽���ߣ�����Ѳ�ߡ�");
        }
=======
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
>>>>>>> parent of d6f89e6 (Merge branch 'main' into JianyongHuang)
    }
}
