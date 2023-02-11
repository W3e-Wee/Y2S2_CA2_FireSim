using UnityEngine;
using UnityEngine.Audio;
using System;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-07
// Description	: script to manage gaame audio
//---------------------------------------------------------------------------------

public class AudioManager : Singleton<AudioManager>
{
    #region Variables
    //===================
    // Public Variables
    //===================
    [Header("Sounds")]
    public Sounds[] musicSounds;
    public Sounds[] sfxSounds;

    [Header("Audio Mixer")]
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;

    //===================
    // Private Variables
    //===================
    private const string MUSIC_PARAM = "MusicVol";
    private const string SFX_PARAM = "SFXVol";

    #endregion
    protected override void Awake()
    {
        base.Awake();

        SetMusicSounds();
        SetSFXSounds();
    }
    #region Own Methods
    private void SetMusicSounds()
    {
        foreach (Sounds music in musicSounds)
        {
            music.source = gameObject.AddComponent<AudioSource>();

            music.source.clip = music.clip;
            music.source.outputAudioMixerGroup = musicMixer;

            music.source.volume = music.volume;
            music.source.pitch = music.pitch;
            music.source.loop = music.loop;
            music.source.playOnAwake = music.playOnAwake;
        }
    }
    private void SetSFXSounds()
    {
        foreach (Sounds sfx in sfxSounds)
        {
            sfx.source = gameObject.AddComponent<AudioSource>();

            sfx.source.clip = sfx.clip;
            sfx.source.outputAudioMixerGroup = sfxMixer;

            sfx.source.volume = sfx.volume;
            sfx.source.pitch = sfx.pitch;
            sfx.source.loop = sfx.loop;
        }
    }

    public void PlayMusic(string name)
    {
        // finds sound with in Array with the same name
        Sounds m = Array.Find(musicSounds, sound => sound.name == name);

        // check if there is matching music
        if (m == null)
        {
            Debug.LogError("[AudioManager] - Music with " + m.name + " not found");
            return;
        }

        // when matching sound found
        // play
        m.source.Play();
    }

    public void StopMusic(string name)
    {
        // finds sound with in Array with the same name
        Sounds m = Array.Find(musicSounds, sound => sound.name == name);

        // check if there is matching music
        if (m == null)
        {
            Debug.LogError("[AudioManager] - Music with " + m.name + " not found");
            return;
        }

        // when matching sound found
        // play
        m.source.Stop();
    }

    public void PlaySFX(string name)
    {
        // finds sound with in Array with the same name
        Sounds sfx = Array.Find(sfxSounds, sound => sound.name == name);

        // check if there is matching SFX
        if (sfx == null)
        {
            Debug.LogError("[AudioManager] - SFX with " + sfx.name + " not found");
            return;
        }

        // when matching sound found
        sfx.source.PlayOneShot(sfx.source.clip);
    }

    public void MuteSounds(string mixerGrp)
    {
        switch (mixerGrp)
        {
            case "SFX":
                sfxMixer.audioMixer.SetFloat(SFX_PARAM, -80f);
                break;
            case "Music":
                musicMixer.audioMixer.SetFloat(MUSIC_PARAM, -80f);
                break;
            default:
                Debug.LogWarning("[AudioManager] - Unable to SetFloat() for " + mixerGrp);
                return;
        }
    }
    public void UnmuteSounds(string mixerGrp)
    {
        switch (mixerGrp)
        {
            case "SFX":
                sfxMixer.audioMixer.SetFloat(SFX_PARAM, 0f);
                break;
            case "Music":
                musicMixer.audioMixer.SetFloat(MUSIC_PARAM, 0f);
                break;
            default:
                Debug.LogWarning("[AudioManager] - Unable to SetFloat() for " + mixerGrp);
                return;
        }
    }

    public void ChangeMusicVolume(float sliderVal)
    {
        musicMixer.audioMixer.SetFloat(MUSIC_PARAM, Mathf.Log10(sliderVal) * 25);
        // save as PlayerPref
    }
    public void ChangeSFXVolume(float sliderVal)
    {
        sfxMixer.audioMixer.SetFloat(SFX_PARAM, Mathf.Log10(sliderVal) * 25);
        // save as PlayerPref
    }
    #endregion

}
