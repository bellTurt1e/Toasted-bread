using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
}

public abstract class UnitBase : MonoBehaviour, IDamageable
{
    public float health = 100f;
    public float attackPower = 10f;
    public float attackTimer = 0;
    public float attackCooldown = 1.0f;
    public float defense = 5f;
    public float movementSpeed = 5f;
    public float attackRange = 1.0f;
    public int teamId;
    public bool hasTarget = false;
    public HealthBar healthBar;
    private UnitBase closestEnemy;
    protected virtual void Start()
    {
        healthBar.SetMaxHealth((int)health);
    }

    protected virtual void Update()
    {
        if (!hasTarget) {
            closestEnemy = FindClosestEnemy();
        }
        if (closestEnemy != null) {
            MoveTowards(closestEnemy);
        } else {
            hasTarget = false;
        }

        if (attackTimer > 0) {
            attackTimer -= Time.deltaTime;
        }
        healthBar.SetHealth((int)health);
    }

    UnitBase FindClosestEnemy() {
        UnitBase[] allUnitBases = FindObjectsOfType<UnitBase>();
        UnitBase closestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (UnitBase UnitBase in allUnitBases) {
            if (UnitBase.teamId != this.teamId) {
                float distance = Vector3.Distance(UnitBase.transform.position, currentPosition);
                if (distance < minDistance) {
                    closestEnemy = UnitBase;
                    minDistance = distance;
                }
            }
        }

        return closestEnemy;
    }

    void MoveTowards(UnitBase enemy){
        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        if (distance > attackRange) {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, movementSpeed * Time.deltaTime);
        }
        else {
            Attack(enemy);
            hasTarget = true;
        }
    }

    public virtual void Attack(UnitBase target)
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= attackRange && attackTimer <= 0f)
        {
            target.TakeDamage(attackPower);
            attackTimer = attackCooldown;
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage - defense;
        if (health <= 0)
        {
            KillUnit();
        }
    }

    protected virtual void KillUnit()
    {
        Destroy(gameObject);
    }
}
