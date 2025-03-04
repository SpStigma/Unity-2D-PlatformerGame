using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Slider volumeSlider;

    void Start()
    {
        float bgmValue, sfxValue, volumeValue;

        if (audioMixer.GetFloat("BGMVolume", out bgmValue))
        {
            bgmSlider.value = Mathf.Pow(10, bgmValue / 20);
        }

        if (audioMixer.GetFloat("SFXVolume", out sfxValue))
        {
            sfxSlider.value = Mathf.Pow(10, sfxValue / 20);
        }

        if(audioMixer.GetFloat("MasterVolume", out volumeValue))
        {
            volumeSlider.value = Mathf.Pow(10, volumeValue / 20);
        }

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        volumeSlider.onValueChanged.AddListener(SetVolumeAll);
    }

    public void SetBGMVolume(float value)
    {

        if (value <= 0.001f) 
        {
            audioMixer.SetFloat("BGMVolume", -80f);
        }
        else
        {
            float volume = Mathf.Log10(value) * 20;
            audioMixer.SetFloat("BGMVolume", volume);
        }
    }

    public void SetSFXVolume(float value)
    {
        if (value <= 0.001f) 
        {
            audioMixer.SetFloat("SFXVolume", -80f);
        }
        else
        {
            float volume = Mathf.Log10(value) * 20;
            audioMixer.SetFloat("SFXVolume", volume);
        }
    }

    public void SetVolumeAll(float value)
    {
        if(value <= 0.001f)
        {
            audioMixer.SetFloat("MasterVolume", -80f);
        }
        else
        {
            float volume = Mathf.Log10(value) * 20;
            audioMixer.SetFloat("MasterVolume", volume);
        }
    }
}