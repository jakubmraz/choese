using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState State;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleLocation;
    public Transform enemyBattleLocation;

    private BattleUnit playerUnit;
    private BattleUnit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public ActionHUD actionHUD;
    public BattleHUD textHUD;

    // Start is called before the first frame update
    void Start()
    {
        State = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGameObject = Instantiate(playerPrefab, playerBattleLocation);
        playerUnit = playerGameObject.GetComponent<BattleUnit>();

        GameObject enemyGameObject = Instantiate(enemyPrefab, enemyBattleLocation);
        enemyUnit = enemyGameObject.GetComponent<BattleUnit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        textHUD.UpdateBattleText($"You have approached {enemyUnit.unitName}.");

        yield return new WaitForSeconds(2f);

        State = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        //Drops the defense status at the start of the turn again
        playerUnit.StopDefending();

        actionHUD.ShowActionHUD();
        textHUD.UpdateBattleText("Select how to lay waste to them.");
    }

    public void OnAttackButton()
    {
        actionHUD.HideActionHUD();
        StartCoroutine(PlayerAttack());
    }

    public void OnDefenseButton()
    {
        actionHUD.HideActionHUD();
        playerUnit.StartDefending();

        textHUD.UpdateBattleText($"You are defending.");

        StartCoroutine(PlayerDefend());
    }

    IEnumerator PlayerAttack()
    {
        //Used to calculate how much dmg was dealt for battle text
        int HPbefore = enemyUnit.unitCurrentHP;
        int HPdifference;

        bool isEnemyDead = enemyUnit.TakeDamage(playerUnit.unitDamage);

        HPdifference = HPbefore - enemyUnit.unitCurrentHP;

        textHUD.UpdateBattleText($"You have hurt {enemyUnit.unitName} for {HPdifference} DMG!");
        enemyHUD.SetHP(enemyUnit.unitCurrentHP);

        yield return new WaitForSeconds(1f);

        if (isEnemyDead)
        {
            //End the battle
            State = BattleState.WON;
            EndBattle();
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    //This is literally just here for the 1 second delay
    IEnumerator PlayerDefend()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyTurn());
    }

    void EndBattle()
    {
        if (State == BattleState.WON)
        {
            textHUD.UpdateBattleText($"You have destroyed {enemyUnit.unitName} epic style.");
        }
        else if (State == BattleState.LOST)
        {
            textHUD.UpdateBattleText("You have pathetically lost like the pathetic loser you are.");
        }
    }

    IEnumerator EnemyTurn()
    {
        bool isPlayerDead = playerUnit.TakeDamage(enemyUnit.unitDamage);

        textHUD.UpdateBattleText($"{enemyUnit.unitName} used Death and Destruction!");
        playerHUD.SetHP(playerUnit.unitCurrentHP);

        yield return new WaitForSeconds(1f);

        if (isPlayerDead)
        {
            State = BattleState.LOST;
            EndBattle();
        }
        else
        {
            State = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
}
