using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixer : MonoBehaviour
{
    private const string SoundEnabled = nameof(SoundEnabled);
    private const string MasterVolume = nameof(MasterVolume);
    private const string ButtonsVolume = nameof(ButtonsVolume);
    private const string BackgroundVolume = nameof(BackgroundVolume);
    private const float FixedSoundValue = 20f;

    [SerializeField] private AudioMixerGroup _mixerAudio;
    [SerializeField] private Toggle _toggleSound;
    [SerializeField] private Slider _sliderMasterVolume;
    [SerializeField] private Slider _sliderButtonsVolume;
    [SerializeField] private Slider _sliderBackgroundVolume;

    private void Start()
    {        
        _sliderMasterVolume.value = PlayerPrefs.GetFloat(MasterVolume, 1);        
        _sliderButtonsVolume.value = PlayerPrefs.GetFloat(ButtonsVolume, 1);
        _sliderBackgroundVolume.value = PlayerPrefs.GetFloat(BackgroundVolume, 1);        
        _sliderMasterVolume.interactable = _toggleSound.isOn = PlayerPrefs.GetInt(SoundEnabled, 1) == 1;        
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(SoundEnabled, _toggleSound.isOn ? 1 : 0);
        PlayerPrefs.SetFloat(MasterVolume, _sliderMasterVolume.value);
        PlayerPrefs.SetFloat(ButtonsVolume, _sliderButtonsVolume.value);
        PlayerPrefs.SetFloat(BackgroundVolume, _sliderBackgroundVolume.value);
    }

    public void ToogleSound(bool isEnabled)
    {        
        _mixerAudio.audioMixer.SetFloat(MasterVolume, isEnabled ? Mathf.Log10(_sliderMasterVolume.value) * FixedSoundValue : -80f);                
    }

    public void ChangeMasterVolume(float volume)
    {
        _mixerAudio.audioMixer.SetFloat(MasterVolume, Mathf.Log10(volume) * FixedSoundValue);        
    }

    public void ChangeButtonsVolume(float volume)
    {
        _mixerAudio.audioMixer.SetFloat(ButtonsVolume, Mathf.Log10(volume) * FixedSoundValue);        
    }

    public void ChangeBackgroundVolume(float volume)
    {
        _mixerAudio.audioMixer.SetFloat(BackgroundVolume, Mathf.Log10(volume) * FixedSoundValue);        
    }
}
