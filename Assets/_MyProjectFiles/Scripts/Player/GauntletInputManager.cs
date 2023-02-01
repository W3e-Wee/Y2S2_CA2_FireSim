using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.XR.Interaction.Toolkit;
using System;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-06
// Description	: Script to manage input actions made using the gauntlet
//---------------------------------------------------------------------------------

public class GauntletInputManager : MonoBehaviour
{
    #region Variables
    //===================
    // Public Variables
    //===================
    public bool runeActive = false;
    public bool isEarthActive = false;
    public bool isWaterActive = false;
    //====================================
    // [SerializeField] Private Variables
    //====================================
    [SerializeField] private XRSocketInteractor socket;
    [SerializeField] private string targetTag;
    //===================
    // Private Variables
    //===================
    private PlayerInput pI;
    #endregion

    #region Unity Methods
    protected void Start()
    {
        // Find object in scene with tag "Socket" and get "SocketWithTagCheck" component
        socket = GameObject.FindGameObjectWithTag("Socket").GetComponent<SocketWithTagCheck>();

        // Find and get PlayerInput component
        pI = this.GetComponent<PlayerInput>();
    }
    #endregion

    #region Own Methods
	// ====================
	// Public
	// ====================

    /// <summary>
    /// Check for the scripts in gameObject attached to socket.
    /// Method to be put into "SocketWithTagCheck" component
    /// </summary>
    public void CheckSocket()
    {
        IXRSelectInteractable socketItem = socket.GetOldestInteractableSelected();
        
		try
        {
            // check for the rune scripts
            if (socketItem.transform.TryGetComponent(out WaterRune wR))
            {
                print("Water rune active");
                isWaterActive = true;
            } // End of WaterRune script check
            if (socketItem.transform.TryGetComponent(out EarthRune eR))
            {
                print("Earth rune active");
                isEarthActive = true;
            } // End of EarthRune script check
        }
        catch(Exception err)
        {
			switch(err)
			{
				case NullReferenceException:
					Debug.LogError("Null Reference: " + err.Message);
					break;
				default:
					print("Unexpected error: " + err.Message);
					break;
			}
        } // End of try...catch block

    } // End of CheckSocket method

	/// <summary>
	/// Sets active rune script to false
	/// </summary>
	public void ChangeState()
	{
		isEarthActive = false;
		isWaterActive = false;

	} // End of ChangeState method

	/// <summary>
	/// Checks if set PlayerInput is triggered
	/// Once triggered/performed, sets runeActive to true
	/// </summary>
	/// <param name="context"></param>
	public void onGauntletFire(InputAction.CallbackContext context)
	{
		if(context.started || context.interaction is HoldInteraction)
		{
			runeActive = true;
		} // End of context started OR HoldInteraction check
		if(context.performed)
		{
			runeActive = true;
		} // End of context performed check
		if(context.canceled)
		{
			runeActive = false;
		} // End of context cancelled check

	} // End of onGauntletFire method
    #endregion

}
