using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "AbilitySystem/Ability")]

public class Ability : ScriptableObject
{
    [SerializeField]
    private new string name;

    [SerializeField]
    public new int Id;

    [SerializeField]
    private AbilityTargets target;

    [SerializeField]
    private IEffect[] effects;

    [SerializeField]
    private AbilityTypes type;
    
    [SerializeField]
    private AbilityAnimationTriggers animToPlay;
    public AbilityAnimationTriggers AnimToPlay { get => animToPlay;  }

    public AbilityTypes Type { get => type; private set => type = value; }

    public void Cast(ICharacter self, ICharacter other)
    {

        string message = self.name + " used " + name;

        foreach (IEffect effect in effects)
        {
            effect.ApplyEffect(self, other);

            message += "\n" + effect.GetEffectMessage(self, other);
        }

        if(target == AbilityTargets.Self)
            self.vfxAnimator.SetTrigger(animToPlay.ToString());
        else if(target == AbilityTargets.Enemy)
            other.vfxAnimator.SetTrigger(animToPlay.ToString());

        self.onAbilityCast.Invoke(this, self, message);
    }
}
