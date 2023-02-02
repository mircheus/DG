using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{ 
    [SerializeField] private Vector2 _parallaxMultiplier;
    private Transform _cameraTransform;
    private Vector3 _lastCameraPosition;
    private float _textureUnitSizeX;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraPosition = _cameraTransform.position;
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Texture2D texture = spriteRenderer.sprite.texture;
        // Debug.Log(spriteRenderer.size.x);
        // _textureUnitSizeX = texture.width / sprite.sprite.pixelsPerUnit;
        _textureUnitSizeX = spriteRenderer.size.x / spriteRenderer.sprite.pixelsPerUnit;
        // Debug.Log(_textureUnitSizeX);
    }

    private void LateUpdate()
    {
        var position = _cameraTransform.position;
        Vector3 deltaMovement = position - _lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * _parallaxMultiplier.x, deltaMovement.y * _parallaxMultiplier.y);
        _lastCameraPosition = position;

        // if (_cameraTransform.position.x - transform.position.x >= _textureUnitSizeX)
        // {
        //     float offsetPositionX = (_cameraTransform.position.x - transform.position.x) % _textureUnitSizeX;
        //     transform.position = new Vector3(_cameraTransform.position.x + offsetPositionX, transform.position.y);
        // }
    }
}
