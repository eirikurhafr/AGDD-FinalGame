using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetInventory : MonoBehaviour {
    public PlayerController player;
    private Text inventory;
    // Use this for initialization
    void OnEnable()
    {
        inventory = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        inventory.text = "";
		foreach(string item in player.Inventory)
        {
            inventory.text += item + "\n";
        }
	}
}
