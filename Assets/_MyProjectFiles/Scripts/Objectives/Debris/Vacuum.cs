using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng & Xuan Wei
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class Vacuum : MonoBehaviour
{
    #region Variables
    //====================================
    // [SerializeField] Private Variables
    //====================================
    [SerializeField] private float clearRate = 0f;
    [SerializeField] private string targetTag = string.Empty;
    [SerializeField] private ParticleSystem blackhole;
    [SerializeField] private float delay;

    //===================
    // Private Variables
    //===================

    #endregion

    #region Unity Methods
    protected void Start()
    {
        blackhole.gameObject.SetActive(false);
        blackhole.Stop();
    }

    protected void Update()
    {

    }
    #endregion

    #region Own Methods
    private void OnCollisionEnter(Collision collision)
    {
        // ignore specific gameObjects
        if (collision.gameObject.CompareTag("Debris"))
        {
            blackhole.gameObject.SetActive(true);
            blackhole.Play();
            Invoke("StopParticles", delay);
        }

        //blackhole.gameObject.SetActive(true);
        //blackhole.Play();
        //Invoke("StopParticles", delay);
    }

    private void StopParticles()
    {
        blackhole.Stop();
    }
    #endregion

}
