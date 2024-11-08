using UnityEngine;

/// <summary>
/// Adds the ability to shoot projectiles at a specified interval, with a sound effect when shooting.
/// Can be used by enemies or the player, with configurable shooting direction and target.
/// </summary>
public class ShootingAbility : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab; // Prefab for the projectile to shoot
    [SerializeField] private Transform firePoint; // The point from which the projectile is fired
    [SerializeField] private float minFireRate = 1.0f; // Minimum time interval between shots
    [SerializeField] private float maxFireRate = 5.0f; // Maximum time interval between shots
    [SerializeField] private Vector3 shootingDirection = Vector3.up; // Direction in which the projectile will be shot
    [SerializeField] private TargetTag targetTag = TargetTag.Enemy; // Tag of the objects to be targeted by the projectile
    [SerializeField] private AudioClip shootSound; // Sound effect that plays when shooting
    [SerializeField] private AudioSource audioSource; // AudioSource to play the shooting sound

    [SerializeField] private bool shootImmidietly = true;

    private float nextFireTime; // Time when the next shot can occur
    private float currentFireRate = 0f; // Current fire rate for this entity

    /// <summary>
    /// Initializes the shooting ability, setting the first delay before shooting.
    /// </summary>
    private void Start()
    {
        // Set the next firing time.
        // If 'shootImmediately' is true, set the next fire time to now (to shoot right away).
        // Otherwise, delay the first shot by a random interval between 'minFireRate' and 'maxFireRate'.    
        nextFireTime = shootImmidietly ? Time.time : Time.time + Random.Range(minFireRate, maxFireRate);
    }

    /// <summary>
    /// Updates the shooting mechanism each frame, allowing the entity to shoot at random intervals.
    /// </summary>
    private void Update()
    {
        HandleShooting();
    }

    /// <summary>
    /// Handles the shooting logic, instantiating projectiles at the fire point.
    /// </summary>
    private void HandleShooting()
    {
        if (Time.time > nextFireTime)
        {
            // Check if a shield is blocking the path before shooting (only for player)
            if (targetTag == TargetTag.Enemy && IsShieldBlocking())
            {
                return; // Do not shoot if a shield is blocking the way
            }

            Shoot();
            currentFireRate = Random.Range(minFireRate, maxFireRate);
            nextFireTime = Time.time + currentFireRate;
        }
    }

    /// <summary>
    /// Instantiates a projectile at the fire point and sets its direction.
    /// Plays a shooting sound if available.
    /// </summary>
    private void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            ProjectileHandler projectileHandler = projectile.GetComponent<ProjectileHandler>();
            if (projectileHandler != null)
            {
                projectileHandler.SetDirection(shootingDirection);
                projectileHandler.SetTargetTag(targetTag);
            }

            // Play the shooting sound if available
            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
        }
    }

    /// <summary>
    /// Checks if there is a shield blocking the player's shooting path.
    /// </summary>
    /// <returns>True if a shield is blocking the way; otherwise, false.</returns>
    private bool IsShieldBlocking()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, shootingDirection);
        if (hit.collider != null && hit.collider.CompareTag("Shield"))
        {
            // A shield is blocking the shooting path
            return true;
        }
        return false;
    }

    /// <summary>
    /// Enumeration for selectable target tags.
    /// </summary>
    public enum TargetTag
    {
        Enemy,
        Player
    }
}
