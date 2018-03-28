using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour {

    public float killTime;
    private float time;
    private bool activated = true;
    private Text text;

	// Use this for initialization
	void Start()
    {
        time = killTime;
    }

    void OnEnable()
    {
        text = gameObject.GetComponent<Text>();
    }

	// Update is called once per frame
	void Update () {
		if(activated)
        {
            time -= Time.deltaTime;
            if(time < 0)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.Translate(0,0.02f,0);
            }
        }
	}

    public void SetText(string damage)
    {
        text.text = damage;
        activated = true;
    }
}
