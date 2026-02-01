using TMPro;
using UnityEngine;

public class CongratulationsUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    private void Start()
    {
        if (GameManager.Instance != null && timeText != null)
        {
            float time = GameManager.Instance.GameTime;
            // Format time as mm:ss
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            
            timeText.text = "Time: " + formattedTime;
        }
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        if (AudioManager.Instance != null)
        {
            // AudioManager.Instance.Play("UIClick");
        }
    }
}
