using UnityEngine;

/// <summary>
/// Abstract class for handling projectile behavior, including movement, lifetime, and collision detection.
/// </summary>
public abstract class ProjectileHandler : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] protected float speed = 10.0f; // Speed of the projectile
    [SerializeField] private float maxLifetime = 5.0f; // Maximum lifetime of the projectile to prevent memory leaks
    private Vector3 direction = Vector3.up; // Direction of the projectile movement
    private ShootingAbility.TargetTag targetTag = ShootingAbility.TargetTag.Enemy; // Tag of the objects that this projectile can hit

    /// <summary>
    /// Initializes the projectile by setting its destruction after the maximum lifetime.
    /// </summary>
    protected virtual void Start()
    {
        // Destroy the projectile after maxLifetime to avoid memory issues
        Destroy(gameObject, maxLifetime);
    }

    /// <summary>
    /// Sets the direction in which the projectile will move.
    /// </summary>
    /// <param name="newDirection">The new direction for the projectile.</param>
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    /// <summary>
    /// Sets the target tag for the projectile.
    /// </summary>
    /// <param name="newTargetTag">The new target tag for the projectile.</param>
    public void SetTargetTag(ShootingAbility.TargetTag newTargetTag)
    {
        if (newTargetTag == ShootingAbility.TargetTag.Enemy || newTargetTag == ShootingAbility.TargetTag.Player)
        {
            targetTag = newTargetTag;
        }
    }


    /// <summary>
    /// Moves the projectile in the specified direction.
    /// </summary>
    protected virtual void MoveProjectile()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    
    /// <summary>
    /// Updates the projectile's position each frame by calling the movement function.
    /// </summary>
    protected virtual void Update()
    {
        MoveProjectile();
    }

    /// <summary>
    /// Handles collision detection and destroys the projectile when it hits a boundary or a valid target.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy the projectile on impact with boundaries
        if (collision.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
        // Destroy the projectile on impact with the target
        else if (collision.CompareTag(targetTag.ToString()))
        {
            Destroy(gameObject);
            
            OnTargetHit(collision.gameObject);
        }
        // Destroy shield on impact if the projectile is from an enemy
        else if (collision.CompareTag("Shield") && targetTag == ShootingAbility.TargetTag.Player)
        {
            Destroy(gameObject);

            ShieldController shield = collision.GetComponent<ShieldController>();
            if (shield != null)
            {
                shield.TakeDamage(); // Call the TakeDamage function on the shield
            }
        }
    }

    /// <summary>
    /// Called when a target is successfully hit by a projectile.
    /// Handles any logic that should occur when the target is hit, such as reducing health or triggering effects.
    /// </summary>
    /// <param name="target">The GameObject that was hit by the projectile.</param>
    public abstract void OnTargetHit(GameObject target);
}
