using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class AudioController : MonoBehaviour
{
    #region Variables
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;
    #endregion

    #region Unity Methods
    void Start()
    {
        LoadPlayerPrefValue();
    }
    #endregion


    #region Audio Methods
    public void ChangeMusicVolume(float musicVol)
    {
        AudioManager.Instance.ChangeMusicVolume(musicVol);
    }

    public void ChangeSFXVolume(float sfxVol)
    {
        AudioManager.Instance.ChangeSFXVolume(sfxVol);
    }

    public void MuteSounds(string mixerGrp)
    {
        AudioManager.Instance.MuteSounds(mixerGrp);
        switch (mixerGrp)
        {
            case "Music":
                musicSlider.value = 0;
                break;
            case "SFX":
                sfxSlider.value = 0;
                break;
        }
    }

    public void UnmuteSounds(string mixerGrp)
    {
        AudioManager.Instance.UnmuteSounds(mixerGrp);
        switch (mixerGrp)
        {
            case "Music":
                musicSlider.value = 1f;
                break;
            case "SFX":
                sfxSlider.value = 1f;
                break;
        }
    }

    #endregion

    #region SFX Methods
    public void OnButtonHover()
    {
        AudioManager.Instance.PlaySFX("ButtonHover");
    }

    public void OnButtonClicked()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
    }
    #endregion

    #region Player Pref Saving & Loading
    public void SaveVolToPlayerPref(Slider volumeSlider)
    {
        float volumeVal = volumeSlider.value;
        PlayerPrefs.SetFloat(volumeSlider.name, volumeVal);
        LoadPlayerPrefValue();
    }

    private void LoadPlayerPrefValue()
    {
        float musicVal = PlayerPrefs.GetFloat(musicSlider.name);
        musicSlider.value = musicVal;

        float sfxVal = PlayerPrefs.GetFloat(sfxSlider.name);
        sfxSlider.value = sfxVal;
    }
    #endregion
}
