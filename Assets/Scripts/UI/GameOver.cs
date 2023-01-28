using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _gameOver;

    private void OnEnable()
    {
        _player.Died += ShowGameOver;
    }

    private void OnDisable()
    {
        _player.Died -= ShowGameOver;
    }

    private void ShowGameOver()
    {
        _gameOver.SetActive(true);
        Time.timeScale = 0.1f;
    }
}
