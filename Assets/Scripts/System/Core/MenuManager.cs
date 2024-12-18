using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//MenuManager是仅在主界面中进行管理的管理类，负责中转调用以及管理游戏进程
//主界面中的其他类如果要对游戏进行实际更改，需要通过MenuManager进行调用
public class MenuManager : MonoBehaviour
{
    //大多数Manager类会有一个静态的instance变量，通过该变量可以实现单例模式
    //使用单例模式可以免去一些调用上的麻烦，但过多使用单例模式也会导致结构上的混乱
    //因此，在一些本次项目中，由我编写的结构里，尽管有部分地方用到单例模式，但整体还是呈现部分-整体的关系
    public static MenuManager instance;

    public GameObject loadingMask;

    void Awake()
    {
        //为了防止意外情况下出现多个实例，会在Monoehaviour的Awake函数处设置检测instance的条件语句
        if (instance == null)
            instance = this;
        if (instance != null && instance != this)
            Destroy(gameObject);

        loadingMask.GetComponent<Animator>().SetBool("Disappear", true);
        loadingMask.GetComponent<Animator>().Play("Disappear");
    }

    IEnumerator ShowLoadingMask()
    {
        loadingMask.GetComponent<Animator>().SetBool("Disappear", false);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator HideLoadingMask()
    {
        yield return new WaitForSeconds(1f);
        loadingMask.GetComponent<Animator>().SetBool("Disappear", true);
    }

    public void StartGame()
    {
        StartCoroutine("ShowLoadingMask");
        StartCoroutine("WaitToStartGame");
    }

    IEnumerator WaitToStartGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.instance.switchToDungeon();
    }

    public void ExitGame()
    {
        //在最终导出的游戏中，如果执行这条代码，那么程序就会直接退出，但在编辑器中，这条代码没有效果
        Application.Quit();

#if UNITY_EDITOR
        //以宏定义划分的程序块。按照条件内容，这部分代码只会在Unity编辑器中执行
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
}
