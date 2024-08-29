using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHub playerHub;
    [SerializeField] BattleHub enemyHub;

    [SerializeField] BattleDialogBox dialogBox;

    BattleState state;
    private int currentAction;
    private int currentMove;

    private void Start()
    {
        StartCoroutine(SetUpBattle());
    }

    public IEnumerator SetUpBattle()
    {
        playerUnit.SetUp();
        enemyUnit.SetUp();
        playerHub.SetData(playerUnit.Pokemon);
        enemyHub.SetData(enemyUnit.Pokemon);

        dialogBox.SetMoveNames(playerUnit.Pokemon.Moves);

        yield return dialogBox.TypeDialog($"A wild {enemyUnit.Pokemon.Base.name} appeared.");

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelection(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelection(false);
        dialogBox.EnableDialogBox(false);
        dialogBox.EnableMoveSelection(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        var move = playerUnit.Pokemon.Moves[currentMove];
        yield return dialogBox.TypeDialog($" {playerUnit.Pokemon.Base.Name} used {move.Base.Name}");


        var damageDetails = enemyUnit.Pokemon.TakeDamage(move, playerUnit.Pokemon);
        yield return enemyHub.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($" {enemyUnit.Pokemon.Base.Name} Fainted");
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Pokemon.GetRandomMove();
        yield return dialogBox.TypeDialog($" {enemyUnit.Pokemon.Base.Name} used {move.Base.Name}");

        var damageDetails = playerUnit.Pokemon.TakeDamage(move, enemyUnit.Pokemon);
        yield return playerHub.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($" {playerUnit.Pokemon.Base.Name} Fainted");
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(Pokemon.DamageDetails damageDetails)
    {
        if (damageDetails.Critical > 1f)
            yield return dialogBox.TypeDialog("A critcal hit!");

        if (damageDetails.TypeEffectiveness > 1f)
            yield return dialogBox.TypeDialog("It's super effective");
        else if (damageDetails.TypeEffectiveness < 1f)
            yield return dialogBox.TypeDialog("It's not very effective");
    }

    private void Update()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction < 1)
                ++currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
                --currentAction;
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                //Fight
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                //Run
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.Pokemon.Moves.Count - 1)
                ++currentMove;
        }else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
                 --currentMove;
        }else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.Pokemon.Moves.Count - 2)
                currentMove += 2;
        }else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1)
                currentMove -= 2;
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableMoveSelection(false);
            dialogBox.EnableDialogBox(true);
            StartCoroutine(PerformPlayerMove());
        }
    }
}
