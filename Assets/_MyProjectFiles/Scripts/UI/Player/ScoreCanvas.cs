using UnityEngine;
using TMPro;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class ScoreCanvas : MonoBehaviour
{
    #region Variables
    //===================
    // Public Variables
    //===================
    [SerializeField] private GameObject clearCanvas;
    [Header("Points Distribution")]
    [SerializeField] private float pointsPerTask = 1f;
    [SerializeField] private float timePoints = 1f;
    //====================================
    // [SerializeField] Private Variables
    //====================================
    [Header("Points UI")]
    [SerializeField] private TextMeshProUGUI taskPointsText;
    [SerializeField] private TextMeshProUGUI timePointsText;
    [SerializeField] private TextMeshProUGUI totalPointsText;

    [Header("Grades")]
    [SerializeField] private List<Grades> gradeList;
    [SerializeField] private TextMeshProUGUI gradeText;

    //===================
    // Private Variables
    //===================
    private LevelManager levelManager;
    private CanvasGroup clearCanvasGroup;
    #endregion

    #region Unity Methods
    protected void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        clearCanvasGroup = clearCanvas.GetComponent<CanvasGroup>();

        clearCanvasGroup.blocksRaycasts = false;
        clearCanvasGroup.alpha = 0f;
    }
    #endregion

    #region Own Methods
    public void ShowClear()
    {
        print("SHOW CLEAR");
        var winSq = LeanTween.sequence();
        TabulateScore();

        winSq
        .append(() =>
        {
            clearCanvasGroup.blocksRaycasts = true;
            LeanTween.alphaCanvas(clearCanvasGroup, 1f, 1f);
        });
    }

    public void LoadNextLevel(string levelName)
    {
        levelManager.LoadNextLevel(levelName);
    }

    private void TabulateScore()
    {
        float taskScore = (levelManager.taskList.Count * pointsPerTask);
        taskPointsText.text = "+" + taskScore.ToString();

        float timeScore = Mathf.RoundToInt(levelManager.timeLeft * timePoints);
        timePointsText.text = "+" + timeScore.ToString();

        // total points = time points + task points
        float totalScore = taskScore + timeScore;
        totalPointsText.text = "+" + totalScore.ToString();

        ComputeGrade(totalScore);
    }

    private void ComputeGrade(float score)
    {
        string finalGrade = string.Empty;
        foreach (Grades g in gradeList)
        {
            if (score >= g.pointsToAchieveGrade)
            {
                finalGrade = g.grade;
                print("FINAL GRADE: " + finalGrade);
                break;
            }
        }

        gradeText.text = finalGrade;
    }
    #endregion
}
