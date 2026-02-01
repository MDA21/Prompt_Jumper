using TMPro;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI happinessText;

    public void UpdateHappiness(int value)
    {
        if (happinessText != null)
        {
            if (happinessText.text != value.ToString())
            {
                 happinessText.text = value.ToString();
                 if (AudioManager.Instance != null)
                 {
                     AudioManager.Instance.Play("ui_update");
                 }
            }
        }
    }
}
