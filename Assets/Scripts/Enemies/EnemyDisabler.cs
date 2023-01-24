using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisabler : MonoBehaviour
{
    public void DisableEnemy(Enemy enemy) 
    {
        enemy.gameObject.SetActive(false);
    }
}
