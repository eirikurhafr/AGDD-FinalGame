using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBossCollider : MonoBehaviour {

    [SerializeField] GameObject BadDude;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player_1" || other.name == "Player_2")
        {
            BadDude.SendMessage("hurt", 1);
            gameObject.SetActive(false);
        }
    }
}
