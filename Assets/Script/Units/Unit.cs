using System;
using UnityEngine;

public class Unit : MonoBehaviour {
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth;
    [SerializeField] private float basicAttack = 10f;
    [SerializeField] private float attackTimer = 0;
    [SerializeField] private float attackCooldown = 1.0f;
    [SerializeField] private float magicalDefense = 5f;
    [SerializeField] private float physicalDefense = 5f;
    [SerializeField] private float movementSpeed = 25f;
    [SerializeField] private float attackRange = 1.0f;
    [SerializeField] private int teamId;
    [SerializeField] private int enemyTeamId;
    [SerializeField] bool hasTarget = false;
    [SerializeField] bool inRange = false;
    public int Health { get => health; set => health = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float BasicAttack { get => basicAttack; set => basicAttack = value; }
    public float AttackTimer { get => attackTimer; set => attackTimer = value; }
    public float AttackCooldown { get => attackCooldown; set => attackCooldown = value; }
    public float MagicalDefense { get => magicalDefense; set => magicalDefense = value; }
    public float PhysicalDefense { get => physicalDefense; set => physicalDefense = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    protected bool HasTarget { get => hasTarget; set => hasTarget = value; }
    protected bool InRange { get => inRange; set => inRange = value; }
    public int TeamId { get => teamId; set => teamId = value; }
    public int EnemyTeamId { get => enemyTeamId; set => enemyTeamId = value; }

    public delegate void UnitDeathHandler(Unit unit);
    public static event UnitDeathHandler OnUnitDeath;

    public HealthBar healthBar;
    protected Unit closestEnemy;
    public GameObject projectilePrefab;

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
