using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderScript : MonoBehaviour {
    private Slider slider;
    public GameObject hurtImage;
    public PlayerController player;
    private float oldHealth;
    private float time;
    private float flashTime = 0.5f;

	// Use this for initialization
	void Start () {
        slider = gameObject.GetComponent<Slider>();
        oldHealth = player.health;
        slider.value = player.health;
    }
	
	// Update is called once per frame
	void Update () {
        if (oldHealth > player.health)
        {
            time = flashTime;
            slider.value = player.health;
            hurtImage.SetActive(true);
            oldHealth = player.health;
        }
        else
        {
            slider.value = player.health;
        }
        
        if(time > 0)
        {
            time -= Time.deltaTime;
        }
        else if (time < 0)
        {
            hurtImage.SetActive(false);
        }
	}
}
