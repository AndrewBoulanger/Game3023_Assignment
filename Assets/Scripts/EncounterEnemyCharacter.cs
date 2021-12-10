using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterEnemyCharacter : ICharacter
{
    public HealthBar healthbar;
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
        stats.CurrentHealth = stats.MaxHealth;
        AbilityProbabilityRange = new List<float>();
        UpdateAIPattern();
        healthbar.SetMaxHealth(stats.MaxHealth);
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
                UseAbility(i);

                break;
            }
        }

    }

    public override int TakeDamage(int damage)
    {
        int damageResult = base.TakeDamage(damage);

        UpdateAIPattern();
        healthbar.SetHealth(stats.CurrentHealth);
        return damageResult;
    }

    public override void AddHealth(int healthToAdd)
    {
        base.AddHealth(healthToAdd);
        healthbar.SetHealth(stats.CurrentHealth);
        UpdateAIPattern();
    }

    //changes the AI pattern based on the enemy's health
    void UpdateAIPattern()
    {
        if (stats.CurrentHealth == stats.MaxHealth)
            ChangeEnemyAIPattern(maxHealthbehaviour);
        else if (stats.CurrentHealth > stats.MaxHealth * 0.5)
            ChangeEnemyAIPattern(healthOverHalfbehaviour);
        else if (stats.CurrentHealth > stats.MaxHealth * 0.25)
            ChangeEnemyAIPattern(HealthBelowHalfbehaviour);
        else if (stats.CurrentHealth < 0.25)
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
