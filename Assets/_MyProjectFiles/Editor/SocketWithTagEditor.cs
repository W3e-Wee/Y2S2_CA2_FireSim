using UnityEditor;
using UnityEditor.XR.Interaction.Toolkit;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-06
// Description	: Custom editor for XR Socket Interactor with tag check
// Referenced from: https://www.youtube.com/watch?v=Q7TGTAgmHQ0&t=95s&ab_channel=VRwithAndrew
//---------------------------------------------------------------------------------

[CustomEditor(typeof(SocketWithTagCheck))]
public class SocketWithTagEditor : XRSocketInteractorEditor
{
	#region Variables
	//===================
	// Private Variables
	//===================
	private SerializedProperty targetTag = null;
	#endregion
	
	#region Unity Methods
	protected override void OnEnable()
	{
		base.OnEnable();
		targetTag = serializedObject.FindProperty("targetTag");
	}
	#endregion

	#region Own Methods
	protected override void DrawProperties()
	{
		base.DrawProperties();

		// show target tag input field
		EditorGUILayout.PropertyField(targetTag);
	}
	#endregion

}
