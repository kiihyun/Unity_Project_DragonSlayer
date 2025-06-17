using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    BGM,
    SFX
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField][Range(0, 1)] private float _soundEffectVolume;
    [SerializeField][Range(0, 1)] private float _soundEffectPitchVariance;
    [SerializeField][Range(0, 1)] private float _musicVolume;

    [SerializeField] private AudioClip[] _musicClips;

    private Dictionary<string, AudioClip> _audioDictionary;

    private AudioSource _musicAudioSource;
    public SoundSource SoundSourcePrefab;
    
    protected override void Awake()
    {
        base.Awake();

        _musicAudioSource = GetComponent<AudioSource>();
        _musicAudioSource.volume = _musicVolume;
        _musicAudioSource.loop = true;
    }

    void Start()
    {
        InitAudioDictionary();
    }

    private void InitAudioDictionary()
    {
        _audioDictionary = new Dictionary<string, AudioClip>();

        foreach(var clip in _musicClips)
        {
            _audioDictionary.Add(clip.name, clip);
        }
    }

    public void SetVolume(SoundType type, float volume)
    {
        if(type == SoundType.BGM)
        {
            _musicVolume = volume;
            _musicAudioSource.volume = _musicVolume;
        }
        else if(type == SoundType.SFX)
        {
            _soundEffectVolume = volume;
        }
    }

    public void PlayBGM(string clipName, bool isLoop = true)
    {
        _musicAudioSource.Stop();
        _musicAudioSource.clip = _audioDictionary[clipName];
        _musicAudioSource.loop = isLoop;
        _musicAudioSource.Play();
    }
    public void StopBGM()
    {
        _musicAudioSource.Stop();
    }
    public void PlaySFX(string clipName)
    {
        if(_audioDictionary.ContainsKey(clipName))
        {
            SoundSource soundSource = Instantiate(SoundSourcePrefab, transform);
            soundSource.Play(_audioDictionary[clipName], _soundEffectVolume);
        }
        else
        {
            Debug.LogError("SoundManager: PlaySFX - 존재하지 않는 오디오 클립입니다.");
        }
    }

 
}
