using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    
    bool isPlayersTurn = true;

    [SerializeField]
    Ability enemyAttack;

    bool ReadyToAdvanceTurn = false;

    bool wonBattle, lostBattle, battleIsOver, readyToLeaveScene;

    [SerializeField]
    GameObject enemyVFXPlayer, enemyHealthBar;

    [SerializeField]
    Text textbox;

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

        Enemy.vfxAnimator = enemyVFXPlayer.GetComponent<Animator>();
        Enemy.GetComponent<EncounterEnemyCharacter>().healthbar = enemyHealthBar.GetComponent<HealthBar>();
        
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
            else if (battleIsOver )
            {
                if (wonBattle)
                {
                    uI.DisplayText(Enemy.name + " was defeated");
                }
                else if(lostBattle)
                    uI.DisplayText("You've run out of health. \nGameOver");
            }

            if (ReadyToAdvanceTurn )
            {
                textbox.text = "advance turns called";
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

        //switch turns based on who just attacked
         isPlayersTurn = (caster == Enemy);

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
        ReadyToAdvanceTurn = false;
        ICharacter currentCharacter = (isPlayersTurn) ? player : Enemy;

        if(wonBattle || lostBattle)
            return;

        if (isPlayersTurn)
        {
            OnPlayerTurnBegin.Invoke(player);
            uI.SetAbilityPanelVisible(true);
            player.TakeTurn();
            textbox.text = "its the players turn";
        }
        else
        {
            uI.SetAbilityPanelVisible(false);
            OnEnemyTurnBegin.Invoke(Enemy);
            Enemy.TakeTurn();
            textbox.text = "its the enemies turn";
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
