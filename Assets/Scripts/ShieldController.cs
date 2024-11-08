using UnityEngine;

/// <summary>
/// Controls the shield behavior, managing its health and visual feedback.
/// </summary>
public class ShieldController : MonoBehaviour
{
    [Header("Shield Settings")]
    [SerializeField] private int maxHealth = 3; // Maximum health of the shield
    private int currentHealth;

    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component for visual feedback

    /// <summary>
    /// Initializes the shield with full health and gets the SpriteRenderer component.
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("ShieldController: No SpriteRenderer found on the GameObject.");
        }
    }

    /// <summary>
    /// Handles the shield being hit, reducing health and updating visual appearance.
    /// </summary>
    public void TakeDamage()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateShieldVisual();

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the shield when health reaches zero
            }
        }
    }

    /// <summary>
    /// Updates the shield's visual appearance by reducing the opacity based on remaining health.
    /// </summary>
    private void UpdateShieldVisual()
    {
        if (spriteRenderer != null)
        {
            float alpha = (float)currentHealth / maxHealth; // Calculate new opacity based on remaining health
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            spriteRenderer.color = newColor;
        }
    }
}
