using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Rogue : Unit { 
    public float power = 20f; 
    public float energy = 0f; 
    public float maxEnergy = 100.0f; 
    public float energyIncreaseRate = 20f; 
    public ManaBar energyBar; 

    protected override void Start() { 
        base.Start(); 
        energyBar.SetMaxMana(maxEnergy); 
        energyBar.SetMana(energy); 
    } 

    protected override void Update() { 
        base.Update();
        energy = Mathf.Min(energy + energyIncreaseRate * Time.deltaTime, maxEnergy);
        energyBar.SetMana(energy);

        if (energy >= maxEnergy && InRange && HasTarget) { 
            PerformSpecialAttack(); 
        } 
    } 

    private void PerformSpecialAttack() { 
        closestEnemy.TakeDamage(10 + (2 * power), "pure"); 
        energy = 0; 
        energyBar.SetMana(energy); 
    } 
}