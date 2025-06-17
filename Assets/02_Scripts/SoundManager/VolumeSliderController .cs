using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VolumeSliderController  : MonoBehaviour
{   
    [SerializeField] private Slider _slider;

    public void Start()
    {
        if(_slider == null)
        {
            _slider = GetComponent<Slider>();
        }
    }

    public void OnValueChangedBGM()
    {
        SoundManager.Instance.SetVolume(SoundType.BGM, _slider.value);
    }

    public void OnValueChangedSFX()
    {
        SoundManager.Instance.SetVolume(SoundType.SFX, _slider.value);
    }
}
