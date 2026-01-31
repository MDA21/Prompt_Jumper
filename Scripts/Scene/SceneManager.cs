using UnityEngine;

public static class SceneManager
{
    public static void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public static void LoadScene(int buildIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }

    public static void ReloadCurrent()
    {
        var s = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(s.name);
    }

    public static void LoadNext()
    {
        int idx = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(idx + 1);
    }
}

