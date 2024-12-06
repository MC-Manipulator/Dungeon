using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour, Initializable
{
    public List<GameObject> UIList;

    [Header("��ʼ���б�")]
    public List<GameObject> initializableList;

    public void Initialize()
    {
        foreach (GameObject gb in initializableList)
        {
            Initializable i = (Initializable)(FetchComponent.GetSpecificComponent<Initializable>(gb));
            i.Initialize();
        }


        GameManager.instance.GamePauseEvent += OnGamePause;
        GameManager.instance.GameResumeEvent += OnGameResume;

        GameManager.instance.SwitchToDungeonBuilding += OnSwitchToDungeonBuilding;
        GameManager.instance.SwitchToTimePassing += OnSwitchToTimePassing;
    }

    public void SwitchMesh()
    {
        if (GameManager.instance.isShowingMesh)
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
        GameManager.instance.ShowMesh();
        GameObject switchButton = FindObjectInListByName("ShowMeshButton");
        switchButton.GetComponentInChildren<TMP_Text>().text = "Hide Mesh";
    }

    public void HideMesh()
    {
        GameManager.instance.HideMesh();
        GameObject switchButton = FindObjectInListByName("ShowMeshButton");
        switchButton.GetComponentInChildren<TMP_Text>().text = "Show Mesh";
    }

    public void SwitchGameStage()
    {
        GameManager.instance.SwitchGameStage();
    }

    public void ExitToMenu()
    {
        GameManager.instance.ExitToMenu();
    }

    public void ExitToDesktop()
    {
        GameManager.instance.ExitToDesktop();
    }

    public void ShowDebugBoard()
    {
        ShowUI("DebugBoard");
    }

    public void CloseDebugBoard()
    {
        CloseUI("DebugBoard");
    }

    public void PauseGame()
    {
        if (!GameManager.instance.isPaused)
            GameManager.instance.Pause();
        ShowUI("PauseMenu");
    }

    public void ResumeGame()
    {
        if (GameManager.instance.isPaused)
            GameManager.instance.Resume();
        CloseUI("PauseMenu");
    }

    public void ShowUI(string name)
    {
        GameObject target = FindObjectInListByName(name);

        target.SetActive(true);
        target.GetComponent<RectTransform>().anchoredPosition = new Vector2(0 , 0);
    }

    public void CloseUI(string name)
    {
        GameObject target = FindObjectInListByName(name);

        target.SetActive(false);
    }

    private GameObject FindObjectInListByName(string name)
    {
        GameObject target = null;

        foreach (GameObject gb in UIList)
        {
            if (gb.name == name)
            {
                target = gb;
                break;
            }
        }

        return target;
    }

    public void OnGamePause()
    {
        PauseGame();
    }

    public void OnGameResume()
    {
        ResumeGame();
    }

    public void OnSwitchToDungeonBuilding()
    {

        GameObject switchButton = FindObjectInListByName("SwitchStageButton");
        switchButton.GetComponentInChildren<TMP_Text>().text = "Switch To Time Passing";
    }

    public void OnSwitchToTimePassing()
    {

        GameObject switchButton = FindObjectInListByName("SwitchStageButton");
        switchButton.GetComponentInChildren<TMP_Text>().text = "Switch To Dungeon Building";
    }
}
