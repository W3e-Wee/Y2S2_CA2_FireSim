using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-08
// Description	: Script to manage level logic
//---------------------------------------------------------------------------------
// Responsible for:
// 1. Spawning required prefabs
// 2. Keep track of win/lose
// 3. Interact with other managers (i.e. UIManager, GameManager)
//---------------------------------------------------------------------------------
public class LevelManager : MonoBehaviour
{
    #region Variables
    public string currentLevelName;
    private PlayerCanvas playerCanvas;
    #endregion

    #region Unity Methods
    protected void Start()
    {
        CheckGameStateChanged();
        playerCanvas = FindObjectOfType<PlayerCanvas>();
    }
    #endregion
    #region Own Methods
    private void CheckGameStateChanged()
    {
        // get current level name
        currentLevelName = SceneManager.GetActiveScene().name;

        // check to see if game is in a RUNNING state
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            FadeCamera.Instance.FadeOutCanvas();
            UIManager.Instance.SetMenuActive(false);

            switch (currentLevelName)
            {
                case "MainLevel_01_Scene":
                    AudioManager.Instance.PlayMusic("Level 1 Theme");
                    break;
                case "MainLevel_02_Scene":
                    break;
                default:
                    Debug.LogError("[LevelManager] - Audio for " + currentLevelName + " not found");
                    break;
            }

            return;
        }
    }
    
    private void ToggleGameOver()
    {
        playerCanvas.ShowGameOver(currentLevelName);
    }
    #endregion
}
