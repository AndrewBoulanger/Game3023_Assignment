using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    [SerializeField]
    Button fleeButton;

    //could change this to a non-scene dependent object that saves the last scene's name when leaving the scene, maybe a scriptable object
    [SerializeField]
    string lastScene = "Cafeteria";

    WorldTraveler OverworldSprite;
    private void Start()
    {
        OverworldSprite = FindObjectOfType<WorldTraveler>();
        if(OverworldSprite != null)
        {
            OverworldSprite.gameObject.SetActive(false);
        }

        fleeButton.onClick.AddListener(LeaveScene);
    }

    public void LeaveScene()
    {
        SceneManager.LoadScene(lastScene);
 
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    private void OnDisable()
    {
     
    }
}
