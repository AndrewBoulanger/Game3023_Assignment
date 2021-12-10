using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "EffectSystem/Effect")]
public abstract class IEffect : ScriptableObject
{

    public abstract void ApplyEffect(ICharacter self, ICharacter other);
}
