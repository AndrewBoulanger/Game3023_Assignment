using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Buttons : MonoBehaviour
{
    Scene m_Scene;
    string sceneName;
    public void OnPlayButtonPressed()
    {
        if (SaveSystem.LoadPlayer() != null)
        {
            PlayerData data = SaveSystem.LoadPlayer();

            sceneName = data.sceneName;

            SceneManager.LoadScene(sceneName);
        }
        else
            SceneManager.LoadScene("CafeTeria");

    }
   
    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
  
     
}
