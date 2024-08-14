using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHub playerHub;
    [SerializeField] BattleHub enemyHub;

    [SerializeField] BattleDialogBox dialogBox;

    private void Start()
    {
        SetUpBattle();
    }

    private void SetUpBattle()
    {
        playerUnit.SetUp();
        enemyUnit.SetUp();
        playerHub.SetData(playerUnit.Pokemon);
        enemyHub.SetData(enemyUnit.Pokemon);

        StartCoroutine(dialogBox.TypeDialog($"A wild {playerUnit.Pokemon.Base.name} appeared."));
    }
}
