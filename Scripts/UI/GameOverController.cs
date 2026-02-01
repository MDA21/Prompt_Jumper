using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("The name of the level scene to restart (e.g. Level1)")]
    public string levelSceneName = "Level1";
    
    [Tooltip("The name of the main menu scene")]
    public string mainMenuSceneName = "MainMenu";

    public void RestartGame()
    {
        // Restart the level
        SceneManager.LoadScene(levelSceneName);
        
        // Optional: click sound
        if (AudioManager.Instance != null)
        {
            // AudioManager.Instance.Play("UIClick"); 
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
        
        // Optional: click sound
        if (AudioManager.Instance != null)
        {
            // AudioManager.Instance.Play("UIClick"); 
        }
    }

    public void ReturnToTrainCort()
    {
        SceneManager.LoadScene("TrainCourt");
        
        // Optional: click sound
        if (AudioManager.Instance != null)
        {
            // AudioManager.Instance.Play("UIClick"); 
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
