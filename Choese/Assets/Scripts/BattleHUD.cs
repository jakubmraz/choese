using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    //HP Box
    public Text nameText;
    public Slider hpSlider;

    //Text Box
    public Text battleText;

    public void SetHUD(BattleUnit unit)
    {
        nameText.text = unit.unitName;
        hpSlider.maxValue = unit.unitHP;
        hpSlider.value = unit.unitCurrentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void UpdateBattleText(string text)
    {
        battleText.text = text;
    }
}
