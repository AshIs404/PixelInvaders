using UnityEngine;

/// <summary>
/// Controls enemy behavior, including health, destruction, and interaction with boundaries.
/// </summary>
public class EnemyController : MonoBehaviour
{
    [Header("Enemy Health Settings")]
    [SerializeField] private int maxHealth = 1; // Maximum health for the enemy
    private int currentHealth;

    [Header("Enemy Score Value Settings")]
    [SerializeField] private int enemyScoreValue = 100; // Score value awarded for destroying this enemy

    private EnemyGroupController groupController; // Reference to the enemy group controller

    /// <summary>
    /// Initializes enemy settings, including finding the group controller and setting initial health.
    /// </summary>
    private void Start()
    {
        groupController = GetComponentInParent<EnemyGroupController>();
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Handles collisions with boundaries and notifies the group controller.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            groupController.OnBoundaryHit();
        }
    }

    /// <summary>
    /// Inflicts damage to the enemy and checks if it should be destroyed.
    /// </summary>
    /// <param name="damage">The amount of damage taken.</param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles the destruction of the enemy, increases player score, and notifies the group controller.
    /// </summary>
    private void Die()
    {
        GameManager.Instance.IncreaseScore(enemyScoreValue); // Increase player score by enemy value
        Destroy(gameObject); // Destroy the enemy game object
        groupController.CheckIfGroupIsEmpty(); // Notify group controller to check if all enemies are destroyed
    }
}
