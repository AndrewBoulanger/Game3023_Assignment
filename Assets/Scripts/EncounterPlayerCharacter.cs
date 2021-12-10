using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EncounterPlayerCharacter : ICharacter
{
    [SerializeField]
    List<AbilityButton> abilityButtons;

    public HealthBar healthbar;
    private void Start()
    {

        abilityButtons[0].GetComponent<Button>().onClick.AddListener(OnButton0Pressed);
        abilityButtons[1].GetComponent<Button>().onClick.AddListener(OnButton1Pressed);
        abilityButtons[2].GetComponent<Button>().onClick.AddListener(OnButton2Pressed);
        abilityButtons[3].GetComponent<Button>().onClick.AddListener(OnButton3Pressed);

        for(int i = 0; i < abilityButtons.Count; i++)
        {
            if(i < abilities.Length)
                abilityButtons[i].SetButtonText(abilities[i].name);
            else
                abilityButtons[i].gameObject.SetActive(false);
        }
        healthbar.SetMaxHealth(stats.MaxHealth);
    }


    void OnButton0Pressed() => UseAbility(0);
    void OnButton1Pressed() => UseAbility(1);
    void OnButton2Pressed() => UseAbility(2);
    void OnButton3Pressed() => UseAbility(3);


    public override void TakeTurn()
    {

    }
    public void Update()
    {
        healthbar.SetHealth(stats.CurrentHealth);
    }
}
