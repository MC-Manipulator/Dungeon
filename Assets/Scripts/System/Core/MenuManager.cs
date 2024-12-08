using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//MenuManager�ǽ����������н��й����Ĺ����࣬������ת�����Լ�������Ϸ����
//�������е����������Ҫ����Ϸ����ʵ�ʸ��ģ���Ҫͨ��MenuManager���е���
public class MenuManager : MonoBehaviour
{
    //�����Manager�����һ����̬��instance������ͨ���ñ�������ʵ�ֵ���ģʽ
    //ʹ�õ���ģʽ������ȥһЩ�����ϵ��鷳��������ʹ�õ���ģʽҲ�ᵼ�½ṹ�ϵĻ���
    //��ˣ���һЩ������Ŀ�У����ұ�д�Ľṹ������в��ֵط��õ�����ģʽ�������廹�ǳ��ֲ���-����Ĺ�ϵ
    public static MenuManager instance; 

    void Awake()
    {
        //Ϊ�˷�ֹ��������³��ֶ��ʵ��������Monoehaviour��Awake���������ü��instance���������
        if (instance == null)
            instance = this;
        if (instance != null && instance != this)
            Destroy(gameObject);
    }

    public void StartGame()
    {
        SceneManager.instance.switchToDungeon();
    }

    public void ExitGame()
    {
        //�����յ�������Ϸ�У����ִ���������룬��ô����ͻ�ֱ���˳������ڱ༭���У���������û��Ч��
        Application.Quit();

#if UNITY_EDITOR
        //�Ժ궨�廮�ֵĳ���顣�����������ݣ��ⲿ�ִ���ֻ����Unity�༭����ִ��
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
}
