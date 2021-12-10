using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "statsSystem/stats")]
public class CharacterStats : ScriptableObject
{
    [SerializeField]
    int level, exp, maxHealth, currentHealth, damage, defence, speed, accuaracy;

    public int Level {get => level;  set => level = value; }
    public int Exp {get => exp; set => exp = value;}

    public int MaxHealth {get => maxHealth; set =>maxHealth = value;}
    public int CurrentHealth {get => currentHealth; set => currentHealth = value;}
    public int Damage {get => damage; set => damage = value;}
    public int Defence {get => defence; set => defence = value;}
    public int Speed {get => speed; set =>speed = value;}
    public int Accuaracy {get => accuaracy; set => accuaracy = value;}

    //returns stats as a comma separated value string
    public string GetStatsAsCSVString()
    {
        return level + "," +
            exp + "," +
            maxHealth + "," +
            currentHealth + "," +
            damage + "," +
            defence + "," +
            speed + "," +
            accuaracy + ",";
    }

    public void SetStatsFromCSVString(string stats)
    {
        string[] csv = stats.Split(',');

        level = int.Parse(csv[0]);
        exp = int.Parse(csv[1]);
        maxHealth = int.Parse(csv[2]);
        currentHealth = int.Parse(csv[3]);
        damage = int.Parse(csv[4]);
        defence = int.Parse(csv[5]);
        speed = int.Parse(csv[6]);
        accuaracy = int.Parse(csv[7]);
    }

}
