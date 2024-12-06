using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBoardManager : MonoBehaviour, Initializable
{
    GameObject board;

    public void Initialize()
    {
        if (board == null)
        {
            board = GameObject.Find("MonsterBoard");
        }
    }

    public void ShowBoard()
    {
        board.SetActive(true);
        board.GetComponent<RectTransform>().anchoredPosition = new Vector2(725, 50);

    }

    public void HideBoard()
    {
        board.SetActive(false);
    }
}
