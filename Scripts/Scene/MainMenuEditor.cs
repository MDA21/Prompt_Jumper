using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuEditor : MonoBehaviour
{
    void Update()
    {
        LoadLevel1();
    }

    void LoadLevel1()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
              UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        }
    }
}
