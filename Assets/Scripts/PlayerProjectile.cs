using UnityEngine;

public class PlayerProjectile : ProjectileHandler
{
    [Header("Projectile Settings")]
    [SerializeField] private int damageAmount = 1; // Amount of damage the projectile deals on hit

    /// <summary>
    /// Called when a target is successfully hit by the projectile.
    /// Handles logic such as destroying the target or applying damage.
    /// </summary>
    /// <param name="target">The GameObject that was hit by the projectile.</param>
    public override void OnTargetHit(GameObject target)
    {
        // Apply damage to the enemy
        EnemyController enemy = target.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damageAmount); // Apply damage to the enemy
        }

        // Destroy the projectile after hitting the target
        Destroy(gameObject);
    }
}
