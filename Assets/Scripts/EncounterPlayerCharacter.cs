using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EncounterPlayerCharacter : ICharacter
{
    [SerializeField]
    public List<AbilityButton> abilityButtons;

    public HealthBar healthbar;
    protected override void Start()
    {
        base.Start();
        abilityButtons[0].GetComponent<Button>().onClick.AddListener( OnButtonPressed);
        abilityButtons[1].GetComponent<Button>().onClick.AddListener( () => UseAbility(1));
        abilityButtons[2].GetComponent<Button>().onClick.AddListener( () => UseAbility(2));
        abilityButtons[3].GetComponent<Button>().onClick.AddListener( () => UseAbility(3));

        for(int i = 0; i < abilityButtons.Count; i++)
        {
            if(i < abilities.Length)
                abilityButtons[i].SetButtonText(abilities[i].name);
            else
                abilityButtons[i].gameObject.SetActive(false);
        }
        healthbar.SetMaxHealth(stats.MaxHealth);
    }

    void OnButtonPressed()
    {
        UseAbility(0);
    }

    public override void TakeTurn()
    {

    }
    public void Update()
    {
        healthbar.SetHealth(currentHealth);
    }

    private void OnDisable()
    {
        stats.CurrentHealth = currentHealth;
    }
}
