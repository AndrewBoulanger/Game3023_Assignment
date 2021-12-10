using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Buttons : MonoBehaviour
{
 
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Cafeteria");
    }
   
    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
  
     
}
