using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MenuUIManager�ǽ����������н���UI����Ĺ����࣬������ת�����Լ���ʾ������UI
//�����UI�����������Ϊ������������Ҵ���֮����Ҫͨ��MenuUIManager�ķ����ٵ�����ʵ�ʹ��ܵķ���
public class MenuUIManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("��ʼ��Ϸ");
        MenuManager.instance.StartGame();
    }

    public void OpenSettingUI()
    {
        Debug.Log("�����ý���");
    }

    public void ExitGame()
    {
        Debug.Log("�˳���Ϸ");
        MenuManager.instance.ExitGame();
    }
}
