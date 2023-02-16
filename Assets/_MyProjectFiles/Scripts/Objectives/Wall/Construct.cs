using UnityEngine;
using UnityEngine.UI;
using System;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-06
// Description	: Script handling wall repairing mechanic and animation
//---------------------------------------------------------------------------------
/*

	REPLACE "Visual feedback here" WITH ACTUAL VISUAL EFFECTS (animations, color change etc.)

*/
public class Construct : MonoBehaviour
{
    #region Variables
    public string wallId = string.Empty;
    //====================================
    // [SerializeField] Private Variables
    //====================================
    //[SerializeField] private Image barFill;
    [SerializeField] private float totalNeededRepair = 0f;
    [SerializeField] private float repairAmount = 0f;
    [Space(20)]
    [SerializeField] private Material dmgMaterial;
    [SerializeField] private Renderer dmgWall;
    //[SerializeField] private float alphaColor = 0.3f;
    [SerializeField][Range(0f, 1f)] private float intensity;
    [SerializeField] private float repairSpeed;
    public bool repaired = false;
    //===================
    // Private Variables
    //===================
    // private TaskManager task;
    private LevelManager levelManager;
    private Color color;
    #endregion

    #region Unity Methods
    protected void Start()
    {
        // Get TaskManager gameObject
        // task = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager>();

        // Set bar fill amount
        // barFill.fillAmount = repairAmount;

        //Color color = dmgWall.material.color;
        //color.a = alphaColor;
        //dmgWall.material.color = color;

        color = dmgWall.material.GetColor("_EmissionColor");

        // Try to get the LevelManager
        try
        {
			levelManager = FindObjectOfType<LevelManager>();
        }
		catch (Exception e)
		{
			Debug.LogError("[Construct] - LevelManager not found in scene");
		}
    }

    // FOR TESTING
    protected void Update()
    {
        if (repaired)
        {
			levelManager.UpdateWallState(wallId, repaired);
        }
    }
    #endregion

    #region Own Methods
    public bool WallRepairing(float amount)
    {
        bool isRepaired = false;

        // Increase repairAmount
        repairAmount += amount;

        // Set progress bar 
        double fillAmount = Math.Round((repairAmount / totalNeededRepair), 2);
        // barFill.fillAmount = (float)fillAmount;

        //Color color = dmgWall.material.color;
        //color.a = repairAmount;
        //dmgWall.material.color = color;

        dmgWall.material.SetColor("_EmissionColor", color * intensity);
        print(color);

        if (intensity > 0)
        {
            intensity -= repairSpeed;
        }

        if (repairAmount >= totalNeededRepair)
        {

            print("Wall Repaired");

            // Disable collider
            // Collider constructCollider = this.GetComponent<BoxCollider>();
            // constructCollider.enabled = false;

            // change the layer
            int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
            gameObject.layer = LayerIgnoreRaycast;
            // show repaired wall
            intensity = 0f;
            dmgWall.material.SetColor("_EmissionColor", color * intensity);

            // check out objective
            //task.UpdateConstructTaskList(1);

            isRepaired = true;
            repaired = true;
            //return true;
        }
        // Switch case to show repair progress
        //switch((float)fillAmount)
        //{
        //	case(.25f):
        //		print("Repair at 25%");

        //		// Visual feedback here

        //		break;
        //	case(.50f):
        //		print("Repair at 50%");

        //		// Visual feedback here

        //		break;
        //	case(.75f):
        //		print("Repair at 75%");

        //		// Visual feedback here

        //		break;
        //	case(1.00f):
        //		print("Wall Repaired");

        //		// Visual feedback here

        //		isRepaired = true; 
        //		break;
        //} // End of switch case

        return isRepaired;
    }

    [ContextMenu("Generate GUID")]
    private void GenerateGUID()
    {
        // generate unique ID
        wallId = Guid.NewGuid().ToString();
    }
    #endregion

}
