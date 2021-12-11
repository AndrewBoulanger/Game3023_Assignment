using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterEnemyCharacter : ICharacter
{
    public HealthBar healthbar;
    //ordered list of probability values, distance from the previous value represents the probability of that ability being called
    List<float> AbilityProbabilityRange;

    EnemyAIPattern aiPattern;

    [SerializeField]
    EnemyAIPattern maxHealthbehaviour;

    [SerializeField]
    EnemyAIPattern healthOverHalfbehaviour;

    [SerializeField]
    EnemyAIPattern HealthBelowHalfbehaviour;

    [SerializeField]
    EnemyAIPattern criticalHealthbehaviour;

    protected override void Start()
    {
        base.Start();
  
        currentHealth = stats.MaxHealth;
        AbilityProbabilityRange = new List<float>();
        UpdateAIPattern();
        if(healthbar != null)
            healthbar.SetMaxHealth(stats.MaxHealth);

    }

  
    public override void TakeTurn()
    {
        if(aiPattern == null)
            UpdateAIPattern();
        //generates a value somewhere within the ability probability range
        float randomValue = Random.Range(0, AbilityProbabilityRange[AbilityProbabilityRange.Count -1] );

        //calls the corresponding ability
        for(int i = 0; i < AbilityProbabilityRange.Count ; i++)
        {
            if(randomValue <= AbilityProbabilityRange[i])
            {
                UseAbility(i);
                
                return;
            }
            print(i);
        }
        
        print(randomValue);
    }

    public override int TakeDamage(int damage)
    {
        print(currentHealth);
        int damageResult = base.TakeDamage(damage);

        UpdateAIPattern();
        healthbar.SetHealth(currentHealth);
        return damageResult;
    }

    public override void AddHealth(int healthToAdd)
    {
        base.AddHealth(healthToAdd);
        healthbar.SetHealth(currentHealth);
        UpdateAIPattern();
    }

    //changes the AI pattern based on the enemy's health
    void UpdateAIPattern()
    {
        if (currentHealth == stats.MaxHealth)
            ChangeEnemyAIPattern(maxHealthbehaviour);
        else if (currentHealth > stats.MaxHealth * 0.5f)
            ChangeEnemyAIPattern(healthOverHalfbehaviour);
        else if (currentHealth > stats.MaxHealth * 0.25f)
            ChangeEnemyAIPattern(HealthBelowHalfbehaviour);
        else 
            ChangeEnemyAIPattern(criticalHealthbehaviour);
    }


    //sets probability of each attack based on the new AIPattern
    void ChangeEnemyAIPattern(EnemyAIPattern newPattern)
    {
       if(AbilityProbabilityRange == null)
            AbilityProbabilityRange = new List<float>();
        AbilityProbabilityRange.Clear();

        float abilityChance = 0.0f;

        foreach(Ability a in abilities)
        {
            abilityChance += newPattern.GetAttackProbablity(a.Type);
            AbilityProbabilityRange.Add(abilityChance);
        }

        aiPattern = newPattern;
    }

}
