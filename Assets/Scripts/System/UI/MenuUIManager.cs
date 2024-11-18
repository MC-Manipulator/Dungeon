using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MenuUIManager是仅在主界面中进行UI管理的管理类，负责中转调用以及显示与隐藏UI
//具体的UI组件与物体作为触发器，在玩家触发之后需要通过MenuUIManager的方法再调用有实际功能的方法
public class MenuUIManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("开始游戏");
        MenuManager.instance.StartGame();
    }

    public void OpenSettingUI()
    {
        Debug.Log("打开设置界面");
    }

    public void ExitGame()
    {
        Debug.Log("退出游戏");
        MenuManager.instance.ExitGame();
    }
}
