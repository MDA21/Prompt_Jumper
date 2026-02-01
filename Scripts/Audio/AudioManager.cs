using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundGroup
{
    public string name;
    public AudioClip[] clips;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.1f, 3f)] public float pitch = 1f;
    public bool loop = false;
    public bool isMusic = false;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sound Settings")]
    public SoundGroup[] sounds;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource musicSource;

    private Dictionary<string, SoundGroup> soundDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeSounds();
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        // Automatically try to play music matching the scene name
        if (soundDict != null && soundDict.ContainsKey(scene.name))
        {
            Play(scene.name);
        }
    }

    private void InitializeSounds()
    {
        soundDict = new Dictionary<string, SoundGroup>();
        foreach (var s in sounds)
        {
            if (!soundDict.ContainsKey(s.name))
            {
                soundDict.Add(s.name, s);
            }
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }

        // Try to play music for the initial scene
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (soundDict.ContainsKey(currentSceneName))
        {
            Play(currentSceneName);
        }
    }

    public void Play(string name, float volumeScale = 1f, float pitchScale = 1f)
    {
        if (soundDict == null) return;
        
        if (soundDict.ContainsKey(name))
        {
            SoundGroup group = soundDict[name];
            if (group.clips == null || group.clips.Length == 0)
            {
                Debug.LogWarning($"SoundGroup {name} has no clips assigned!");
                return;
            }

            // Randomly select a clip
            AudioClip clip = group.clips[Random.Range(0, group.clips.Length)];

            if (group.isMusic)
            {
                if (musicSource.clip != clip)
                {
                    musicSource.clip = clip;
                    musicSource.volume = group.volume * volumeScale;
                    musicSource.pitch = group.pitch * pitchScale;
                    musicSource.Play();
                }
            }
            else
            {
                // For SFX, we can use PlayOneShot to allow overlapping sounds
                sfxSource.volume = group.volume * volumeScale;
                sfxSource.pitch = group.pitch * pitchScale;
                sfxSource.PlayOneShot(clip);
            }
        }
        else
        {
            Debug.LogWarning($"Sound: {name} not found in AudioManager!");
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}
