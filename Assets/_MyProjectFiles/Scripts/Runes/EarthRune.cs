using UnityEngine;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-06
// Description	: Script for Earth rune mechanics and visuals
//---------------------------------------------------------------------------------

public class EarthRune : MonoBehaviour
{
    #region Variables
    //====================================
    // [SerializeField] Private Variables
    //====================================
    [Header("Raycast Setting")]
    [SerializeField] private Transform rayFirePos;
    [SerializeField, Range(0f, 150f)] private float rayRange = 100f;
    [SerializeField] private int targetLayer = 0;

    [Space]

    [Header("Rune Setting")]
    [SerializeField] private ParticleSystem earthparticle;
    [SerializeField, Range(0f, 20f)] private float wallRepairAmount = 1f;
    [SerializeField] private float manaCount = 100f;
    [SerializeField] private float manaDecreaseAmt = 0f;
    [SerializeField] private GameObject gunArm;

    //===================
    // Private Variables
    //===================
    private int layerMask; // the target layer mask
    private RaycastHit hit; // register what raycast has hit
    private GauntletInputManager gauntletInput;

    #endregion

    #region Unity Methods
    protected void Start()
    {
        // Find and get GauntletInputManager component
        gauntletInput = GameObject.Find("GauntletInputManager").GetComponent<GauntletInputManager>();

        // Set layer mask
        layerMask = (1 << targetLayer);
    }

    protected void Update()
    {
        if (gauntletInput.runeActive && gauntletInput.isEarthActive)
        {
            // Repair wall
            FixWall();
            // Play earth particle here
            earthparticle.Play();
        }// End of IF check
        else
        {
            // Stop earth particle
            earthparticle.Stop();
        }
    }
    #endregion

    #region Own Methods
    /// <summary>
    /// Fires Raycast and calls WallRepairing
    /// If in contact with a gameObject with Construct script
    /// </summary>
    private void FixWall()
    {
        // Fire a raycast
        if (Physics.Raycast(rayFirePos.position, rayFirePos.forward, out hit, rayRange) && gunArm.activeSelf)
        {
            print("Gameobject hit: " + hit.transform.name);
            // Repair the wall
            if (hit.collider.TryGetComponent(out Construct wall))
            {
                print("Fixing Wall");
                wall.WallRepairing(wallRepairAmount * Time.deltaTime);
            } // End of IF check 2

        }
        //else
        //{

        //} // End of IF...ElSE

        // Decrease mana
        DecreaseMana(manaDecreaseAmt);

    }// End of Repair method

    /// <summary>
    /// Subtracts fron "manaCount" by "decreaseAmt"
    /// </summary>
    /// <param name="decreaseAmt"></param>
    private void DecreaseMana(float decreaseAmt)
    {
        float mana = manaCount;

        // Decrease mana
        mana -= decreaseAmt;
        float percentageDecrease = mana / manaCount;

        // When mana hits 0
        if (mana <= 0)
        {
            onLowGauge();

        }// End of IF check

    }// End of DecreaseMana method

    /// <summary>
    /// Called when manaCount hits 0
    /// </summary>
    private void onLowGauge()
    {
        // Destroy rune
        Destroy(this.gameObject);

    }// End of onLowGauge method
    #endregion
}
