using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        if(_volumeSlider != null) _volumeSlider.value = 0.125F;
        _audioSource.volume = 0.125F;
    }

    public void OnChangeVolumeSlider(System.Single value)
    {
        _audioSource.volume = value;
    }
}
