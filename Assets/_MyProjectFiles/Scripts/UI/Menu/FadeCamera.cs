using UnityEngine;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-15
// Description	: Sript to fade in and out camera
//---------------------------------------------------------------------------------

public class FadeCamera : Singleton<FadeCamera>
{
    #region Variables
    //====================================
    // Public 
    //====================================
    [Header("Transition Timing")]
    [Range(0, 5)] public float fadeInTime;
    [Range(0, 5)] public float fadeOutTime;

    //====================================
    // [SerializeField] Private
    //====================================
    [Space]
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    #endregion

    #region Unity Methods

    #endregion

    #region Own Methods

    /// <summary>
    /// Fade's in the canvas via LeanTween sequence
    /// </summary>
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


    /// <summary>
    /// Fade's out the canvas via LeanTween sequence
    /// </summary>
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

    /// <summary>
    /// Called when canvas is faded in
    /// </summary>
    private void OnFadeInComplete()
    {

    }

    /// <summary>
    /// Called when canvas is faded out
    /// </summary>
    private void OnFadeOutComplete()
    {

    }
    #endregion

}
