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

    public static PlayerInputManager instance;

    private bool hasInitialized = false;

    public void Initialize()
    {
        if (instance == null)
            instance = this;
        if (instance != null && instance != this)
            gameObject.SetActive(false);

        _isPaused = false;

        GameManager.instance.GamePauseEvent += Pause;
        GameManager.instance.GameResumeEvent += Resume;

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
        }
        else if (_isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeEvent?.Invoke();
            }
        }
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void Resume()
    {
        _isPaused = false;
    }
}