using UnityEngine;
using TMPro;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class TaskManager : MonoBehaviour
{
    #region Variables
    //===================
    // Public Variables
    //===================
    public List<TaskItem> taskList;

    //====================================
    // [SerializeField] Private Variables
    //====================================
    [Header("Timer Setting")]
    [SerializeField] private float timeLeft = 60f;
    [SerializeField] private bool timerOn;
    [SerializeField] private TextMeshProUGUI timeText;

    //===================
    // Private Variables
    //===================
    private LevelManager levelManager;

    #endregion

    #region Unity Methods
    protected void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager == null)
        {
            Debug.LogError("[TaskManager] - Level Manager not in scene");
            return;
        }

		PopulateTasks();
    }

    protected void Update()
    {
        // update timer
        if (timerOn)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimer(timeLeft);

            // if timer hits 0
            if (timeLeft <= 0)
            {
                timerOn = false;
            }
        }
    }
    #endregion

    #region Own Methods
    public void PopulateTasks()
    {
        foreach (TaskItem task in taskList)
        {
            task.checkBox.isOn = false;
            task.objectiveCount.text = 0.ToString();

            // Set totalObjectiveCount
            switch (task.taskType)
            {
                case TaskType.Fire:
                    task.totalObjectiveCount.text = levelManager.fireList.Count.ToString();
                    break;
                case TaskType.Repair:
                    task.totalObjectiveCount.text = levelManager.wallList.Count.ToString();
                    break;
                case TaskType.Debris:
                    task.totalObjectiveCount.text = levelManager.debrisList.Count.ToString();
                    break;
                case TaskType.Enemy:
                    task.totalObjectiveCount.text = levelManager.enemyList.Count.ToString();
                    break;
                default:
                    Debug.LogError("[LevelManager] - TaskType not found");
                    break;
            }
        }

        timerOn = true;
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
    #endregion

}
