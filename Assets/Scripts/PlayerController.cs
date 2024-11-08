using UnityEngine;

/// <summary>
/// Controls the player's movement, shooting, and collision detection in the game.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5.0f; // Speed of the player movement

    [Header("Player Health Settings")]
    [SerializeField] private int maxLives = 3; // Maximum health for the player
    private int currentLives;

    private Camera mainCamera; // Cached reference to the main camera
    private bool canMoveRight = true; // Determines if the player can move right
    private bool canMoveLeft = true; // Determines if the player can move left

    /// <summary>
    /// Initializes player settings, including caching the main camera reference and setting initial health.
    /// </summary>
    private void Start()
    {
        mainCamera = Camera.main;
        currentLives = maxLives;
    }

    /// <summary>
    /// Handles physics-related calculations for player movement.
    /// </summary>
    private void FixedUpdate()
    {
        HandleMovement();
    }

    /// <summary>
    /// Moves the player based on input, either keyboard or touch, and respects movement boundaries.
    /// </summary>
    private void HandleMovement()
    {
        float horizontalInput = 0;

        // Handle keyboard input (useful for testing in the editor)
        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }
        // Handle touch input for mobile devices
        else if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
            horizontalInput = touchPosition.x > transform.position.x ? 1 : -1;
        }

        // Apply movement based on boundaries
        if ((horizontalInput > 0 && canMoveRight) || (horizontalInput < 0 && canMoveLeft))
        {
            Vector3 movement = new Vector3(horizontalInput, 0, 0) * speed * Time.fixedDeltaTime;
            transform.Translate(movement);
        }
    }

    /// <summary>
    /// Handles player damage and updates lives through the GameManager.
    /// </summary>
    /// <param name="damage">The amount of damage taken by the player.</param>
    public void TakeDamage(int damage)
    {
        GameManager.Instance.UpdateLives(-damage);
    }

    /// <summary>
    /// Detects collisions with enemies and triggers game over if an enemy touches the player.
    /// Also handles collisions with boundaries to stop movement in that direction.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.GameOver();
        }
        else if (collision.gameObject.CompareTag("Boundary"))
        {
            // Stop movement in the direction of the collision
            Vector2 contactPoint = collision.ClosestPoint(transform.position);
            if (contactPoint.x > transform.position.x)
            {
                canMoveRight = false;
            }
            else if (contactPoint.x < transform.position.x)
            {
                canMoveLeft = false;
            }
        }
    }

    /// <summary>
    /// Handles trigger exit with boundaries to allow movement again.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            // Allow movement again in the direction of the collision
            Vector2 contactPoint = collision.ClosestPoint(transform.position);
            if (contactPoint.x > transform.position.x)
            {
                canMoveRight = true;
            }
            else if (contactPoint.x < transform.position.x)
            {
                canMoveLeft = true;
            }
        }
    }
}
