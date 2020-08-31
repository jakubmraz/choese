using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitState { NORMAL, DEFENDING }

public class BattleUnit : MonoBehaviour
{
    public string unitName;

    public int unitHP;
    public int unitDamage;
    public int unitDefense;

    public int unitCurrentHP;
    public UnitState unitState = UnitState.NORMAL;

    public bool TakeDamage(int dmg)
    {
        dmg -= unitDefense;

        //If the unit is defending, the damage gets reduced once again, meaning a defending unit has double defense
        if (unitState == UnitState.DEFENDING)
        {
            dmg -= unitDefense;
        }

        //Negative damage won't heal
        if (dmg < 0)
            dmg = 0;

        unitCurrentHP -= dmg;

        if (unitCurrentHP < 0)
            return true;
        return false;
    }

    public void StartDefending()
    {
        unitState = UnitState.DEFENDING;
    }

    public void StopDefending()
    {
        unitState = UnitState.NORMAL;
    }
}
