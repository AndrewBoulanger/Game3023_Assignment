using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AIPattern", menuName = "AIPatternSystem/AIPattern")]
public class EnemyAIPattern : ScriptableObject
{
    public float buffs_and_debuffs;
    public float attacks;
    public float healing;
    public float defensive;

    public float GetAttackProbablity(AbilityTypes type )
    {
        if(type == AbilityTypes.Buff || type == AbilityTypes.Debuff)
        {
            return buffs_and_debuffs;
        }
        else if(type == AbilityTypes.Attack)
        {
            return attacks;
        }
        else if(type == AbilityTypes.Healing)
        {
            return healing;
        }
        else if(type == AbilityTypes.Guard)
        {
            return defensive;
        }

        Debug.Log("Attack type not supported, please update Enemy AI Patterns"); 
        return 0;
    }
}
