using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ICharacter : MonoBehaviour
{

    public new string name;
    
    public Ability[] abilities;
    public UnityEvent<Ability, ICharacter> onAbilityCast;
    public UnityEvent<ICharacter> OnCharacterDefeated;

    protected ICharacter opponent;

    [SerializeField]
    protected int maxHealth;
    protected int currentHealth;

    public void SetOpponent(ICharacter other)
    {
        if(opponent == null)
            opponent = other;
    }

    public abstract void TakeTurn();

    public void UseAbility(int abilitySlot)
    {
        abilities[abilitySlot].Cast(this, opponent);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
            OnCharacterDefeated.Invoke(this);

    }

}
