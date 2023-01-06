using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using UnityEngine.InputSystem;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-06
// Description	: Script for Water rune mechanic and visuals
//---------------------------------------------------------------------------------

public class WaterRune : MonoBehaviour
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
    [SerializeField] private ParticleSystem waterParticle;
    // [SerializeField] private PlayerInput pI;
    [SerializeField, Range(0f, 1f)] private float amountExtinguishedPerSec = .5f;
    [SerializeField] private float manaCount = 100f;
    [SerializeField] private float manaDecreaseAmt = 0f;

    //===================
    // Private Variables
    //===================
    private GauntletInputManager gauntletInput;
    private int layerMask;
    private RaycastHit hit;
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
        if (gauntletInput.runeActive && gauntletInput.isWaterActive)
        {
            Extinguish();

        }// End of IF check
    }
    #endregion
    #region Own Methods
	
    /// <summary>
    /// Fires Raycast and calls FireExtinguishing
    /// If in contact with a gameObject with Fire script
    /// </summary>
    private void Extinguish()
    {
        // Fire raycast
        if (Physics.Raycast(rayFirePos.position, rayFirePos.forward, out hit, rayRange, layerMask))
        {
            // Check for Fire script in gameObject
            if (hit.collider.TryGetComponent(out Fire fire))
            {
                // Extinguish fire
                print("Extinguishing Fire");

                // Play water particle here

                fire.FireExtinguishing(amountExtinguishedPerSec * Time.deltaTime);
            }
            else
            {
                // Stop water particle here

            }// End of IF...ELSE

            // Decrease mana
            DecreaseMana(manaDecreaseAmt);

        } // End of IF check
    }// End of Extinguish

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

        if (mana <= 0)
        {
            // StopParticle();
            OnLowGauge();

        }// End of IF check
    }// End of DecreaseGauge

    /// <summary>
    /// Called when manaCount hits 0
    /// </summary>
    private void OnLowGauge()
    {
        // destroy rune object
        Destroy(this.gameObject);

    }// End of OnLowGauge 
    #endregion

}
