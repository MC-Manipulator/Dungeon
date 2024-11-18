using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//��ע�⣬�˴���SceneManager��Unity�Դ���SceneManager����ͬһ����
//�˴�ֻ��Ϊ�˼򻯵��ö���д���м������
public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        if (instance != null && instance != this)
            gameObject.SetActive(false);
    }

    public void switchToDungeon()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void switchToMenu()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
