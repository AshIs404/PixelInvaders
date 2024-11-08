using UnityEngine;

/// <summary>
/// Controls the display of player lives, updating the visibility of UI elements based on the current number of lives.
/// </summary>
public class LivesController : MonoBehaviour
{
    /// <summary>
    /// Updates the visibility of each life UI element based on the player's current lives.
    /// </summary>
    /// <param name="newValue">The current number of lives.</param>
    /// <param name="maxLives">The maximum number of lives.</param>
    public void UpdateLivesDisplay(int newValue, int maxLives)
    {
        for (int i = 0; i < maxLives; i++)
        {
            // Activate or deactivate each life image based on currentLives
            transform.GetChild(i).gameObject.SetActive(i < newValue);
        }
    }
}
