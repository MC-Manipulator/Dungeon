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

    [Header("չʾ����")]
    public bool isShowingMesh;

    [Header("���ؽ���")]
    public GameObject loadingMask;

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

        Vector2 zoneCenter = ZoneManager.instance.currentZone.GetComponent<Zone>().zoneCenter;

        CameraManager.instance.DirectMove(zoneCenter);

        isShowingMesh = false;
    }

    public void Initialize()
    {
        isPaused = false;
        gameStage = GameStage.DungeonBuilding;
        PlayerInputManager.instance.PauseEvent += Pause;
        PlayerInputManager.instance.ResumeEvent += Resume;
        StartCoroutine("HideLoadingMask");
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


    public void SwitchMesh()
    {
        if (isShowingMesh)
        {
            HideMesh();
        }
        else
        {
            ShowMesh();
        }
    }

    public void ShowMesh()
    {
        isShowingMesh = true;
        EmptyBlock[] blocks = ZoneManager.instance.currentZone.GetComponent<Zone>().map.GetComponentsInChildren<EmptyBlock>();
        foreach (EmptyBlock block in blocks)
        {
            block.ShowLine();
        }
    }

    public void HideMesh()
    {

        isShowingMesh = false;
        EmptyBlock[] blocks = ZoneManager.instance.currentZone.GetComponent<Zone>().map.GetComponentsInChildren<EmptyBlock>();
        foreach (EmptyBlock block in blocks)
        {
            block.HideLine();
        }
    }

    public void ExitToMenu()
    {
        StartCoroutine("ShowLoadingMask");
        StartCoroutine("WaitToExitToMenu");
    }

    IEnumerator WaitToExitToMenu()
    {
        yield return new WaitForSeconds(1f);
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