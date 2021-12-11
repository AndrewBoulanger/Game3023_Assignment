using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   public List<EncounterEnemyCharacter> Enemies;


    public  void Start()
    {
        



    }

    public EncounterEnemyCharacter randomPicker()
    {
      int  randomPicker = Random.Range(0, 4);
        if(randomPicker == 0)
        {
            return Enemies[0];
        }
        else if(randomPicker == 1)
        {
            return Enemies[1];
        }
        else if(randomPicker == 2)
        {
            return Enemies[2];
        }
        else if (randomPicker == 3)
        {
            return Enemies[3];
        }
        else
            return Enemies[0];


    }
}
