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
            happinessText.text = value.ToString();
        }
    }
}
