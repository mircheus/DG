using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    private ParticleSystemRenderer PSR;

    private void Start()
    {
        PSR = GetComponent<ParticleSystemRenderer>();
        // PSR.gameObject.SetActive(false);
    }

    private float InputX;
    
    private void Update()
    {
        InputX = Input.GetAxisRaw("Horizontal");
        //
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     // PSR.gameObject.SetActive(true);
        // }
    
        if (InputX < 0)
        {
            PSR.flip = Vector3.right;
            Debug.LogWarning($"PSR flip = {PSR.flip}");
        }
        else if (InputX > 0)
        {
            PSR.flip = Vector3.left;
            Debug.LogWarning($"PSR flip = {PSR.flip}"); 
        }
    }
}
