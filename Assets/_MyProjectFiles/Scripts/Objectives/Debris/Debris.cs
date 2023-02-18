using UnityEngine;

//---------------------------------------------------------------------------------
// Author		: Wee Heng & Xuan Wei
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
    //[SerializeField] private Vector3 debrisReduction;
    [SerializeField] private float shrinkSpeed;

    //[SerializeField] private Vector3 vector3;
    [SerializeField] private float clearAmt;

    //====================================
    // Private Variables
    //====================================
    [SerializeField] private float axisX;
    private float axisY;
    private float axisZ;
    private LevelManager levelManager;
    #endregion

    #region Unity Methods
    private void Start()
    {
        axisX = transform.localScale.x;
        axisY = transform.localScale.y;
        axisZ = transform.localScale.z;

        levelManager = FindObjectOfType<LevelManager>();
    }
    #endregion

    #region Own Methods
    public void ClearingDebris(float clearAmount)
    {
        print("Clearing Debris...");

        // decrease gauge
        totalClearGauge -= clearAmount;

        // update scale axis
        axisX = transform.localScale.x;
        axisY = transform.localScale.y;
        axisZ = transform.localScale.z;

        // reduce scale of debris
        if (axisX > 0 && axisY > 0 && axisZ > 0)
        {
            transform.localScale = new Vector3((axisX - shrinkSpeed), (axisY - shrinkSpeed), (axisZ - shrinkSpeed));
        }

        if (totalClearGauge <= 0)
        {
            cleared = true;
            clearAmount = 0;
            transform.localScale = new Vector3(0, 0, 0);

            // show an effect

            // update task
            levelManager.UpdateDebrisState(debrisId, cleared);
            
            // destroy gameobject
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Vacuum"))
        {
            print("in range of vacuum");
            ClearingDebris(clearAmt * Time.deltaTime);
        }
    }

    //private void ReduceScale(Vector3 reduceNum)
    //{
    //    //Vector3 debrisScale = this.gameObject.transform.localScale;
    //    Vector3 debrisScale = transform.localScale;
    //    //debrisScale -= reduceNum;
    //}

    [ContextMenu("Genrate GUID")]
    private void GenerateGUID()
    {
        debrisId = System.Guid.NewGuid().ToString();
    }
    #endregion

}
