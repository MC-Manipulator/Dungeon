using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour, Initializable
{
    private bool _isPaused;

    public float scrollInput;

    public Vector2 inputDirection;

    public event Action ZoomEvent;

    public event Action MoveEvent;

    public event Action PauseEvent;

    public event Action ResumeEvent;

    public event Action MonsterBoardEvent;

    public event Action PlaceEvent;

    public event Action CancelEvent;

    public event Action BuildingBoardEvent;

    public static PlayerInputManager instance;

    private bool hasInitialized = false;

    public void Initialize()
    {
        if (instance == null)
            instance = this;
        if (instance != null && instance != this)
            gameObject.SetActive(false);

        _isPaused = false;

        GameManager.instance.GamePauseEvent += OnGamePause;
        GameManager.instance.GameResumeEvent += OnGameResume;

        hasInitialized = true;
    }

    void Update()
    {
        if (!hasInitialized)
            return;


        // ºÏ≤‚ Û±Íπˆ¬÷ ‰»Î
        scrollInput = Input.GetAxis("Mouse ScrollWheel");
        // ºÏ≤‚∑ΩœÚ ‰»Î
        inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        if (!_isPaused)
        {

            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Escape))
            {
                if (DungeonManager.instance.isBuilding)
                {
                    CancelEvent?.Invoke();
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (DungeonManager.instance.isBuilding)
                {
                    PlaceEvent?.Invoke();
                    return;
                }
            }

            if (scrollInput != 0)
            {
                ZoomEvent?.Invoke();
            }

            if (inputDirection.x != 0 || inputDirection.y != 0)
            {
                MoveEvent?.Invoke();
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseEvent?.Invoke();
            }


            if (Input.GetKeyDown(KeyCode.B))
            {
                BuildingBoardEvent?.Invoke();
            }


            if (Input.GetKeyDown(KeyCode.M))
            {
                MonsterBoardEvent?.Invoke();
            }

        }
        else if (_isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeEvent?.Invoke();
            }
        }
    }

    public void OnGamePause()
    {
        _isPaused = true;
    }

    public void OnGameResume()
    {
        _isPaused = false;
    }
}