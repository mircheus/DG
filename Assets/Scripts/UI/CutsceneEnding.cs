using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneEnding : MonoBehaviour
{
    [SerializeField] private SceneLoader _sceneLoader;
    
    private void OnEnable()
    {
        _sceneLoader.LoadMainMenu();
    }
}
