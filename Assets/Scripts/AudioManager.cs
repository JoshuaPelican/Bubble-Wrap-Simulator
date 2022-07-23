using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Simple Singleton
    public static AudioManager Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField] AudioMixer Mixer;
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource EffectsSource;

    float defaultEffectsVolume;
    float defaultEffectsPitch;

    private void Start()
    {
        defaultEffectsVolume = EffectsSource.volume;
        defaultEffectsPitch = EffectsSource.pitch;
    }

    public void PlayEffect(AudioClip effectClip, float volume = 1f)
    {
        EffectsSource.volume = defaultEffectsVolume;
        EffectsSource.pitch = defaultEffectsPitch;

        EffectsSource.PlayOneShot(effectClip, volume);
    }

    public void PlayEffect(AudioClip effectClip, float volume = 1f, float volumeRandom = 0, float pitchRandom = 0)
    {
        PlayEffect(effectClip, volume);

        EffectsSource.volume += Random.Range(-volumeRandom, volumeRandom);
        EffectsSource.pitch += Random.Range(-pitchRandom, pitchRandom);
    }

    public void ChangeMusic(AudioClip musicClip, float fadeDuration, float volume)
    {
        StartCoroutine(FadeMusic(musicClip, fadeDuration, volume));
    }

    IEnumerator FadeMusic(AudioClip musicClip, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;

        Mixer.GetFloat("MusicVolume", out currentVol);

        currentVol = Mathf.Pow(10, currentVol / 20);

        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration / 2)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, 0, currentTime / (duration / 2));
            Mixer.SetFloat("MusicVolume", Mathf.Log10(newVol) * 20);
            yield return null;
        }

        MusicSource.clip = musicClip;
        MusicSource.Play();

        currentTime = 0;
        currentVol = 0;

        while (currentTime < duration / 2)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / (duration / 2));
            Mixer.SetFloat("MusicVolume", Mathf.Log10(newVol) * 20);
            yield return null;
        }

        yield break;
    }
}
