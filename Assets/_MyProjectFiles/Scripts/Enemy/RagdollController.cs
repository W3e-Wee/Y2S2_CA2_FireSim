using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-13
// Description	: Controls enemy ragdoll state
//---------------------------------------------------------------------------------

public class RagdollController : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool enableRagdoll = false;

    //===================
    // Private Variables
    //===================
    private Collider[] rigColliders;
    private Rigidbody[] rigRigidbodies;

    #endregion

    #region Unity Methods
    protected void Awake()
    {
        rigColliders = GetComponentsInChildren<Collider>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();

        SetRagDollCollider(false);
        SetRigidBodyKinematic(true);
    }

    protected void Update()
    {
        if (enableRagdoll)
        {
            SetRagDollCollider(true);
            SetRigidBodyKinematic(false);
        }
        else
        {
            SetRagDollCollider(false);
            SetRigidBodyKinematic(true);
        }
    }
    #endregion

    #region Own Methods
    private void SetRagDollCollider(bool enabled)
    {
        foreach (Collider col in rigColliders)
        {
            col.enabled = enabled;
        }
    }
    private void SetRigidBodyKinematic(bool enabled)
    {
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = enabled;
        }
    }
    #endregion

}
