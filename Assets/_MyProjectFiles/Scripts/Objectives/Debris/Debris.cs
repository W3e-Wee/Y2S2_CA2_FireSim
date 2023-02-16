using UnityEngine;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-16
// Description	: Script that manages Debris methods
//---------------------------------------------------------------------------------

public class Debris : MonoBehaviour
{
    #region Variables
    //===================
    // Public Variables
    //===================
    public string debrisId;
    public bool cleared = false;

    //====================================
    // [SerializeField] Private Variables
    //====================================
    [SerializeField] private float totalClearGauge = 0f;
    [SerializeField] private Vector3 debrisReduction;
    #endregion

    #region Unity Methods

    #endregion

    #region Own Methods
    public void ClearingDebris(float clearAmount)
    {
        print("Clearing Debris...");

        // decrease gauge
        totalClearGauge -= clearAmount;

        // update progress bar

        // reduce scale of debris
        // ReduceScale(debrisReduction);

        if (totalClearGauge <= 0)
        {
            cleared = true;
            // show an effect

            // update task
        }
    }

    private void ReduceScale(Vector3 reduceNum)
    {
        Vector3 debrisScale = this.gameObject.transform.localScale;
        debrisScale -= reduceNum;
    }

    [ContextMenu("Genrate GUID")]
    private void GenerateGUID()
    {
        debrisId = System.Guid.NewGuid().ToString();
    }
    #endregion

}
