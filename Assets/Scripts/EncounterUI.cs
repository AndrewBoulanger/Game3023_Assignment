using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EncounterUI : MonoBehaviour
{

    [SerializeField]
    TMPro.TextMeshProUGUI encounterText;

    [SerializeField]
    float timeBetweenCharacters= 0.1f;

    [SerializeField]
    private GameObject abilityPanel;

    private IEnumerator animateTextReference = null;

    public UnityEvent onTextAnimationDone;

    // Start is called before the first frame update
    void Start()
    {
        //disable panel => animate text => display panel

        abilityPanel.SetActive(false);

        animateTextReference = AnimateText("You have encountered an opponent");
        StartCoroutine(animateTextReference);

       // SetAbilityPanelVisible(true);
 
    }


    public IEnumerator AnimateText(string message)
    {
        encounterText.text = "";
        for(int currentCharacter = 0; currentCharacter < message.Length; currentCharacter++ )
        {
            encounterText.text += message[currentCharacter];

            yield return new WaitForSeconds(timeBetweenCharacters);
        }
        animateTextReference = null;
        onTextAnimationDone.Invoke();
        yield return null;
    }


    public void DisplayText(string message)
    {
        animateTextReference = AnimateText(message);
        StartCoroutine(animateTextReference);

    }

    public void disableAbilityPanel()
    {
        abilityPanel.SetActive(false);
    }

    public void SetAbilityPanelVisible(bool isVisible)
    {
        abilityPanel.SetActive(isVisible);
    }


    public bool IsAnimDone()
    {
        return animateTextReference == null;
    }

}
