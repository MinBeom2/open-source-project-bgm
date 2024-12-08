using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----------Audio Source-------------")]
    [SerializeField] public AudioSource master;
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    [Header("-----------Audio clip-------------")]
    public AudioClip Book;
    public AudioClip buttonClick;
    public AudioClip DoorOpen;
    public AudioClip normaldoor;
    public AudioClip gameover;
    public AudioClip creature;
    public AudioClip lockeddoor;
    public AudioClip piercing;
    [Header("-----------Background Music-------------")]
    public AudioClip mainBackground;
    public AudioClip DefaultBackground;


    [Header("-----------Volume Levels-------------")]
    [Range(0f, 1f)] public float normalVolume = 1f;
    [Range(0f, 1f)] public float pauseVolume = 0.2f;

    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }
}