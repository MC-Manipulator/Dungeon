using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour, Initializable
{
    public List<GameObject> UIList;

    public void Initialize()
    {
        GameManager.instance.GamePauseEvent += OnGamePause;
        GameManager.instance.GameResumeEvent += OnGameResume;

        GameManager.instance.SwitchToDungeonBuilding += OnSwitchToDungeonBuilding;
        GameManager.instance.SwitchToTimePassing += OnSwitchToTimePassing;
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

        GameObject switchButton = FindObjectInListByName("SwitchButton");
        switchButton.GetComponentInChildren<TMP_Text>().text = "Switch To Time Passing";
    }

    public void OnSwitchToTimePassing()
    {

        GameObject switchButton = FindObjectInListByName("SwitchButton");
        switchButton.GetComponentInChildren<TMP_Text>().text = "Switch To Dungeon Building";
    }
}
