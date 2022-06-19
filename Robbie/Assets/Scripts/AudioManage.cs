using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManage : MonoBehaviour
{
    private static AudioManage current;

    [Header("环境声音")] 
    public AudioClip ambientClip;
    public AudioClip musicClip;

    [Header("FX音效")] 
    public AudioClip DeathFXClip;
    public AudioClip orbFXClip;

    [Header("Robbie音效")] 
    public AudioClip[] walkStepClips;
    public AudioClip[] crouchStepClips;
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public AudioClip jumpVoiceClip;
    public AudioClip deathVoiceClip;
    public AudioClip orbVoiceClip;

    private AudioSource ambientSource;
    private AudioSource musicSource;
    private AudioSource fxSource;
    private AudioSource playerSource;
    private AudioSource voiceSource;

    private void Awake()
    {
        current = this;
        DontDestroyOnLoad(gameObject);

        ambientSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        fxSource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();
        
        StartPlayAudio();
    }

    void StartPlayAudio()
    {
        ambientSource.clip = ambientClip;
        ambientSource.loop = true;
        ambientSource.Play();
        
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public static void PlayFootStepAudio()
    {
        int index = Random.Range(0, current.walkStepClips.Length);

        current.playerSource.clip = current.walkStepClips[index];
        current.playerSource.Play();
    }
    
    public static void PlayCrouchFootStepAudio()
    {
        int index = Random.Range(0, current.crouchStepClips.Length);

        current.playerSource.clip = current.crouchStepClips[index];
        current.playerSource.Play();
    }

    public static void PlayJumpAudio()
    {
        current.playerSource.clip = current.jumpClip;
        current.playerSource.Play();
        
        current.voiceSource.clip = current.jumpVoiceClip;
        current.voiceSource.Play();
    }

    public static void PlayDeathAudio()
    {
        current.playerSource.clip = current.deathClip;
        current.playerSource.Play();
        
        current.voiceSource.clip = current.deathVoiceClip;
        current.voiceSource.Play();

        current.fxSource.clip = current.DeathFXClip;
        current.fxSource.Play();
    }

    public static void PlayOrbAudio()
    {
        current.voiceSource.clip = current.orbVoiceClip;
        current.voiceSource.Play();
        
        current.fxSource.clip = current.orbFXClip;
        current.fxSource.Play();
    }
}
