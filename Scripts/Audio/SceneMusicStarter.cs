using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusicStarter : MonoBehaviour
{
    [Header("Music Settings")]
    [Tooltip("The name of the music SoundGroup to play defined in AudioManager")]
    public string musicName;
    
    [Tooltip("If true, any currently playing music will be stopped.")]
    public bool stopMusic = false;

    [Range(0f, 1f)] public float volumeScale = 1f;
    [Range(0.1f, 3f)] public float pitchScale = 1f;

    [Header("Delay Settings")]
    public float delay = 0f;

    private void Start()
    {
        if (delay > 0f)
        {
            StartCoroutine(PlayMusicDelayed());
        }
        else
        {
            PlayMusic();
        }
    }

    private void PlayMusic()
    {
        if (AudioManager.Instance == null) return;

        if (stopMusic)
        {
            AudioManager.Instance.StopMusic();
            return;
        }

        if (!string.IsNullOrEmpty(musicName))
        {
            AudioManager.Instance.Play(musicName, volumeScale, pitchScale);
        }
    }

    private IEnumerator PlayMusicDelayed()
    {
        yield return new WaitForSeconds(delay);
        PlayMusic();
    }
}
