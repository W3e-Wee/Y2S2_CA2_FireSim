using UnityEngine;
using TMPro;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-13
// Description	: Script to spawn gameobject onto Player's hands
//---------------------------------------------------------------------------------
public class PlayerInventory : MonoBehaviour
{
    #region Variables
    //===================
    // Public Variables
    //===================
    public GameObject itemPrefab;
    public int maxSpawn;
    public int counter;

	[Header("UI")]
	[SerializeField] private TextMeshProUGUI maxSpawnCountText;
	[SerializeField] private TextMeshProUGUI currentSpawnCountText;
    #endregion

	#region Unity Methods
	protected void Start()
	{
		counter = 0;
	}

	protected void Update()
	{
		// update the UI
		maxSpawnCountText.text = maxSpawn.ToString();
		currentSpawnCountText.text = counter.ToString();
	}
	#endregion

    #region Own Methods
    public void SpawnGameObjectOnHand()
    {
        if (counter == maxSpawn)
        {
			Debug.Log("[PlayerInventory] - Reached max spwan count");
            return;
        }
        else
        {
            GameObject runeClone = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            counter++;
        }

    }
    #endregion

}
