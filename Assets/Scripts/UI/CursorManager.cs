using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D _cursorSprite;

    private Vector2 _cursorHotspot;

    private void Start()
    {
        _cursorHotspot = new Vector2(_cursorSprite.width / 2, _cursorSprite.height / 2);
        // _cursorSprite.filterMode = 0;
        // _cursorSprite.filterMode.
        Cursor.SetCursor(_cursorSprite, _cursorHotspot, CursorMode.Auto);
    }
}
