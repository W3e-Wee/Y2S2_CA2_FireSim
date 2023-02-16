using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2022-12-24
// Description	: Script that handles Fire logic 
//---------------------------------------------------------------------------------

public class Fire : MonoBehaviour
{
    public string fireId = string.Empty;
    #region Variables
    //====================================
    // [SerializeField] Private Variables
    //====================================
    [Header("Fire Particle Settings")]
    [SerializeField, Range(0f, 1f)] private float fireIntensity = 1.0f;
    [SerializeField] private ParticleSystem[] fireParticleSystem = new ParticleSystem[0];

    [Header("Fire Regen Settings")]
    [SerializeField, Range(0f, 5f)] private float fireRegenRate = .2f;
    [SerializeField, Range(0f, 5f)] private float fireRegenDelay = 2.5f;

    [Header("Steam Particle Setting")]
    [SerializeField] private ParticleSystem steamParticles;
    [SerializeField] private Transform steamEmissionPoint;

    [Space]
    [SerializeField] private Image barFill;
    public bool extinguished = false;

    //====================================
    // Private Variables
    //====================================
    private float[] startIntensities = new float[0];
    private float timeLastExtinguished = 0f;
    private bool isLit = true;
    private LevelManager levelManager;


    #endregion

    #region Unity Methods
    protected void Start()
    {
        // Set progress bar
        // barFill.fillAmount = fireIntensity;

        // Populate variable
        startIntensities = new float[fireParticleSystem.Length];

        // Sets the start intensity for each fireParticle in scene
        for (int i = 0; i < fireParticleSystem.Length; i++)
        {
            startIntensities[i] = fireParticleSystem[i].emission.rateOverTime.constant;
        }

        // Get the level manager
        try{
            levelManager = FindObjectOfType<LevelManager>();
        }
        catch(Exception e)
        {
            Debug.LogError("[Fire] - LevelManger is not present in scene: " + e.Message);
        }
        
    }
    protected void Update()
    {
        // Checks if fire still has intensity
        if ((isLit && fireIntensity < 1.0f) && (Time.time - timeLastExtinguished >= fireRegenDelay))
        {
            fireIntensity += fireRegenRate * Time.deltaTime;

            // Set progress bar
            // barFill.fillAmount = fireIntensity;

            // regen fire
            ChangeIntensity();

        }// End of IF check


        // FOR TESTING
        // if (extinguished)
        // {
        //     levelManager.UpdateFireState(fireId, extinguished);
        // }
    }
    #endregion

    #region Own Methods
    // ===================
    // Public
    // ===================

    /// <summary>
    /// Called to manage fire particle's state
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>Returns true when intensity hits 0</returns>
    public void FireExtinguishing(float amount)
    {
        // decrease fire intensity
        fireIntensity -= amount;

        // update progress bar
        // barFill.fillAmount = fireIntensity;

        // set last extinguished to current time
        timeLastExtinguished = Time.time;

        // Decrease fire particles
        ChangeIntensity();

        // Fire gone
        if (fireIntensity <= 0)
        {
            // Set isLit to false;
            isLit = false;

            // Set extinguished to true
            extinguished = true;

            // Show steam particle
            EmitSteam();

            // Change layer
            int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
            gameObject.layer = LayerIgnoreRaycast;

            // Invoke method to inform LevelManager
            levelManager.UpdateFireState(fireId, extinguished);

            return;
        }

        return;
    }// End of FireExtinguishing

    /// <summary>
    /// Decreases the fire particle's rate over time
    /// </summary>
    public void ChangeIntensity()
    {
        // decrease fire particle
        for (int i = 0; i < fireParticleSystem.Length; i++)
        {
            var emission = fireParticleSystem[i].emission;
            emission.rateOverTime = fireIntensity * startIntensities[i];
        }

    }// End of ChangeIntensity

    // ===================
    // Private
    // ===================

    /// <summary>
    /// Called when particle rate over time is 0
    /// </summary>
    private void EmitSteam()
    {
        // Play steam particle at center of fire
        var emitParams = new ParticleSystem.EmitParams();
        emitParams.position = steamEmissionPoint.position;
        steamParticles.Emit(emitParams, 1);

    }// End of EmitSteam

    [ContextMenu("Generate GUID")]
    private void GenerateGUID()
    {
        Guid fireGuid = Guid.NewGuid();
        fireId = fireGuid.ToString();
    }
    #endregion

}
