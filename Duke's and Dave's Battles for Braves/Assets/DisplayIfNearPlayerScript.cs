using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayIfNearPlayerScript : MonoBehaviour {
    private bool found;
    public GameObject panel;

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5f);
        found = false;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].name == "Player_1" || hitColliders[i].name == "Player_2")
            {
                found = true;
                break;
            }
        }

        if (found)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}
