using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHUD;
    [SerializeField] BattleHud enemyHUD;
    [SerializeField] BattleDialogBox dialogBox;
    BattleState state;
    int curAction;
    

    private void Start()
    {
        StartCoroutine(SetUpBattle());
    }


    public IEnumerator SetUpBattle()
    {
        playerUnit.SetUp();
        enemyUnit.SetUp();
        playerHUD.SetData(playerUnit.Character);
        enemyHUD.SetData(enemyUnit.Character);

        dialogBox.SetMoveNames(playerUnit.Character.Attacks);

        yield return dialogBox.TypeDialog($"Un {enemyUnit.Character.Base.Name} te esta atacando.");

        PlayerAction();
    }
    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Elije una accion"));
        dialogBox.EnableActionSelector(true);

    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);  
        dialogBox.EnableMoveSelector(true);  
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;
        var move = playerUnit.Character.Attacks[curAction];
        yield return dialogBox.TypeDialog($"{playerUnit.Character.Base.Name} uso {move.Base.Name}");
        var damageDetails = enemyUnit.Character.TakeDamage(move, playerUnit.Character);
        yield return enemyHUD.UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Character.Base.Name} esta derrotado");
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }
    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;
        var move = enemyUnit.Character.GetRandomMove();
        yield return dialogBox.TypeDialog($"{enemyUnit.Character.Base.Name} uso {move.Base.Name}");
        var damageDetails = playerUnit.Character.TakeDamage(move, enemyUnit.Character);
        yield return playerHUD.UpdateHP();
        yield return ShowDamageDetails(damageDetails);
        if (damageDetails.Fainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Character.Base.Name} esta derrotado");
        }
        else
        {
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(DamageDeatails damageDetails)
    {
        if (damageDetails.Critico > 1f)
            yield return dialogBox.TypeDialog("Golpe Critico!");
        if (damageDetails.Resist > 1f)
            yield return dialogBox.TypeDialog("El objetivo es vulnerable ante este tipo de daño!");
        else if (damageDetails.Resist == 0f)
            yield return dialogBox.TypeDialog("El objetivo es invulnerable!");
        else if (damageDetails.Resist < 1f)
            yield return dialogBox.TypeDialog("El objetivo es resistente a este tipo de daño!");

    }

    private void Update()
    {
        switch (state)
        {
            case BattleState.PlayerAction:
                HandleActionSlection();
                break;
            case BattleState.PlayerMove:
                HandleMoveSelection();
                break;
        }
        
    }

    void HandleActionSlection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (curAction < 1) ++curAction;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (curAction > 0) --curAction;
        }
        dialogBox.UpdateActionSelection(curAction);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            switch (curAction)
            {
                case 0:
                    //fight
                    PlayerMove();
                    break;
                case 1:
                    //run
                    break;
            }
            curAction = 0;
            /*
            if (curAction == 0)
            {
                //fight
                PlayerMove();
            }
            else if(curAction == 1)
            {
                //run
            }
            */

        }

    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (curAction < playerUnit.Character.Attacks.Count - 1) ++curAction;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (curAction > 0) --curAction;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (curAction < playerUnit.Character.Attacks.Count - 2) curAction += 2;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (curAction > 1) curAction -= 2;
        }

        dialogBox.UpdateMoveSelection(curAction, playerUnit.Character.Attacks[curAction]);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
            curAction = 0;
        }
    }
}
