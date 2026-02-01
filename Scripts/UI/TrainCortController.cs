using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCortController : MonoBehaviour
{
    public string targetSceneName = "Level1";
    public void ExitTrainCort()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
