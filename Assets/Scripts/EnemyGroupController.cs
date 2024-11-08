using UnityEngine;

/// <summary>
/// Controls the movement and management of the entire enemy group, including boundary detection and level completion logic.
/// </summary>
public class EnemyGroupController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2.0f; // Speed at which the enemies move
    [SerializeField] private float dropDistance = 0.5f; // Distance enemies move down after hitting a boundary
    [SerializeField] private int cooldownFrames = 5; // Frames to wait before allowing another boundary hit

    private bool movingRight = true; // Track the direction of enemy movement
    private int cooldownCounter = 0; // Frame counter to avoid multiple calls in quick succession

    /// <summary>
    /// Updates the enemy group's movement and manages cooldown for boundary hits.
    /// </summary>
    private void FixedUpdate()
    {
        HandleMovement();

        // Decrease cooldown counter if it's greater than zero
        if (cooldownCounter > 0)
        {
            cooldownCounter--;
        }
    }

    /// <summary>
    /// Handles the horizontal movement of the enemy group.
    /// </summary>
    private void HandleMovement()
    {
        float movement = moveSpeed * Time.fixedDeltaTime * (movingRight ? 1 : -1);
        transform.Translate(movement, 0, 0);
    }

    /// <summary>
    /// Handles the event when the enemy group hits a boundary, switching direction and moving down.
    /// </summary>
    public void OnBoundaryHit()
    {
        // Avoid multiple boundary hit calls within a short time span
        if (cooldownCounter == 0)
        {
            movingRight = !movingRight;
            MoveDown();

            // Set cooldown counter to prevent multiple calls in quick succession
            cooldownCounter = cooldownFrames;
        }
    }

    /// <summary>
    /// Moves the enemy group down by the specified drop distance.
    /// </summary>
    private void MoveDown()
    {
        transform.position += new Vector3(0, -dropDistance, 0);
    }

    /// <summary>
    /// Checks if the enemy group is empty and triggers level completion if no enemies remain.
    /// </summary>
    public void CheckIfGroupIsEmpty()
    {
        if (transform.childCount <= 1) // If no enemies remain (child count is zero or only this script object remains)
        {
            EndGame();
        }
    }

    /// <summary>
    /// Ends the game by notifying the GameManager that the level is completed.
    /// </summary>
    private void EndGame()
    {
        GameManager.Instance.LevelCompleted();
    }
}
