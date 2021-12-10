using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "AbilitySystem/Ability")]
public class Ability : ScriptableObject
{
    [SerializeField]
    private new string name;

    [SerializeField]
    private AbilityTargets target;

    [SerializeField]
    private IEffect[] effects;

    public void Cast(ICharacter self, ICharacter other)
    {

        Debug.Log("Cast " + name);

        foreach(IEffect effect in effects)
        {
            effect.ApplyEffect(self, other);
        }

        self.onAbilityCast.Invoke(this, self);
    }
}
