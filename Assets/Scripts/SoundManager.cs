using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    public AudioLibrary audioLibrary;
    private void Awake()
    {
        // Singleton bất tử xuyên Scene
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }
    //// Phát nhạc nền (Lặp lại)
    //public void PlayMusic(AudioClip clip)
    //{
    //    if (clip == null) return;

    //    musicSource.clip = clip;
    //    musicSource.loop = true;
    //    musicSource.Play();
    //}

    // Phát tiếng động (Bắn súng, nhấn nút...)
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        // PlayOneShot giúp phát nhiều âm thanh chồng lên nhau mà không ngắt thằng trước
        sfxSource.PlayOneShot(clip, volume);
    }
    public void PlayShotEffect()
    {
        if (audioLibrary == null) return;
        PlaySFX(audioLibrary.audioDataList[1].AudioClip, 1);
    }
    public void PlaySelectEffect()
    {
        if (audioLibrary == null) return;
        PlaySFX(audioLibrary.audioDataList[0].AudioClip, 1);
    }
    //0 : ClickSound
    //1 : ShotSound
}