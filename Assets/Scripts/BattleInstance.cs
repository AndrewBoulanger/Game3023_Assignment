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

    // Start is called before the first frame update
    void Start()
    {
       player.onAbilityCast.AddListener(OnAbilityCast);
        Enemy.onAbilityCast.AddListener(OnAbilityCast);
       uI.onTextAnimationDone.AddListener(OnTextDisplayed);

        player.SetOpponent(Enemy);
        Enemy.SetOpponent(player);
    }



    // Update is called once per frame
    void Update()
    {
        if((!isPlayersTurn) && Input.GetMouseButtonDown(0))
        {
           // EnemyAttack();
        }
    }


    public UnityEvent<ICharacter> OnPlayerTurnBegin;
    public UnityEvent<ICharacter> OnEnemyTurnBegin;
    
    private void OnAbilityCast(Ability ability, ICharacter caster)
    {
        uI.DisplayText(caster.name + " used " + ability.name);
    }

    private void OnTextDisplayed()
    {
        AdvanceTurns();
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
            uI.SetAbilityPanelVisible(false);
            OnEnemyTurnBegin.Invoke(Enemy);
            Enemy.TakeTurn();
        }
    }

}
