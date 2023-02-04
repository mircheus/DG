using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisabler : MonoBehaviour
{
    [SerializeField] private float _secondsBeforeDisabling;
    
    public void DisableEnemy(Enemy enemy) 
    {
        StartCoroutine(DisableIn(_secondsBeforeDisabling, enemy));
    }

    private IEnumerator DisableIn(float seconds, Enemy enemy)
    {
        var waitFor = new WaitForSeconds(seconds);
        yield return waitFor;
        enemy.gameObject.SetActive(false);
    }
}
