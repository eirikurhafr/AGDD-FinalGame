using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivate : MonoBehaviour {
    public GameObject item;

	void Activate()
    {
        Debug.Log("Activate()");
        item.SetActive(true);
    }
}
