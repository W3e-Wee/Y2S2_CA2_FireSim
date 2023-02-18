using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using UnityEngine;
using TMPro;

//---------------------------------------------------------------------------------
// Author		: Lim Wee Heng
// Date  		: 2022-12-01
// Description	: Write testing result to Excel file in Output folder.
//---------------------------------------------------------------------------------
public class ExcelHandler : MonoBehaviour
{
    #region Variables
    // ===========================
    // Private
    // ===========================
    private string excelName;
    private string timeElapse;
    private LevelManager levelManager;
    // ===========================
    // [SerializeField] private
    // ===========================
    [Header("Text Fields to Save")]
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI grade;

    #endregion

    // =====================
    // Unity Methods
    // =====================
    #region Unity Methods
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        RunTimeTest();
    }
    #endregion

    // =====================
    // Own Methods
    // =====================
    #region Own Methods

    // Method that creates and excel file and sheet on start
    void RunTimeTest()
    {
        DateTime dt = DateTime.Now;
        excelName = dt.ToString("yyyy-MM-dd") + ".xls";

        string path = Application.dataPath + "/Output/";

        if (!Directory.Exists(path))
        {
            Debug.Log("Create Directory");
            Directory.CreateDirectory(path);
        }

        Debug.Log("streaming assets: " + Application.streamingAssetsPath);
        Debug.Log("FILE IO path: " + path + excelName);

        if (System.IO.File.Exists(path + excelName))
        {
            Debug.Log("File Exist: [" + path + "]");
            //*****
            HSSFWorkbook book;
            using (FileStream file = new FileStream(@path + excelName, FileMode.Open, FileAccess.Read))
            {
                book = new HSSFWorkbook(file);
                file.Close();
            }

            ISheet sheet = book.GetSheetAt(0);

            using (FileStream file = new FileStream(@path + excelName, FileMode.Open, FileAccess.Write))
            {
                book.Write(file);
                file.Close();
            }
        }
        else
        {
            Debug.Log("File DOES NOT Exist");

            //*****
            IWorkbook book = new HSSFWorkbook(); ;

            ISheet sheet = book.CreateSheet("Batch" + dt.ToString("yyyy-MM-dd"));

            // Row population
            sheet.CreateRow(0).CreateCell(0).SetCellValue("Attempt");
            sheet.CreateRow(0).CreateCell(1).SetCellValue("Level Name");
            sheet.GetRow(0).CreateCell(2).SetCellValue("Time Elapse");
            sheet.GetRow(0).CreateCell(3).SetCellValue("Final Score");
            sheet.GetRow(0).CreateCell(4).SetCellValue("Grade");

            sheet.CreateRow(sheet.LastRowNum + 1).CreateCell(0).SetCellValue("-END-");
            Debug.Log("Last ROW: " + sheet.LastRowNum);

            //save
            FileStream xfile = File.Create(path + excelName);
            book.Write(xfile);
            xfile.Close();
        }
    }


    // Method to write in-game data to created excel file
    private void WriteToExcel()
    {
        // get the time elapse and convert it
        TimeSpan time = TimeSpan.FromSeconds(levelManager.timePast);
        timeElapse = time.ToString("hh' : 'mm' : 'ss");

        // write to excel
        DateTime dt = DateTime.Now;
        excelName = dt.ToString("yyyy-MM-dd") + ".xls";

        string path = Application.dataPath + "/Output/";

        // Check if Directory path exists
        if (!Directory.Exists(path))
        {
            Debug.Log("Create Directory");
            Directory.CreateDirectory(path);
        }

        Debug.Log("streaming assets: " + Application.streamingAssetsPath);
        Debug.Log("FILE IO path: " + path + excelName);

        // IF EXIST
        if (System.IO.File.Exists(path + excelName))
        {
            Debug.Log("File Exist: [" + path + "]");
            //*****
            HSSFWorkbook book;
            using (FileStream file = new FileStream(@path + excelName, FileMode.Open, FileAccess.Read))
            {
                book = new HSSFWorkbook(file);
                file.Close();
            }

            ISheet sheet = book.GetSheetAt(0);

            // Get current row
            IRow hRow = sheet.GetRow(0);

            // Create a new row
            IRow row = sheet.CreateRow(sheet.LastRowNum);

            // Populate data
            row.CreateCell(0).SetCellValue(sheet.LastRowNum);
            row.CreateCell(1).SetCellValue(levelManager.currentLevelName);
            row.CreateCell(2).SetCellValue(timeElapse);
            row.CreateCell(3).SetCellValue(finalScore.text);
            row.CreateCell(4).SetCellValue(grade.text);
            sheet.CreateRow(sheet.LastRowNum + 1).CreateCell(0).SetCellValue("-END-");

            using (FileStream file = new FileStream(@path + excelName, FileMode.Open, FileAccess.Write))
            {
                book.Write(file);
                file.Close();
            }
        }
    }


    public void ButtonSave()
    {
        WriteToExcel();
    }
    #endregion
}
