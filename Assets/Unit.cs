using System;
using UnityEngine;

public class Unit : MonoBehaviour {
    public float health = 100f;
    public float attackPower = 10f;
    public float attackTimer = 0;
    public float attackCooldown = 1.0f;
    public float defense = 5f;
    public float movementSpeed = 5f;
    public float attackRange = 1.0f;
    public int teamId;
    public bool hasTarget = false;
    public HealthBar healthbar;

    public int currentHealth = 100;
    private Unit closestEnemy;

    void Start()
    {
        currentHealth = (int)health;
        healthbar.SetMAxHealth((int)health);
    }
    void Update() {
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
        healthbar.SetHealth((int)health);
    }

    Unit FindClosestEnemy() {
        Unit[] allUnits = FindObjectsOfType<Unit>();
        Unit closestEnemy = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Unit unit in allUnits) {
            if (unit.teamId != this.teamId) {
                float distance = Vector3.Distance(unit.transform.position, currentPosition);
                if (distance < minDistance) {
                    closestEnemy = unit;
                    minDistance = distance;
                }
            }
        }

        return closestEnemy;
    }

    void MoveTowards(Unit enemy) {
        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        if (distance > attackRange) {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, movementSpeed * Time.deltaTime);
        }
        else {
            Attack(enemy);
            hasTarget = true;
        }
    }

    public virtual void Attack(Unit target) {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= attackRange && attackTimer <= 0f) {
            target.TakeDamage(attackPower);
            attackTimer = attackCooldown;

        }
    }

    private void TakeDamage(float attackPower) {
        health -= attackPower - defense;
        if (health <= 0) {
            KillUnit();
        }
    }

    protected virtual void KillUnit() {
        Destroy(gameObject);
    }
}
