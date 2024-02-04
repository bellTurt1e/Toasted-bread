using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider slider;
    public TextMeshProUGUI unitNameText;

    void Start() {
        UpdateName();
    }

    void UpdateName() {
        unitNameText.text = transform.parent.parent.gameObject.name;
    }

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health) {
        slider.value = health;
    }

}
