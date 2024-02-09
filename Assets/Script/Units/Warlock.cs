using System.Collections;
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

        if (mana >= maxMana && InRange && HasTarget) {
            PerformSpecialAttack();
        }
    }

    private void PerformSpecialAttack(){
        float curseDuration = 9f;
        StartCoroutine(ApplyCurse(closestEnemy, (8f + (spellPower * 0.3f)), (15f + (spellPower *0.2f)), curseDuration));
        StartCoroutine(HealSelf((5f + (spellPower * 0.1f)), curseDuration));
        mana = 0;
        manaBar.SetMana(mana);
    }

    IEnumerator ApplyCurse(Unit target, float damagePerSecond, float attackPowerLowered, float totalDuration) {
        if (target == null) yield break;

        float timePassed = 0f;
        float halfDuration = totalDuration / 3f;
        bool attackPowerReduced = false;
        float originalBasicAttack = target.BasicAttack;

        while (timePassed < totalDuration) {
            if (target == null) {
                if (attackPowerReduced) {
                    target.BasicAttack = originalBasicAttack;
                }
                yield break;
            }

            target.TakeDamage(damagePerSecond, "mag");

            if (timePassed < halfDuration && !attackPowerReduced) {
                target.BasicAttack -= attackPowerLowered;
                attackPowerReduced = true;
            }

            yield return new WaitForSeconds(1f);
            timePassed += 1f;

            if (timePassed >= halfDuration && attackPowerReduced) {
                target.BasicAttack = originalBasicAttack;
                attackPowerReduced = false;
            }
        }

        if (attackPowerReduced) {
            target.BasicAttack = originalBasicAttack;
        }
    }

    IEnumerator HealSelf(float totalHealAmount, float duration = 9f) {
        float timePassed = 0f;
        float healInterval = 1f;
        float healPerTick = totalHealAmount / duration * healInterval;

        while (timePassed < duration) {
            if (this == null || gameObject == null) {
                yield break;
            }

            getHealed(healPerTick);
            yield return new WaitForSeconds(healInterval);
            timePassed += healInterval;
        }
    }

}
