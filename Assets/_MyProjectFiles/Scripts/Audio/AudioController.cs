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
    // [Header("Sliders")]
    // public Slider musicSlider;
    // public Slider sfxSlider;
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
    }

    public void UnmuteSounds(string mixerGrp)
    {
        AudioManager.Instance.UnmuteSounds(mixerGrp);
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
}
