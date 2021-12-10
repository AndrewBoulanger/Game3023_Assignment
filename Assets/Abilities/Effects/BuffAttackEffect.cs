using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "EffectSystem/BuffAttack")]
public class BuffAttackEffect : IEffect
{
    public override void ApplyEffect(ICharacter self, ICharacter other)
    {

        self.SetIsBuffed();
    }

    public override string GetEffectMessage(ICharacter self, ICharacter other)
    {
        return "Preparing to Attack";
    }
}
