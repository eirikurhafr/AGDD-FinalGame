using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreakScript : MonoBehaviour {

    public GameObject scroll;

    private void Start()
    {
        scroll.gameObject.SetActive(false);
    }

    public void explosionFunction()
    {
        scroll.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
