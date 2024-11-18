using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//请注意，此处的SceneManager与Unity自带的SceneManager不是同一个，
//此处只是为了简化调用而编写的中间管理类
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
