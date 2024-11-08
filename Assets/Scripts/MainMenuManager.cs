using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the main menu, including starting the game, quitting the application, and handling additional menu functionalities.
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    /// <summary>
    /// Starts the game by loading the first level.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1); // Assuming level one is at build index 1
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Displays the settings menu. Placeholder function for additional menu settings.
    /// </summary>
    public void OpenSettings()
    {
        // Logic to open settings can be added here
    }

    /// <summary>
    /// Handles displaying information about the game, such as credits.
    /// </summary>
    public void OpenCredits()
    {
        // Logic to open credits can be added here
    }

    /// <summary>
    /// Exits the current menu and returns to the first menu.
    /// </summary>
    public void BackToFirstMenu()
    {
        // Logic to return to the first menu can be added here
    }
}
