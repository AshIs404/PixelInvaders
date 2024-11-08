using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages core gameplay events such as scoring, lives, game over, level completion, and game pause. Implements a singleton pattern.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of GameManager.
    /// </summary>
    public static GameManager Instance { get; private set; }

    private const string PLAYER_SCORE_KEY = "PlayerScore";

    [Header("Managers")]
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private LivesController livesController;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOverUIPanel;         // Reference to the Game Over UI panel, set in the inspector
    [SerializeField] private GameObject levelCompletedUIPanel;   // Reference to the Level Completed UI panel, set in the inspector
    [SerializeField] private GameObject gamePausedUIPanel;       // Reference to the Game Paused UI panel, set in the inspector

    private bool isGamePaused = false;

    private int playerScore = 0; // Player's current score
    private int maxLives = 3;    // Maximum player lives
    private int currentLives;    // Player's current lives

    /// <summary>
    /// Ensures only one instance of GameManager exists, applying the singleton pattern.
    /// Destroys duplicates and persists the instance across scenes.
    /// </summary>
    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist through scene loads

        // Assuming the player starts with full lives
        currentLives = maxLives;
    }

    private void Start(){
        LoadScore();
        scoreController.UpdateScoreUI(playerScore);
    }

    /// <summary>
    /// Increases the player's score by a specified amount and updates the UI.
    /// </summary>
    /// <param name="amount">The amount by which to increase the score.</param>
    public void IncreaseScore(int amount)
    {
        playerScore += amount;
        if (scoreController != null)
        {
            scoreController.UpdateScoreUI(playerScore);
        }
    }

    /// <summary>
    /// Resets the player's score to zero.
    /// </summary>
    public void ResetScore()
    {
        playerScore = 0; // Reset score if needed (e.g., at the start of a new game)
        SaveScore();
    }

    /// <summary>
    /// Saves the player's score to PlayerPrefs.
    /// </summary>
    public void SaveScore()
    {
        PlayerPrefs.SetInt(PLAYER_SCORE_KEY, playerScore);
        PlayerPrefs.Save(); // Save PlayerPrefs to disk
    }

    /// <summary>
    /// Loads the player's score from PlayerPrefs.
    /// </summary>
    public void LoadScore()
    {
        playerScore = PlayerPrefs.GetInt(PLAYER_SCORE_KEY, 0); // Default to 0 if no score is saved
    }

    /// <summary>
    /// Deletes the player's score from PlayerPrefs.
    /// </summary>
    public void DeleteScore()
    {
        PlayerPrefs.DeleteKey(PLAYER_SCORE_KEY);
    }

    /// <summary>
    /// Updates the player's lives by a specified amount and checks for game over.
    /// </summary>
    /// <param name="deltaLives">The amount to change lives by (positive to gain, negative to lose).</param>
    public void UpdateLives(int deltaLives)
    {
        currentLives = Mathf.Clamp(currentLives + deltaLives, 0, maxLives);
        if (livesController != null)
        {
            livesController.UpdateLivesDisplay(currentLives, maxLives);
        }

        // Check for death condition
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Handles the game over state by displaying the game over screen and stopping gameplay.
    /// </summary>
    public void GameOver()
    {
        Time.timeScale = 0f; // Pauses the game
        ResetScore();
        if (gameOverUIPanel != null)
        {
            gameOverUIPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Handles the level completed state by displaying the level completed screen.
    /// </summary>
    public void LevelCompleted()
    {
        Time.timeScale = 0f; // Pauses the game
        if (levelCompletedUIPanel != null)
        {
            levelCompletedUIPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Pauses the game and displays the pause menu.
    /// </summary>
    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Pauses the game
        if (gamePausedUIPanel != null)
        {
            gamePausedUIPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Resumes the game from a paused state.
    /// </summary>
    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f; // Resumes the game
        if (gamePausedUIPanel != null)
        {
            gamePausedUIPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Restarts the current level.
    /// </summary>
    public void RestartGame()
    {
        ResetScore();
        Time.timeScale = 1f; // Reset time scale before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Quits to the main menu by loading the Main Menu scene.
    /// </summary>
    public void QuitToMainMenu()
    {
        ResetScore();
        Time.timeScale = 1f; // Reset time scale before quitting
        SceneManager.LoadScene(0); // Assuming main menu is at build index 0
    }

    /// <summary>
    /// Loads the next level when the current level is completed.
    /// </summary>
    public void ContinueToNextLevel()
    {
        SaveScore();
        Time.timeScale = 1f; // Reset time scale before loading the next level
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1); // Loads the next scene in the build index
    }
}
