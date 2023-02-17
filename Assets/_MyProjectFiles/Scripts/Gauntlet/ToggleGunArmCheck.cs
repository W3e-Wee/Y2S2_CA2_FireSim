using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleGunArmCheck : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    public GameObject leftHand;
    private GameObject playerInventory;

    // Start is called before the first frame update
    private void Awake()
    {
        toggleReference.action.started += Toggle;
        gameObject.SetActive(false);
        print("gunarm [ ON! ]");
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
        //print("gunarm [ OFF! ]");
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);

        print(isActive);

        if (isActive == true)
        {
            leftHand.SetActive(false);
        }
        else
        {
            leftHand.SetActive(true);
        }
    }
}
