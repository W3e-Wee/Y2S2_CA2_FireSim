using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class PlayerCanvas : MonoBehaviour
{
    #region Variables
    //===================
    // Public Variables
    //===================
    [Header("Transition Settings")]
    [Range(0, 5)] public float transitionInTime;
    [Range(0, 5)] public float transitionOutTime;
    [Range(0, 5)] public float displayTime = 3f;

    //====================================
    // [SerializeField] Private Variables
    //====================================
    [Header("Canvas")]
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject clearCanvas;

    //===================
    // Private Variables
    //===================
    private CanvasGroup gameOverCanvasGroup;
    private CanvasGroup clearCanvasGroup;
    #endregion

    #region Unity Methods
    protected void Start()
    {
        // FadeCamera.Instance.FadeOutCanvas();
        gameOverCanvasGroup = gameOverCanvas.GetComponent<CanvasGroup>();

        gameOverCanvasGroup.blocksRaycasts = false;
    }
    #endregion

    #region Own Methods

    /// <summary>
    /// Displays game over canvas to player and restart the level
    /// </summary>
    /// <param name="currentLevel">The current loaded level</param>
    public void ShowGameOver(string currentLevel)
    {
        // get last loaded level
        string previousLoadedLevel = currentLevel;

        // start animation sequence
        var loseSq = LeanTween.sequence();

        loseSq
        .append(() =>
        {
            FadeCamera.Instance.FadeInCanvas();
        })
        .append(1f)
        .append(() =>
        {
            LeanTween.alphaCanvas(gameOverCanvasGroup, 1f, transitionInTime);
        })
        .append(displayTime)
        // restart level
        .append(() =>
        {
            LeanTween.alphaCanvas(gameOverCanvasGroup, 0f, transitionOutTime);
            GameManager.Instance.UnloadLevel(currentLevel);
        })
        .append(1f)
        .append(() =>
        {
            GameManager.Instance.LoadLevel(previousLoadedLevel);
        });
    }

    public void LoadNextLevel(string levelName)
    {
        // Save progress

        GameManager.Instance.LoadLevel(levelName);
    }
    
    public void ReturnBtn()
    {
        // Save progress

        GameManager.Instance.LoadLevel("Menu_Scene");
    }
    #endregion
}
