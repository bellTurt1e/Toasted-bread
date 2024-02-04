using UnityEngine;
using UnityEngine.Analytics;

public class Mage : Unit {
    public float spellPower = 20f;
    public float mana = 0f;
    public float maxMana = 100.0f;  // Set your desired maximum mana value
    public float manaIncreaseRate = 5.0f;
    public ManaBar manaBar;


    protected override void Start()
    {
        base.Start();
        manaBar.SetMana(0f);
    }

    protected override void Update()
    {
        base.Update();
        // Increase mana over time
        mana += manaIncreaseRate * Time.deltaTime;

        // Ensure mana doesn't exceed the maximum value
        mana = Mathf.Clamp(mana, 0f, maxMana);

        //upadtes the mana bar
        manaBar.SetMana(mana);
    }
    protected override void Attack(Unit target){
    // Additional mage-specific methods and properties can be added here
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= attackRange && attackTimer <= 0f) {
            target.TakeDamage(basicAttack, "phy");
            attackTimer = attackCooldown;
            mana += manaIncreaseRate;
        }
        manaBar.SetMana(mana);
    }
}
