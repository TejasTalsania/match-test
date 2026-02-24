using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private List<SoundSFX> allSounds;
    private AudioSource audioSource;

    private Dictionary<SoundType, AudioClip> _soundMap;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        _soundMap = new Dictionary<SoundType, AudioClip>();

        for (var i = 0; i < allSounds.Count; i++)
        {
            _soundMap[allSounds[i].type] = allSounds[i].clip;
        }
    }

    private void OnEnable()
    {
        GameEvents.OnSoundRequested += OnRequestSoundPlay;
    }

    private void OnDisable()
    {
        GameEvents.OnSoundRequested -= OnRequestSoundPlay;
    }

    private void OnRequestSoundPlay(SoundType type)
    {
        if (_soundMap.TryGetValue(type, out var clip))
        {
            audioSource.PlayOneShot(clip);
        }
    }
}

[System.Serializable]
public class SoundSFX
{
    public SoundType type;
    public AudioClip clip;
}

public enum SoundType
{
    ButtonClick,
    CardFlip,
    CardMatch,
    CardNoMatch,
    LevelComplete
}
