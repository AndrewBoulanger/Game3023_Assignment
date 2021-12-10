using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "EffectSystem/DamageEnemy")]
public class DamageEffect : IEffect
{
    private int intendedDamage;
    private int resultingDamage;
    public override void ApplyEffect(ICharacter self, ICharacter other)
    {
        intendedDamage = self.CalculateDamage();
        resultingDamage = other.TakeDamage(intendedDamage);
    }

    public override string GetEffectMessage(ICharacter self, ICharacter other)
    {
        return other.name +  " took " + resultingDamage + " points of damage";
    }
}