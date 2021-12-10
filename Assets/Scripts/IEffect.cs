using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class IEffect : ScriptableObject
{
    protected string effect;
    public abstract void ApplyEffect(ICharacter self, ICharacter other);
    public abstract string GetEffectMessage(ICharacter self, ICharacter other);
}


