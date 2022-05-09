using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundsController : MonoBehaviour
{
    public static SoundsController Instance;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _fireClip;
    [SerializeField] private AudioClip _checkPointClip;
    [SerializeField] private AudioClip _victoryClip;
    [SerializeField] private AudioClip _loseClip;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(Sound sound)
    {
        switch(sound)
        {
            case Sound.Fire: _audioSource.clip = _fireClip; _audioSource.Play();
                break;
            case Sound.CheckPoint: _audioSource.clip = _checkPointClip; _audioSource.Play();
                break;
            case Sound.Victory: _audioSource.clip = _victoryClip; _audioSource.Play();
                break;
            case Sound.Lose: _audioSource.clip = _loseClip; _audioSource.Play();
                break;
        }
    }
}

public enum Sound
{
    Fire,
    CheckPoint,
    Victory,
    Lose
}
