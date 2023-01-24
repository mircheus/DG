using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLookToPlayer : MonoBehaviour
{
    [SerializeField] private Player _player;
    private void Update()
    {
        LightToPLayer();
    }
    
    private void LightToPLayer()
    {
        // float step = 5 * Time.deltaTime;
        // Vector3 newDirection = Vector3.RotateTowards(_alarmLight.transform.forward, playerDirection, step, 0f);
        // _alarmLight.transform.localRotation = Quaternion.LookRotation(newDirection);
        
        Vector3 playerDirection = (_player.transform.position - transform.position).normalized;
        // Vector3 playerDirection = (transform.position - _player.transform.position).normalized;
        float rotationZ = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }
}
