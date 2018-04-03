using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameZone : MonoBehaviour {

    [SerializeField]
    private float coolDown;

    [SerializeField]
    private float duration;

    [SerializeField]
    private float currCoolD;

    [SerializeField]
    private float damage;

    private bool doDamage;

    private GameObject flameEffect;
    private float isOn;

	// Use this for initialization
	void Start () {
	    doDamage = false;
        flameEffect = this.gameObject.transform.GetChild(0).gameObject;
	    isOn = 0f;
	    flameEffect.active = false;
	}
	
	// Update is called once per frame
	void Update () {

	    currCoolD -= Time.deltaTime;
        isOn -= Time.deltaTime;

        if (currCoolD <= 0f || isOn >= 0.01f) {
	        if (isOn <= 0f) {
	            isOn = duration;
	            doDamage = true;
	        }
            if (!flameEffect.active) {
                flameEffect.active = true;
            }
            currCoolD = coolDown;
	    } else {
            if (flameEffect.active) {
                flameEffect.active = false;
                doDamage = false;
            }
        }
	    
    }

    void OnTriggerStay(Collider collision) {
        if (collision.gameObject.tag == "Player_1" || collision.gameObject.tag == "Player_2") {
            if (doDamage) {
                collision.SendMessage("hurtFunction", damage);
            }
        }
    }

}
