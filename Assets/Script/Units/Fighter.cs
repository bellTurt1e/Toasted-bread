using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class Fighter : Unit {
    public float power = 20f;
    public float rage = 0f;
    public float maxRage = 100.0f;
    public float originalMaxRage;
    public float rageIncreaseRate = 5.0f;
    public ManaBar rageBar;
    private int specialAttackCounter = 0;
    private Vector3 originalScale;


    protected override void Start() {
        base.Start();
        rageBar.SetMaxMana(maxRage);
        rageBar.SetMana(rage);
        originalScale = transform.localScale;
        originalMaxRage = maxRage;
    }

    protected override void Update() {
        base.Update();
        
        if (rage >= maxRage && InRange && HasTarget) {
            PerformSpecialAttack();
        }
    }

    private void PerformSpecialAttack() {
        

        float scaleIncrease = 0.1f;
        transform.localScale = originalScale + originalScale * scaleIncrease * specialAttackCounter;

        int additionalDamage = 6 * specialAttackCounter;
        closestEnemy.TakeDamage(power + additionalDamage, "phy");
        specialAttackCounter++;
        
        if (maxRage > 10) {
            maxRage -= 5;
        }

        rage = 0;
        rageBar.SetMaxMana(maxRage);
        rageBar.SetMana(rage);
    }

    private void ResetSpecialAttack() {
        specialAttackCounter = 0;
        StartCoroutine(ShrinkOverTime(3f));
        maxRage = originalMaxRage;
        rageBar.SetMaxMana(originalMaxRage);
        rageBar.SetMana(rage);
    }

    protected override bool Attack(Unit target) {
        bool attackResult = base.Attack(target);
        if (attackResult) {
            rage += rageIncreaseRate;
            rageBar.SetMana(rage);
        }

        return attackResult;
    }

    protected void OnEnable() {
        Unit.OnUnitDeath += HandleUnitDeath;
    }

    protected void OnDisable() {
        Unit.OnUnitDeath -= HandleUnitDeath;
    }

    private void HandleUnitDeath(Unit unit) {
        if (unit == closestEnemy) {
            Debug.Log("The current Value of unitKilled is: true");
            ResetSpecialAttack();
        }
    }

    IEnumerator ShrinkOverTime(float duration) {
        Vector3 startScale = transform.localScale;
        float time = 0;

        while (time < duration) {
            transform.localScale = Vector3.Lerp(startScale, originalScale, time / duration);
            time += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        transform.localScale = originalScale; // Ensure the scale is set to the original scale at the end
    }
}
