using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

///<summary>
///Initializable�ӿڱ������κο�����Ҫ���г�ʼ�������Ҷ��ڳ�ʼ�����Ⱥ�˳��Ҫ��Ķ���<br/>
///�ڽű������չ�˸ýӿں���Ҫ�����иýӿڵ���Ϸ�������GameManager��initializableList�б�<br/>
///�����������ط���������Initialize������
///</summary>
public interface Initializable
{
    public abstract void Initialize();
}
