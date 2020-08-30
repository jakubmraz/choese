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

    public void SetHUD(PlayerBattle unit)
    {
        nameText.text = unit.playerName;
        hpSlider.maxValue = unit.playerHP;
        hpSlider.value = unit.playerCurrentHP;
    }

    public void SetHUD(Enemy unit)
    {
        nameText.text = unit.enemyName;
        hpSlider.maxValue = unit.enemyMaxHP;
        hpSlider.value = unit.enemyCurrentHP;
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
