using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public string playerName;

    public int playerDamage;
    public int playerHP;

    public int playerCurrentHP;

    public bool TakeDamage(int dmg)
    {
        playerCurrentHP -= dmg;

        if (playerCurrentHP < 0)
            return true;
        return false;
    }
}
