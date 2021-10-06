using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FleeButton : MonoBehaviour
{
    Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        SceneManager.LoadScene(tag);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
