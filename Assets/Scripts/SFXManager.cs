using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : Singleton<SFXManager>
{
    [SerializeField] private Slider _volumeSFXSlider;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonSwitchSFX;
    [SerializeField] private AudioClip _clickColorSFX;
    private void Start()
    {
        _volumeSFXSlider.value = 0.125F;
        _audioSource.volume = 0.125F;
    }

    public void OnChangeVolumeSFXSlider(System.Single value)
    {
        _audioSource.volume = value;
    }


    public void OnClickSwitchSFX()
    {
        _audioSource.PlayOneShot(_buttonSwitchSFX);
    }

    public void OnClickColorSFX()
    {
        _audioSource.PlayOneShot(_clickColorSFX);
    }

}
