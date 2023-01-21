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
        // _handGunPosition.position += new Vector3(-0.12f, 0, 0);
        // Debug.Log($"x:{_handGunPosition.position.x} y:{_handGunPosition.position.y} z:{_handGunPosition.position.z}");
    }

    private void TurnCharacterRight()
    {
        _spriteRenderer.flipX = false;
        // _handGunPosition.position += new Vector3(0.12f, 0, 0);
        // Debug.Log($"x:{_handGunPosition.position.x} y:{_handGunPosition.position.y} z:{_handGunPosition.position.z}");
    }
}
