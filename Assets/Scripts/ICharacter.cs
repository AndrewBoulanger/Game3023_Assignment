using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ICharacter : MonoBehaviour
{

    public new string name;
    
    public Ability[] abilities;
    public UnityEvent<Ability, ICharacter, string> onAbilityCast;
    public UnityEvent<ICharacter> OnCharacterDefeated;
    public int currentHealth;

    [SerializeField]
    protected CharacterStats stats;

    protected ICharacter opponent;

    [SerializeField]
    public Animator vfxAnimator;

    bool isGuarding, isBuffed;
    public void SetGuarding() => isGuarding = true;
    public void SetIsBuffed() => isBuffed = true;
    public void SetOpponent(ICharacter other)
    {
        if(opponent == null)
            opponent = other;
    }

    protected virtual void Start()
    {
        currentHealth = stats.CurrentHealth;
        print(this.name + currentHealth);
        print(this.name + stats.CurrentHealth);
    }

    public abstract void TakeTurn();

    public void UseAbility(int abilitySlot)
    {
        abilities[abilitySlot].Cast(this, opponent);

    }

    public virtual int TakeDamage(int damage)
    {
        print(currentHealth);
        float tempDamage = damage;
        if (isGuarding)
        {
            tempDamage *= (1.0f - ((float)stats.Defence)/100.0f);
            isGuarding = false;
        }

        currentHealth = currentHealth - (int)tempDamage;

        if(currentHealth <= 0)
            OnCharacterDefeated.Invoke(this);
        
        return (int)tempDamage;
    }

    public virtual void AddHealth(int healthToAdd)
    {
        currentHealth += healthToAdd;

        if(currentHealth > stats.MaxHealth)
           currentHealth = stats.MaxHealth;

               print(currentHealth);
    }

    public int CalculateDamage()
    {
        int damage = stats.Damage;

        if(isBuffed)
        { 
            damage *= 2;
            isBuffed = false;
        }
        return damage;
    }
}
