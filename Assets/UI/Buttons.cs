using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Buttons : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("Overworld");
    }
    //public void OnInstructionButtonPressed()
    //{
    //    SceneManager.LoadScene("Instructions");
    //}
    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
