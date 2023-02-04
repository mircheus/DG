using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSideChecker : MonoBehaviour
{
    private float _zRotation;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckSpriteSide();
    }

    private void CheckSpriteSide()
    {
        Vector3 lookDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        float rotationZ = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        _zRotation = rotationZ;

        if (rotationZ < -90 || rotationZ > 90)
        {
            TurnCharacterLeft();
        }
        else
        {
            TurnCharacterRight();
        }
    }
    
    private void TurnCharacterLeft()
    {
        _spriteRenderer.flipX = true;
    }

    private void TurnCharacterRight()
    {
        _spriteRenderer.flipX = false;
    }
}
