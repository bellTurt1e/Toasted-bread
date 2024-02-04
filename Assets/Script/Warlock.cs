using UnityEngine;
using UnityEngine.Analytics;

public class Warlock : Unit {
    public float spellPower = 20f;
    public float mana = 0f;
    public float maxMana = 100.0f;
    public float manaIncreaseRate = 5.0f;
    public ManaBar manaBar;
    
    protected override void Start() {
        base.Start();
        manaBar.SetMana(0f);
        manaBar.SetMaxMana(maxMana);
    }

    protected override void Update() {
        base.Update();
        mana = Mathf.Min(mana + manaIncreaseRate * Time.deltaTime, maxMana);
        manaBar.SetMana(mana);

        if (mana >= maxMana && inRange && hasTarget) {
            PerformSpecialAttack();
        }
    }

    private void PerformSpecialAttack(){
        getHealed(15f + (0.2f * spellPower));
        closestEnemy.TakeDamage(30f + (1.5f * spellPower), "mag");
        mana = 0;
        manaBar.SetMana(mana);
    }
}
