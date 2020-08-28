using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;

    public int enemyDamage;
    public int enemyMaxHP;

    public int enemyCurrentHP;

    public bool TakeDamage(int dmg)
    {
        enemyCurrentHP -= dmg;

        if (enemyCurrentHP < 0)
            return true;
        return false;
    }
}
