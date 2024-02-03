using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Fighter : Unit {
    public float Power = 20f;
    public float rage = 0f;
    public float maxrage = 100.0f;  // Set your desired maximum mana value
    public float rageIncreaseRate = 5.0f;
    public ManaBar rageBar;
    

    void Start()
    {
        rageBar.SetMana(0f);
    }

    protected override void Update()
    {
        base.Update();
        
    }
    public override void Attack(Unit target){
    // Additional mage-specific methods and properties can be added here
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= attackRange && attackTimer <= 0f) {
            target.TakeDamage(attackPower);
            attackTimer = attackCooldown;
            rage += rageIncreaseRate;
        }
        rageBar.SetMana(rage);
    }
}
