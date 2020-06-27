using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _damageText;
    
    public float MaxHealth = 100f;
    public float CurrentHealth;
    public float CurrentStamina;
    public float PlayerDamage = 40f;
    public float MaxStamina = 100f;

    public bool wasHealed = false;

  

    public void TakeHeal(float healingUnits, float staminaUnits, float damageUnits)
    {
        if (wasHealed == false)
        {
            CurrentHealth += healingUnits;
            wasHealed = true;
            if (CurrentHealth >= MaxHealth) CurrentHealth = MaxHealth;
        }
        wasHealed = false;

        if (wasHealed == false)
        {
            CurrentStamina += staminaUnits;
            wasHealed = true;
            if (CurrentStamina >= MaxStamina) CurrentHealth = MaxStamina;
        }
        wasHealed = false;

        PlayerDamage += damageUnits;
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
        CurrentStamina = MaxStamina;
        _damageText.text = PlayerDamage.ToString();
    }

    
    private void Update()
    {
        _damageText.text = PlayerDamage.ToString();
    }
}
