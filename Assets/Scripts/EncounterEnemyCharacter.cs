using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterEnemyCharacter : ICharacter
{
    //ordered list of probability values, distance from the previous value represents the probability of that ability being called
    List<float> AbilityProbabilityRange;

    [SerializeField]
    EnemyAIPattern maxHealthbehaviour;

    [SerializeField]
    EnemyAIPattern healthOverHalfbehaviour;

    [SerializeField]
    EnemyAIPattern HealthBelowHalfbehaviour;

    [SerializeField]
    EnemyAIPattern criticalHealthbehaviour;

    private void Start()
    {
        AbilityProbabilityRange = new List<float>();
        currentHealth = maxHealth;
        UpdateAIPattern();
    }

  
    public override void TakeTurn()
    {
        //generates a value somewhere within the ability probability range
        float randomValue = Random.Range(0, AbilityProbabilityRange[AbilityProbabilityRange.Count -1] );

        //calls the corresponding ability
        for(int i = 0; i < AbilityProbabilityRange.Count; i++)
        {
            if(randomValue < AbilityProbabilityRange[i])
            {
                abilities[i].Cast(this, opponent);
                break;
            }
        }

    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        UpdateAIPattern();

    }

    //changes the AI pattern based on the enemy's health
   void UpdateAIPattern()
    {
        if (currentHealth == maxHealth)
            ChangeEnemyAIPattern(maxHealthbehaviour);
        else if (currentHealth > maxHealth * 0.5)
            ChangeEnemyAIPattern(healthOverHalfbehaviour);
        else if (currentHealth > maxHealth * 0.25)
            ChangeEnemyAIPattern(HealthBelowHalfbehaviour);
        else if (currentHealth < 0.25)
            ChangeEnemyAIPattern(criticalHealthbehaviour);
    }


    //sets probability of each attack based on the new AIPattern
    void ChangeEnemyAIPattern(EnemyAIPattern newPattern)
    {
       
        AbilityProbabilityRange.Clear();

        float abilityChance = 0.0f;

        foreach(Ability a in abilities)
        {
            if(a != null)
            { 
                abilityChance += newPattern.GetAttackProbablity(a.Type);
                AbilityProbabilityRange.Add(abilityChance);
            }
        }

        
    }

}
