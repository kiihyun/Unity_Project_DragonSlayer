using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public class SoundSource : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioSource AudioSource => _audioSource;

    public void Play(AudioClip audioClip, float soundEffectVolume)
    {
        if(_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }

        CancelInvoke();
        _audioSource.clip = audioClip;
        _audioSource.volume = soundEffectVolume;
        _audioSource.Play();
        
        Invoke("Disable", audioClip.length + 2f);
    }
    
    public void Disable()
    {
        _audioSource.Stop();
        Destroy(gameObject);
    }
    
}
