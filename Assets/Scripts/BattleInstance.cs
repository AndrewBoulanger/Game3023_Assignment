using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleInstance : MonoBehaviour
{
    [SerializeField]
    ICharacter player;
    
    [SerializeField]
    ICharacter Enemy;

    [SerializeField]
    EncounterUI uI;
    
    bool isPlayersTurn = false;

    [SerializeField]
    Ability enemyAttack;

    bool ReadyToAdvanceTurn;

    // Start is called before the first frame update
    void Start()
    {
       player.onAbilityCast.AddListener(OnAbilityCast);
        Enemy.onAbilityCast.AddListener(OnAbilityCast);
        player.OnCharacterDefeated.AddListener(OnPlayerDefeated);
        Enemy.OnCharacterDefeated.AddListener(OnEnemyDefeated);
       uI.onTextAnimationDone.AddListener(OnTextDisplayed);

        player.SetOpponent(Enemy);
        Enemy.SetOpponent(player);
    }



    // Update is called once per frame
    void Update()
    {
        if(ReadyToAdvanceTurn && Input.GetButtonDown("Submit"))
        {
            ReadyToAdvanceTurn = false;
           AdvanceTurns();
        }
    }


    public UnityEvent<ICharacter> OnPlayerTurnBegin;
    public UnityEvent<ICharacter> OnEnemyTurnBegin;
    
    private void OnAbilityCast(Ability ability, ICharacter caster)
    {
        uI.DisplayText(caster.gameObject.name + " used " + ability.name);
         uI.SetAbilityPanelVisible(false);
    }

    private void OnTextDisplayed()
    {
        ReadyToAdvanceTurn = true;
    }

    public void AdvanceTurns()
    {
        isPlayersTurn = !isPlayersTurn;

        ICharacter currentCharacter = (isPlayersTurn) ? player : Enemy;

        if (isPlayersTurn)
        {
            OnPlayerTurnBegin.Invoke(player);
            uI.SetAbilityPanelVisible(true);
            player.TakeTurn();
        }
        else
        {
           // uI.SetAbilityPanelVisible(false);
            OnEnemyTurnBegin.Invoke(Enemy);
            Enemy.TakeTurn();
        }
    }


    private void OnPlayerDefeated(ICharacter player)
    {
        uI.DisplayText("You've run out of health. \n Game Over");
    }

    private void OnEnemyDefeated(ICharacter enemy)
    {
        uI.DisplayText(enemy.gameObject.name  + " was defeated");
    }

}
