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

    private PlayerBattle playerUnit;
    private Enemy enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public ActionHUD actionHUD;

        // Start is called before the first frame update
    void Start()
    {
        State = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGameObject = Instantiate(playerPrefab, playerBattleLocation);
        playerUnit = playerGameObject.GetComponent<PlayerBattle>();

        GameObject enemyGameObject = Instantiate(enemyPrefab, enemyBattleLocation);
        enemyUnit = enemyGameObject.GetComponent<Enemy>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        State = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        actionHUD.ShowActionHUD();
    }

    public void OnAttackButton()
    {
        actionHUD.HideActionHUD();
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        bool isEnemyDead = enemyUnit.TakeDamage(playerUnit.playerDamage);

        enemyHUD.SetHP(enemyUnit.enemyCurrentHP);

        yield return new WaitForSeconds(0.5f);

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

    void EndBattle()
    {
        if (State == BattleState.WON)
        {
            //u won
        }
        else if (State == BattleState.LOST)
        {
            //u lost
        }
    }

    IEnumerator EnemyTurn()
    {
        bool isPlayerDead = playerUnit.TakeDamage(enemyUnit.enemyDamage);

        playerHUD.SetHP(playerUnit.playerCurrentHP);

        yield return new WaitForSeconds(0.5f);

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
