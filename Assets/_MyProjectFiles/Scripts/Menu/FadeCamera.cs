using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class FadeCamera : MonoBehaviour
{
    #region Variables
    //====================================
    // [SerializeField] Private Variables
    //====================================
    [Header("Transition Timing")]
    [Range(0, 5)] public float fadeInTime;
    [Range(0, 5)] public float fadeOutTime;

    [Space]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    //===================
    // Private Variables
    //===================

    #endregion

    #region Unity Methods

    #endregion

    #region Own Methods
    public void FadeInCanvas()
    {
        var fadeInSq = LeanTween.sequence();

        fadeInSq
        .append(1f)
        .append(() =>
        {
            LeanTween.alphaCanvas(fadeCanvasGroup, 1f, fadeInTime);
        })
        .append(() =>
        {
            OnFadeInComplete();
        });
    }

    public void FadeOutCanvas()
    {
        var fadeOutSq = LeanTween.sequence();

        fadeOutSq
        .append(1f)
        .append(() =>
        {
            LeanTween.alphaCanvas(fadeCanvasGroup, 0f, fadeOutTime);
        })
        .append(() =>
        {
            OnFadeOutComplete();
        });

    }

    private void OnFadeInComplete()
    {
        Debug.Log("FADE IN COMPLETE");
    }

    private void OnFadeOutComplete()
    {
        Debug.Log("FADE OUT COMPLETE");
    }
    #endregion

}
