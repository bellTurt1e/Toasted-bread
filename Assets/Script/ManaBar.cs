using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;
    
    public void SetMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }
    public void SetMana(float mana)
    {
        slider.value = mana;
    }
}
