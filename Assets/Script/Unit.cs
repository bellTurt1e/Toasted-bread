using System;
using UnityEngine;

public class Unit : MonoBehaviour {
    public float health = 100f;
    public float basicAttack = 10f;
    public float attackTimer = 0;
    public float attackCooldown = 1.0f;
    public float magicalDefense = 5f;
    public float physicalDefense = 5f;
    public float movementSpeed = 5f;
    public float attackRange = 1.0f;
    public int teamId;

    public HealthBar healthBar;
    protected Unit closestEnemy;

    public bool hasTarget = false;
    protected bool inRange = false;

    protected virtual void Start() {
        healthBar.SetMaxHealth((int)health);
    }

     protected virtual void Update() {
        
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
    }

    private Unit FindClosestEnemy() {
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

    private void MoveTowards(Unit enemy) {
        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        if (distance > attackRange) {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, movementSpeed * Time.deltaTime);
            inRange = false;
        }
        else {
            Attack(enemy);
            hasTarget = true;
            inRange = true;
        }
    }

    protected virtual void Attack(Unit target) {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= attackRange && attackTimer <= 0f) {
            target.TakeDamage(basicAttack, "phy");
            attackTimer = attackCooldown;
        }
    }

    public void TakeDamage(float damage, string damageType) {
        switch (damageType) {
            case "mag":
                health -= damage - magicalDefense;
                break;
            case "phy":
                health -= damage - physicalDefense;
                break;
            case "both":
                health -= damage - (physicalDefense + magicalDefense);
                break;
        }

        if (health <= 0) {
            KillUnit();
        }
    }

    protected virtual void KillUnit() {
        Destroy(gameObject);
    }
}
