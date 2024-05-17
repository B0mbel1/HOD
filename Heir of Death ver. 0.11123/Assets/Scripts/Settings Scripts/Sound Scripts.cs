using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundScripts : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private GameObject menuMusicObject;
    [SerializeField] private List<Transform> CanvaChildren;

    [SerializeField] private Slider sfxSlider;

    public void Update()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
            if (menuMusicObject.GetComponent<AudioSource>().volume == 0)
            {
                menuMusicObject.GetComponent<AudioSource>().loop = false;
            }
        }
        else
        {
            SetMusicVolume();
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
            if (sfxSlider.value == 0)
            {
                TurnOffAudioSources(transform);
            }
            else
            {
                TurnOnAudioSources(transform);
            }
        }
        else
        {
            SetsfxVolume();
        }
    }

    public void TurnOffAudioSources(Transform parent)
    {
        foreach (Transform child in parent)
        {
            AudioSource audioSource = child.GetComponent<AudioSource>();

            if (audioSource != null)
            {
                audioSource.enabled = false;
            }

            TurnOffAudioSources(child);
        }
    }

    public void TurnOnAudioSources(Transform parent)
    {
        foreach (Transform child in parent)
        {
            AudioSource audioSource = child.GetComponent<AudioSource>();

            if (audioSource != null)
            {
                audioSource.enabled = true;
            }

            TurnOnAudioSources(child);
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetsfxVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        SetsfxVolume();
    }
}
