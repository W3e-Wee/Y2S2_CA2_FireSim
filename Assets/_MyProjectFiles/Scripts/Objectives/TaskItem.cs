using UnityEngine.UI;
using TMPro;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

[System.Serializable]
public enum TaskType
{
	Fire,
	Repair,
	Debris,
	Enemy
}

[System.Serializable]
public class TaskItem
{
	public TaskType taskType;
	public Toggle checkBox;
	public TextMeshProUGUI objectiveCount;
	public TextMeshProUGUI totalObjectiveCount;
}
