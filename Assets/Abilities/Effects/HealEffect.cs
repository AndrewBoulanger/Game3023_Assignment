using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "EffectSystem/HealSelf")]
public class HealEffect : IEffect
{
    [SerializeField]
    private int healthToAdd;
    public override void ApplyEffect(ICharacter self, ICharacter other)
    {

    }

    public override string GetEffectMessage(ICharacter self, ICharacter other)
    {
        return self.name + " healed " + healthToAdd + " points of damage";
    }
}

