using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBossCollider : MonoBehaviour {

    [SerializeField] GameObject BadDude;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player_1" || other.name == "Player_2")
        {
            Debug.Log(other.GetComponent<Rigidbody>().velocity.y);
            if ((other.GetComponent<Rigidbody>().velocity.y + 0.01) < -0.5 && other.transform.position.y > transform.position.y && other.GetComponent<PlayerController>().health > 0)
            {
                BadDude.SendMessage("hurt", 1);
                gameObject.SetActive(false);
            }
        }
    }
}
