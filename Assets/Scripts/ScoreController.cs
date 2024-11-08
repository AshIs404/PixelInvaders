using UnityEngine;
using TMPro;

/// <summary>
/// Controls the player's score display in the UI, updating the score text when the score changes.
/// </summary>
public class ScoreController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText; // UI Text for displaying the score

    /// <summary>
    /// Initializes the score display with a value of 0.
    /// </summary>
    private void Start()
    {
        UpdateScoreUI(0);
    }

    /// <summary>
    /// Updates the score UI with the new score value.
    /// </summary>
    /// <param name="newValue">The new score value to be displayed.</param>
    public void UpdateScoreUI(int newValue)
    {
        if (scoreText != null)
        {
            scoreText.text = $"{newValue}";
        }
    }
}
