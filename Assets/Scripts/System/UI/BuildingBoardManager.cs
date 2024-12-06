using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BuildingBoardManager : MonoBehaviour, Initializable
{
    public GameObject board;


    private bool _boardState = false;

    public void Initialize()
    {
        if (board == null)
        {
            board = GameObject.Find("BuildingBoard");
        }

        PlayerInputManager.instance.BuildingBoardEvent += OnSwitchingBuildingBoard;
        DungeonManager.instance.StartPlacingEvent += OnPlacingStart;
        DungeonManager.instance.EndPlacingEvent += OnPlacingEnd;


    }

    private void OnSwitchingBuildingBoard()
    {
        if (_boardState)
        {
            HideBoard();
        }
        else
        {
            ShowBoard();
        }
    }

    public void BuildRoom()
    {
        DungeonManager.instance.Build("Room");
    }


    private bool _preOpening = false; 

    private void OnPlacingStart()
    {
        _preOpening = _boardState;
        if (_preOpening)
        {
            HideBoard();
        }
    }

    private void OnPlacingEnd()
    {
        if (_preOpening)
        {
            ShowBoard();
        }
    }

    public void ShowBoard()
    {
        _boardState = true;
        board.SetActive(true);
        board.GetComponent<RectTransform>().anchoredPosition = new Vector2(725, 50);

    }

    public void HideBoard()
    {
        _boardState = false;
        board.SetActive(false);
    }
}
