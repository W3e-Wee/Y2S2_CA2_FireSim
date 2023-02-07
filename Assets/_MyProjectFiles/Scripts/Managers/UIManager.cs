using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-04
// Description	: Script to manage UI in scene
//---------------------------------------------------------------------------------

public class UIManager : Singleton<UIManager>
{
    #region Variables
    //===================
    // Public Variables
    //===================

    //====================================
    // [SerializeField] Private Variables
    //====================================
    [SerializeField] private MainMenu mainMenu;
    //===================
    // Private Variables
    //===================
    #endregion

    #region Unity Methods
    private void Start()
    {
        // check if the GameState is RUNNING
        if(GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
        {
            return;
        }

        GameManager.Instance.StartGame();
        FadeCamera.Instance.FadeOutCanvas();
    }

    #endregion

    #region Own Methods
    public void SetMenuActive(bool isActive)
    {
        mainMenu.gameObject.SetActive(isActive);
    }
    #endregion

}