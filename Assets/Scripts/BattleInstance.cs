using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleInstance : MonoBehaviour
{
    [SerializeField]
    ICharacter player;

    [SerializeField]
    Spawner enemyPicker;
    [SerializeField]
    ICharacter Enemy;

    [SerializeField]
    EncounterUI uI;
    
    bool isPlayersTurn = false;

    [SerializeField]
    Ability enemyAttack;

    bool ReadyToAdvanceTurn;

    bool wonBattle, lostBattle, battleIsOver, readyToLeaveScene;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = enemyPicker.randomPicker();
        ICharacter.Instantiate(Enemy);
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
        if(Input.GetButtonDown("Submit"))
        {
            if(readyToLeaveScene)
            {
                EndBattleScene();
            }
            else if (battleIsOver && Input.GetButtonDown("Submit"))
            {
                if (wonBattle)
                {
                    uI.DisplayText(Enemy.name + " was defeated");
                }
                else
                    uI.DisplayText("You've run out of health. \nGameOver");
            }

            if (ReadyToAdvanceTurn && Input.GetButtonDown("Submit"))
            {
                ReadyToAdvanceTurn = false;

                AdvanceTurns();
            }


        }
    }


    public UnityEvent<ICharacter> OnPlayerTurnBegin;
    public UnityEvent<ICharacter> OnEnemyTurnBegin;
    
    private void OnAbilityCast(Ability ability, ICharacter caster, string msg)
    {
        uI.DisplayText(msg);
         uI.SetAbilityPanelVisible(false);
    }

    private void OnTextDisplayed()
    {
        if(battleIsOver)
        {
            readyToLeaveScene = true;
        }
        else if(wonBattle || lostBattle)
        {
            battleIsOver = true;
        }
        else
            ReadyToAdvanceTurn = true;
    }

    public void AdvanceTurns()
    {
        isPlayersTurn = !isPlayersTurn;

        ICharacter currentCharacter = (isPlayersTurn) ? player : Enemy;

        if(wonBattle || lostBattle)
            return;

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
        lostBattle = true;
    }

    private void OnEnemyDefeated(ICharacter enemy)
    {
        wonBattle = true;
    }

    private void EndBattleScene()
    {
        if(wonBattle)
            GetComponent<BattleSceneManager>().LeaveScene();
        else
            GetComponent<BattleSceneManager>().ExitGame();
    }
}
