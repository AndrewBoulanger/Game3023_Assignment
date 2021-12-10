using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;

   public GameObject pauseMenuUI;
  
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {

            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }


        }
    }
   public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        isGamePaused = true;
    }
    public void LoadMainMenu()
    {
        isGamePaused = false;
        SceneManager.LoadScene("MainMenuScene");
    }
    public void Save()
    {

    }
}
