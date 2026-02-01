using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congratulation : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player reached the goal!");
            SceneManager.LoadScene("Congratulations");
        }
    }
}
