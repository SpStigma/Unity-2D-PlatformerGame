using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Clips")]
    public AudioClip[] bgmClips;
    public AudioClip[] sfxClips;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private Dictionary<string, int> sceneBGMMap = new Dictionary<string, int>();

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
            return;
        }

        bgmSource = transform.Find("BGM").GetComponent<AudioSource>();
        sfxSource = transform.Find("SFX").GetComponent<AudioSource>();

        sceneBGMMap.Add("Start", 0);
        sceneBGMMap.Add("HUB", 1);
        sceneBGMMap.Add("Game 01", 1);
        sceneBGMMap.Add("Game 02", 2);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneBGMMap.TryGetValue(scene.name, out int bgmIndex))
        {
            PlayBGM(bgmIndex);
        }
    }

    public void PlayBGM(int index)
    {
        if (index < 0 || index >= bgmClips.Length)
        {
            return;
        }

        AudioClip clip = bgmClips[index];
        if (bgmSource.clip == clip) 
        {
            return;
        }
        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void PlaySFX(int index)
    {
        if (index < 0 || index >= sfxClips.Length)
        {
            return;
        }

        sfxSource.pitch = Random.Range(0.9f, 1.1f);
        sfxSource.PlayOneShot(sfxClips[index]);
        sfxSource.pitch = 1.0f;
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void StopAllSounds()
    {
        bgmSource.Stop();
        sfxSource.Stop();
    }
}
