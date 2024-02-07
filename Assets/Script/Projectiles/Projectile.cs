using UnityEngine;

public class ProjectileBall : MonoBehaviour {
    public float speed = 20f;
    private Unit targetUnit;
    private float damage;
    private string damageType;

    // Initialize the projectile with its target, damage, and damage type
    public void Initialize(Unit target, float dmg, string type) {
        targetUnit = target;
        damage = dmg;
        damageType = type;
    }

    void Update() {
        if (targetUnit == null) {
            Destroy(gameObject); // Destroy the projectile if the target is gone
            return;
        }

        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetUnit.transform.position, speed * Time.deltaTime);

        // Check for "collision" with the target based on proximity
        if (Vector3.Distance(transform.position, targetUnit.transform.position) < 0.5f) // 0.5f is the proximity threshold
        {
            targetUnit.TakeDamage(damage, damageType); // Apply damage to the target
            Destroy(gameObject); // Destroy the projectile
        }
    }
}
