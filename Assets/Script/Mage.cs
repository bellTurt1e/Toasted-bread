using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;

public class Mage : Unit {
    public float spellPower = 20f;
    public float mana = 0f;
    public float maxMana = 100.0f;
    public float manaIncreaseRate = 5.0f;
    public ManaBar manaBar;


    protected override void Start() {
        base.Start();
        manaBar.SetMaxMana(maxMana);
        manaBar.SetMana(0f);
    }

    protected override void Update() {
        base.Update();
        mana = Mathf.Min(mana + manaIncreaseRate * Time.deltaTime, maxMana);
        manaBar.SetMana(mana);

        if (mana >= maxMana && inRange && hasTarget) {
            PerformSpecialAttack();
        }
    }

    private void PerformSpecialAttack() {
        //int rand = Random.Range(0, 2);
        int rand = 1;
        switch (rand) {
            case 0:
                closestEnemy.TakeDamage(50 * 1.75f, "mag"); // Deal 50 damage then slow the enemies attack speed for 3 seconds
                StartCoroutine(TempIncreaseCooldown(closestEnemy, 3f));
                break;
            case 1:
                StartCoroutine(ApplyBurn(closestEnemy, 15 * 1.4f, 8)); // Burn target for 15 damage per second over 8 seconds
                break;
            case 2:
                float damageDealt = 90 * 1.85f;
                closestEnemy.TakeDamage(damageDealt, "mag"); 
                StartCoroutine(ApplyHealingOverTime(closestEnemy, damageDealt * 1.5f, 12)); // Deal massive damage then heal the target for 1.5x the damage over 12 seconds
                break;
        }
        mana = 0;
        manaBar.SetMana(mana);
    }

    IEnumerator TempIncreaseCooldown(Unit enemy, float duration) {
        if (enemy == null || enemy.gameObject == null) {
            yield break;
        }

        float originalCooldown = enemy.attackCooldown;
        enemy.attackCooldown *= 2;
        yield return new WaitForSeconds(duration);

        if (enemy != null && enemy.gameObject != null) {
            enemy.attackCooldown = originalCooldown;
        }
    }


    IEnumerator ApplyBurn(Unit target, float damagePerSecond, float duration) {
        float timePassed = 0f;

        while (timePassed < duration) {
            if (target == null || target.gameObject == null) {
                yield break;
            }

            target.TakeDamage(damagePerSecond, "mag");
            yield return new WaitForSeconds(1f);
            timePassed += 1f;
        }
    }

    IEnumerator ApplyHealingOverTime(Unit target, float totalHealingAmount, float duration) {
        float healPerSecond = totalHealingAmount / duration;
        float timePassed = 0f;

        while (timePassed < duration) {
            if (target == null || target.gameObject == null) {
                yield break;
            }

            target.getHealed(healPerSecond);
            yield return new WaitForSeconds(1f);
            timePassed += 1f;
        }
    }


}
