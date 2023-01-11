using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-11
// Description	: Toggles between teleport rays and grab rays.
// Referenced from - https://www.youtube.com/watch?v=9dc1zq8eH54&ab_channel=VRwithAndrew
//---------------------------------------------------------------------------------

public class RayToggle : MonoBehaviour 
{
	#region Variables
	//====================================
	// [SerializeField] Private Variables
	//====================================
	[SerializeField] private InputActionReference activateReference = null;
	[SerializeField] private XRRayInteractor grabRayInteractor = null;

	//===================
	// Private Variables
	//===================
	private XRRayInteractor tpRayInteractor = null;
	private bool isEnabled = false;

	#endregion
	
	#region Unity Methods
	protected void Awake()
	{
		tpRayInteractor = this.GetComponent<XRRayInteractor>();
	}

	protected void OnEnable()
	{
		activateReference.action.started += ToggleRay;
		activateReference.action.canceled += ToggleRay;
	}
	#endregion

	#region Own Methods
	private void ToggleRay(InputAction.CallbackContext context)
	{
		isEnabled = context.control.IsPressed();
	}

	private void LateUpdate()
	{
		ApplyStatus();
	}

	private void ApplyStatus()
	{
		if(tpRayInteractor.enabled != isEnabled)
		{
			tpRayInteractor.enabled = isEnabled;
			grabRayInteractor.enabled = !isEnabled;
		}
	}
	#endregion

}
