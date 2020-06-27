using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void SetMaxStamina(float stamina)
    {
        _slider.maxValue = stamina;
        _slider.value = stamina;
    }

    public void SetStamina(float stamina)
    {
        _slider.value = stamina;
    }
}
