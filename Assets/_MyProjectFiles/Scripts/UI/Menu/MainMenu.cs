using UnityEngine;
using UnityEngine.UI;
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
    [Range(1, 5)] public float transitionInTime;
    [Range(1, 5)] public float transitionOutTime;
    [Range(1, 5)] public float logoWaitTime;

    //====================================
    // [SerializeField] Private
    //====================================
    [Header("Canvas")]
    [SerializeField] private GameObject logoCanvas;
    [SerializeField] private GameObject mainMenuCanvas;

    [Header("Canvas Groups")]
    [SerializeField] private CanvasGroup menuCanvasGroup;
    [SerializeField] private CanvasGroup settingCanvasGroup;

    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingPanel;

    [Space]
    public string unloadLevelName;
    //===================
    // Private
    //===================
    private CanvasGroup mainMenuCanvasGroup;
    private CanvasGroup logoCanvasGroup;

    #endregion

    #region Unity Methods
    protected void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);

        mainMenuCanvasGroup = mainMenuCanvas.GetComponent<CanvasGroup>();
        logoCanvasGroup = logoCanvas.GetComponent<CanvasGroup>();

        mainMenuCanvasGroup.blocksRaycasts = false;
        logoCanvasGroup.blocksRaycasts = false;
        settingCanvasGroup.blocksRaycasts = false;
        menuCanvasGroup.blocksRaycasts = false;

        StartSequence();
    }
    #endregion

    #region Handler Methods
    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        // check if current state is RUNNING and prev state is PREGAME
        if (previousState == GameManager.GameState.PREGAME && currentState == GameManager.GameState.RUNNING)
        {
            return;
        }
    }

    #endregion

    #region Decal & Splash Methods
    private void StartSequence()
    {
        var startSq = LeanTween.sequence();

        startSq
        .append(2f)
        .append(() =>
        {
            // fade in logo
            LeanTween.alphaCanvas(logoCanvasGroup, 1f, transitionInTime);
            Debug.Log("Fade in logo");
        })
        .append(logoWaitTime)
        .append(() =>
        {
            // fade out logo
            LeanTween.alphaCanvas(logoCanvasGroup, 0f, transitionOutTime);
            Debug.Log("Fade out logo");
        })
        .append(1f)
        .append(() =>
        {
            // fade in decal and menu
            LeanTween.alphaCanvas(mainMenuCanvasGroup, 1f, transitionInTime);

            // Enable raycast blocking
            mainMenuCanvasGroup.blocksRaycasts = true;
            menuCanvasGroup.blocksRaycasts = true;

            // Disbale logo ans start the music
            logoCanvas.SetActive(false);
            AudioManager.Instance.PlayMusic("Menu Theme");
        });
    }

    #endregion

    #region Menu Methods
    public void LoadNextLevel()
    {
        // check for players last played level
        string levelName = " ";
        DataPersistenceManager.Instance.LoadGame();

        if (DataPersistenceManager.Instance.gameData != null)
        {
            levelName = DataPersistenceManager.Instance.gameData.currentLevel;
        }
        else
        {
            DataPersistenceManager.Instance.NewGame();
        }

        var loadSq = LeanTween.sequence();

        loadSq
        .append(() =>
        {
            LeanTween.alphaCanvas(mainMenuCanvasGroup, 0f, transitionOutTime);
            mainMenuCanvasGroup.blocksRaycasts = false;
        })
        .append(1f)
        .append(() =>
        {
            FadeCamera.Instance.FadeInCanvas();
            mainMenuCanvas.SetActive(false);
        })
        .append(2f)
        .append(() =>
        {
            // unload previous level and stop the music
            GameManager.Instance.UnloadLevel(unloadLevelName);
            AudioManager.Instance.StopMusic("Menu Theme");
        })
        .append(2f)
        .append(() =>
        {
            // load next level
            GameManager.Instance.LoadLevel(levelName);
        });
    }
    public void ShowSetting()
    {
        var settingSq = LeanTween.sequence();

        settingSq.append(() =>
        {
            // hide menu
            LeanTween.alphaCanvas(menuCanvasGroup, 0f, transitionOutTime);
            menuCanvasGroup.blocksRaycasts = false;
        })
        .append(() =>
        {
            ToggleMenuPanel(false);
            // show setting
            LeanTween.alphaCanvas(settingCanvasGroup, 1f, transitionInTime);
            settingCanvasGroup.blocksRaycasts = true;
        });
    }

    public void HideSetting()
    {
        var settingSq = LeanTween.sequence();

        settingSq.append(() =>
        {
            // hide setting
            LeanTween.alphaCanvas(settingCanvasGroup, 0f, transitionOutTime);
            ToggleMenuPanel(true);
            settingCanvasGroup.blocksRaycasts = false;

        })
        .append(() =>
        {
            // show menu
            LeanTween.alphaCanvas(menuCanvasGroup, 1f, transitionInTime);
            menuCanvasGroup.blocksRaycasts = true;
        });
    }

    public void ExitGame()
    {
        GameManager.Instance.QuitGame();
    }
    public void ToggleMenuPanel(bool isActive)
    {
        menuPanel.SetActive(isActive);
    }

    #endregion
}
