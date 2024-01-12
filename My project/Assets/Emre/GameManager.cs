using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        // Load the scene for the game
        SceneManager.LoadScene("towerdefenseprototype");
    }

    public void QuitGame()
    {
        // Quit the application (works in build, not in the Unity Editor)
        Application.Quit();
    }
}
