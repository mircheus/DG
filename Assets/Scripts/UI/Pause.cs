using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    private bool _isPaused = false;
    
    public event UnityAction Paused;
    public event UnityAction Unpaused;
    
    private void Update()
    {
        ShowPauseMenu();
    }

    private void ShowPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _isPaused == false)
        {
            _isPaused = true;
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Paused?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            _isPaused = false;
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            Unpaused?.Invoke();
        }
    }
}
