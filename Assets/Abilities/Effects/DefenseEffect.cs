using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "EffectSystem/Guard")]
public class DefenseEffect : IEffect
{
    public override void ApplyEffect(ICharacter self, ICharacter other)
    {

        self.SetGuarding();
    }

    public override string GetEffectMessage(ICharacter self, ICharacter other)
    {
        return "Preparing to take damage";
    }
}