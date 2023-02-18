using UnityEngine.XR.Interaction.Toolkit;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-06
// Description	: XR Socket Interactor with a tag check. 
// Referenced from: https://www.youtube.com/watch?v=Q7TGTAgmHQ0&t=95s&ab_channel=VRwithAndrew
//---------------------------------------------------------------------------------

public class SocketWithTagCheck : XRSocketInteractor
{
	#region Variables
	//===================
	// Public Variables
	//===================
	public string targetTag = string.Empty;

	#endregion

	#region Own Methods
	// ===========================
	// Override
	// ===========================
	
	/// <summary>
	/// Determines if interactable is Hoverable
	/// </summary>
	/// <param name="interactable"></param>
	/// <returns>Returns true if interactable can be hovered</returns>
	public override bool CanHover(XRBaseInteractable interactable)
	{
		return base.CanHover(interactable) && MatchTargetTag(interactable);	
	}

	/// <summary>
	/// Determines if interactable is Selectable
	/// </summary>
	/// <param name="interactable"></param>
	/// <returns>Returns true if interactable can be selected</returns>
	public override bool CanSelect(XRBaseInteractable interactable)
	{
		return base.CanSelect(interactable) && MatchTargetTag(interactable);	
	}

	// ===========================
	// Private
	// ===========================

	/// <summary>
	/// Checks if gameobject matches targetTag
	/// </summary>
	/// <param name="interactable"></param>
	/// <returns>Returns true if interactable matches</returns>
	private bool MatchTargetTag(XRBaseInteractable interactable) => interactable.CompareTag(targetTag);

	#endregion

}
