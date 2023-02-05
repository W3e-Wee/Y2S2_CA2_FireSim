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
    [SerializeField] private FadeCamera fadeCam;
    //===================
    // Private Variables
    //===================
    #endregion

    #region Unity Methods
    private void Start()
    {
        fadeCam.FadeOutCanvas();
    }

    #endregion

    #region Own Methods
    public void SetMenuActive(bool isActive)
    {
        mainMenu.gameObject.SetActive(isActive);
    }
    #endregion

}
