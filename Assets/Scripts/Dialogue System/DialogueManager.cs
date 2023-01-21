using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]  private Queue<string> _sentences;
    
    private void Start()
    {
        _sentences = new Queue<string>();
    }
}
