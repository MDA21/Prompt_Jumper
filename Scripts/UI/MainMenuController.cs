using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("The name of the scene to load when Start is pressed")]
    public string targetSceneName = "Level1";

    public void StartGame()
    {
        // Use our custom SceneManager wrapper
        SceneManager.LoadScene(targetSceneName);
        
        // Optional: Play a click sound if AudioManager exists
        if (AudioManager.Instance != null)
        {
            // You can define a generic UI click sound in AudioManager with name "UIClick"
            // AudioManager.Instance.Play("UIClick"); 
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
