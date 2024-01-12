using UnityEngine;
using UnityEngine.SceneManagement; // Add this line for scene management

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    public GameObject pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0; // Stop the game
        pauseMenu.SetActive(true); // Show the pause menu
    }

    public void ResumeGame()
    {
        Time.timeScale = 1; // Resume the game
        pauseMenu.SetActive(false); // Hide the pause menu
    }

    public void QuitGame()
    {
        // Implement any additional cleanup or save functionality before quitting
        Application.Quit();
    }
}
