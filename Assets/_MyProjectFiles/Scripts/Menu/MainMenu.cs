using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-04
// Description	: Script to handle Main Menu animations
//---------------------------------------------------------------------------------

public class MainMenu : MonoBehaviour
{
    #region Variables
    //===================
    // Public
    //===================
    [Header("Transition Timing")]
    [Range(0, 5)] public float transitionInTime;
    [Range(0, 5)] public float transitionOutTime;

    //====================================
    // [SerializeField] Private
    //====================================
    [Header("Canvas Groups")]
    [SerializeField] private CanvasGroup menuCanvasGroup;
    [SerializeField] private CanvasGroup settingCanvasGroup;

    [Space]
    [SerializeField] private FadeCamera fadeCam;
    //===================
    // Private
    //===================
    #endregion

    #region Unity Methods
    protected void Awake()
    {

    }

    protected void Start()
    {

    }
    #endregion

    #region Own Methods

    #region Decal & Splash Methods
    public void ShowDecalAndMenu()
    {

    }
    public void HideDecalAndMenu()
    {

    }
    #endregion

    #region Menu Methods
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Debug.Log("Exiting Application...");
        Application.Quit();
    }

    public void ShowSetting()
    {
        var settingSq = LeanTween.sequence();

        settingSq.append(() =>
        {
            // hide menu
            LeanTween.alphaCanvas(menuCanvasGroup, 0f, transitionOutTime);
        })
        .append(() =>
        {
            // show setting
            LeanTween.alphaCanvas(settingCanvasGroup, 1f, transitionInTime);
        });
    }

    public void HideSetting()
    {
        var settingSq = LeanTween.sequence();

        settingSq.append(() =>
        {
            // hide setting
            LeanTween.alphaCanvas(settingCanvasGroup, 0f, transitionOutTime);

        })
        .append(() =>
        {
            // show menu
            LeanTween.alphaCanvas(menuCanvasGroup, 1f, transitionInTime);
        });
    }

    public void LoadNextLevel(string levelName)
    {
        var loadSq = LeanTween.sequence();

        loadSq
        .append(() =>
        {
            fadeCam.FadeInCanvas();
        })
        .append(2f)
        .append(() =>
        {
            GameManager.Instance.LoadLevel(levelName);
        })
        .append(2f)
        .append(() =>
        {
            fadeCam.FadeOutCanvas();
            UIManager.Instance.SetMenuActive(false);
        });
    }
    #endregion

    #endregion
}
