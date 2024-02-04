using UnityEngine;

public class Warrior : UnitBase
{
    public float rage = 0f;
    public float maxRage = 100.0f;
    public float rageIncreaseRate = 5.0f;
    public ManaBar manaBar;

    protected override void Start()
    {
        base.Start();
        manaBar.SetMana(0f);
    }
    public override void Attack(UnitBase target){
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= attackRange && attackTimer <= 0f)
        {
            target.TakeDamage(attackPower);
            attackTimer = attackCooldown;
            rage += rageIncreaseRate;
        }
        manaBar.SetMana(rage);
    }
    protected override void Update()
    {
        base.Update();
        //IncreaseRageOverTime();
    }

    protected override void KillUnit()
    {
        // Add specific kill logic for Warrior
        base.KillUnit();
    }

    private void IncreaseRageOverTime()
    {
        rage += rageIncreaseRate * Time.deltaTime;
        rage = Mathf.Clamp(rage, 0f, maxRage);
        manaBar.SetMana(rage);
    }
}