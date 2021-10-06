using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    WorldTraveler OverworldSprite;
    private void Start()
    {
        OverworldSprite = FindObjectOfType<WorldTraveler>();
        if(OverworldSprite != null)
        {
            OverworldSprite.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
     
    }
}
