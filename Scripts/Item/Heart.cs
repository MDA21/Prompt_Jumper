using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hear : MonoBehaviour
{
    [SerializeField] private string music = "Ì‡Œ“∏÷√≈ª·…œ”∞ - ﬂ’ﬂ’‡Ωﬂ’‡Ωﬂ’.mp3";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Increase player's health
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Add(100); // Increase health by 1
            }
            // Play heart pickup sound
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.Play(music);
            }
            // Destroy the heart object
            Destroy(gameObject);
        }
    }
}
