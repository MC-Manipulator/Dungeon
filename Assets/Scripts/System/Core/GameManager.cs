using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameStage
{
    TimePassing,
    DungeonBuilding
}

///<summary>
///GameManager����Ϸ�����еĺ��Ĺ���ϵͳ��������ת�����Լ�ʵ������Ϸ������صĹ��ܡ�
///</summary>
public class GameManager : MonoBehaviour, Initializable
{
    public static GameManager instance;

    public event Action GamePauseEvent;
    public event Action GameResumeEvent;
    public event Action SwitchToTimePassing;
    public event Action SwitchToDungeonBuilding;

    [Header("��Ϸ��ǰ�׶�")]
    public GameStage gameStage;
    public bool isPaused;

    [Header("��ʼ���б�")]
    public List<GameObject> initializableList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        if (instance != null && instance != this)
            gameObject.SetActive(false);

        foreach (GameObject gb in initializableList)
        {
            Initializable i = (Initializable)(FetchComponent.GetSpecificComponent<Initializable>(gb));
            i.Initialize();
        }
    }

    public void Initialize()
    {
        isPaused = false;
        gameStage = GameStage.DungeonBuilding;
        PlayerInputManager.instance.PauseEvent += Pause;
        PlayerInputManager.instance.ResumeEvent += Resume;
    }

    public void SwitchGameStage()
    {
        if (gameStage == GameStage.DungeonBuilding)
        {
            SwitchToTimePassing?.Invoke();
            gameStage = GameStage.TimePassing;
            return;
        }

        if (gameStage == GameStage.TimePassing)
        {
            SwitchToDungeonBuilding?.Invoke();
            gameStage = GameStage.DungeonBuilding;
            return;
        }
    }

    public void ExitToMenu()
    {
        SceneManager.instance.switchToMenu();
    }

    public void ExitToDesktop()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }

    public void Pause()
    {
        Debug.Log("��Ϸ��ͣ");
        isPaused = true;
        GamePauseEvent?.Invoke();
    }

    public void Resume()
    {
        Debug.Log("��Ϸ�ָ�");
        isPaused = false;
        GameResumeEvent?.Invoke();
    }
}