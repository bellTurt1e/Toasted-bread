using System;
using UnityEngine;

public class Unit : MonoBehaviour {
    public int health = 100;
    public int maxHealth;
    public float basicAttack = 10f;
    public float attackTimer = 0;
    public float attackCooldown = 1.0f;
    public float magicalDefense = 5f;
    public float physicalDefense = 5f;
    public float movementSpeed = 25f;
    public float attackRange = 1.0f;
    public int teamId;
    public int enemyTeamId;

    public HealthBar healthBar;
    protected Unit closestEnemy;
    public GameObject projectilePrefab;

    protected bool hasTarget = false;
    protected bool inRange = false;

    public delegate void UnitDeathHandler(Unit unit);
    public static event UnitDeathHandler OnUnitDeath;

    protected virtual void Start() {
        maxHealth = health;
        healthBar.SetMaxHealth(maxHealth);
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
            if (unit.teamId != this.teamId && unit.teamId == this.enemyTeamId) {
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

    protected virtual bool Attack(Unit target) {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= attackRange && attackTimer <= 0f) {
            GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            ProjectileBall projectile = projectileObject.GetComponent<ProjectileBall>();

            if (projectile != null) {
                projectile.Initialize(target, basicAttack, "phy");
            }

            attackTimer = attackCooldown;
            return true;
        }

        return false;
    }

    public void TakeDamage(float damage, string damageType) {
        float effectiveDamage = damage;

        switch (damageType) {
            case "mag":
                float magBlockPercent = CalculateBlockPercentage(magicalDefense);
                effectiveDamage = damage * (1f - magBlockPercent / 100f);
                break;
            case "phy":
                float phyBlockPercent = CalculateBlockPercentage(physicalDefense);
                effectiveDamage = damage * (1f - phyBlockPercent / 100f);
                break;
            case "both":
                float combinedBlockPercent = (CalculateBlockPercentage(magicalDefense) + CalculateBlockPercentage(physicalDefense)) / 2f;
                effectiveDamage = damage * (1f - combinedBlockPercent / 100f);
                break;
            case "pure":
                // Pure damage bypasses all defense
                break;
        }

        health -= (int)Mathf.Round(effectiveDamage);

        healthBar.SetHealth((int)health);

        if (health <= 0) {
            KillUnit();
        }
    }

    private float CalculateBlockPercentage(float defense) {
        float defenseScalingFactor = 10f;
        float maxBlockPercent = 75f;

        float blockPercent = maxBlockPercent * (1f - Mathf.Exp(-defense / defenseScalingFactor));
        return blockPercent;
    }

    public void getHealed(float healAmount) {
        health = (int)Mathf.Min(health + healAmount, maxHealth);
        healthBar.SetHealth((int)health);
    }

    protected virtual void KillUnit() {
        OnUnitDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
