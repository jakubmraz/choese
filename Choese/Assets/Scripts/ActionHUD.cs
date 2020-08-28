using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ActionHUD : MonoBehaviour
{
    public Image actionHUD;
    public Button AttackButton;
    public Button DefendButton;
    public Button ItemButton;
    public Button SurrenderButton;

    void Start()
    {
        HideActionHUD();
    }

    public void ShowActionHUD()
    {
        if (!actionHUD.enabled)
        {
            actionHUD.enabled = true;
            AttackButton.enabled = true;
            DefendButton.enabled = true;
            ItemButton.enabled = true;
            SurrenderButton.enabled = true;
        }
    }

    public void HideActionHUD()
    {
        if (actionHUD.enabled)
        {
            actionHUD.enabled = false;
            AttackButton.enabled = false;
            DefendButton.enabled = false;
            ItemButton.enabled = false;
            SurrenderButton.enabled = false;
        }
    }
}
