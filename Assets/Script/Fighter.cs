using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Fighter : Unit {
    public float power = 20f;
    public float rage = 0f;
    public float maxRage = 100.0f;
    public float rageIncreaseRate = 5.0f;
    public ManaBar rageBar;
    

    protected override void Start()
    {
        base.Start();
        rageBar.SetMaxMana(maxRage);
        rageBar.SetMana(rage);
    }

    protected override void Update()
    {
        base.Update();
        if (rage >= maxRage && inRange) {
            PerformSpecialAttack();
        }
    }

    private void PerformSpecialAttack() {
        closestEnemy.TakeDamage(20+(2*power), "phy");
        rage = 0;
        rageBar.SetMana(rage);
    }

    protected override void Attack(Unit target) {
        float targetHP = target.health;
        base.Attack(target);
        float targetNewHP = target.health;

        if (targetNewHP < targetHP) {
            rage += rageIncreaseRate;
            rageBar.SetMana(rage);
        }
    }
}
