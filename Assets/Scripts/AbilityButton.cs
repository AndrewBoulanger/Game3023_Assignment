using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField]
    Text buttonText;

    public void SetButtonText(string label)
    {
        buttonText.text = label;
    }
    

}
