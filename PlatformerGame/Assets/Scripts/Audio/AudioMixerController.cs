using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Slider volumeSlider;
    public Button applyButton;
    public Button cancelButton;

    private float initialBGM, initialSFX, initialMaster;

    void Start()
    {
        audioMixer.GetFloat("BGMVolume", out initialBGM);
        audioMixer.GetFloat("SFXVolume", out initialSFX);
        audioMixer.GetFloat("MasterVolume", out initialMaster);

        bgmSlider.value = Mathf.Pow(10, initialBGM / 20);
        sfxSlider.value = Mathf.Pow(10, initialSFX / 20);
        volumeSlider.value = Mathf.Pow(10, initialMaster / 20);

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        volumeSlider.onValueChanged.AddListener(SetMasterVolume);

        applyButton.onClick.AddListener(ApplyVolumeChanges);
        cancelButton.onClick.AddListener(CancelVolumeChanges);
    }

    void SetBGMVolume(float value) => SetVolume("BGMVolume", value);
    void SetSFXVolume(float value) => SetVolume("SFXVolume", value);
    void SetMasterVolume(float value) => SetVolume("MasterVolume", value);

    void ApplyVolumeChanges()
    {
        audioMixer.GetFloat("BGMVolume", out initialBGM);
        audioMixer.GetFloat("SFXVolume", out initialSFX);
        audioMixer.GetFloat("MasterVolume", out initialMaster);
    }

    void CancelVolumeChanges()
    {
        SetVolume("BGMVolume", Mathf.Pow(10, initialBGM / 20));
        SetVolume("SFXVolume", Mathf.Pow(10, initialSFX / 20));
        SetVolume("MasterVolume", Mathf.Pow(10, initialMaster / 20));

        bgmSlider.value = Mathf.Pow(10, initialBGM / 20);
        sfxSlider.value = Mathf.Pow(10, initialSFX / 20);
        volumeSlider.value = Mathf.Pow(10, initialMaster / 20);
    }

    void SetVolume(string parameter, float value)
    {
        if (value <= 0.001f)
            audioMixer.SetFloat(parameter, -80f);
        else
            audioMixer.SetFloat(parameter, Mathf.Log10(value) * 20);
    }
}
