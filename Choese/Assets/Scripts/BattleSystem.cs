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
        playerUnit = playerGameObject.GetComponent<PlayerBattle>();

        GameObject enemyGameObject = Instantiate(enemyPrefab, enemyBattleLocation);
        enemyUnit = enemyGameObject.GetComponent<Enemy>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        textHUD.UpdateBattleText($"You have approached {enemyUnit.enemyName}.");

        yield return new WaitForSeconds(2f);

        State = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        actionHUD.ShowActionHUD();
        textHUD.UpdateBattleText("Select how to lay waste to them.");
    }

    public void OnAttackButton()
    {
        actionHUD.HideActionHUD();
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        bool isEnemyDead = enemyUnit.TakeDamage(playerUnit.playerDamage);

        textHUD.UpdateBattleText($"You have hurt {enemyUnit.enemyName} for (code damage here)!");
        enemyHUD.SetHP(enemyUnit.enemyCurrentHP);

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

    void EndBattle()
    {
        if (State == BattleState.WON)
        {
            textHUD.UpdateBattleText($"You have destroyed {enemyUnit.enemyName} epic style.");
        }
        else if (State == BattleState.LOST)
        {
            textHUD.UpdateBattleText("You have pathetically lost like the pathetic loser you are.");
        }
    }

    IEnumerator EnemyTurn()
    {
        bool isPlayerDead = playerUnit.TakeDamage(enemyUnit.enemyDamage);

        textHUD.UpdateBattleText($"{enemyUnit.enemyName} used Death and Destruction!");
        playerHUD.SetHP(playerUnit.playerCurrentHP);

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
