using UnityEngine;

public class EnemyProjectile : ProjectileHandler
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
        // Apply damage to the player
        PlayerController player = target.GetComponent<PlayerController>();
        if (player != null)
        {
            player.TakeDamage(damageAmount); // Apply damage to the player
        }

        // Destroy the projectile after hitting the target
        Destroy(gameObject);
    }
}
