using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreakScript : MonoBehaviour {

    public GameObject scroll;
    public GameObject spawnObject;
    private float timer;
    private float counter;
    private bool spawnerCheck;

    private void Start()
    {
        scroll.gameObject.SetActive(false);
        timer = 20f;
        counter = 0;
        spawnerCheck = true;
    }

    public void explosionFunction()
    {
        scroll.gameObject.SetActive(true);
        Destroy(gameObject);
    }
    

    private void Update()
    {
        counter += Time.deltaTime;
        if(counter >= timer)
        {
            counter = 0;
            spawnerCheck = true;
        }
    }

    private void OnTriggerStay(Collider other)
{
        if ((other.gameObject.name == "Player_1" || other.gameObject.name == "Player_2") && spawnerCheck)
        {
            Instantiate(spawnObject, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
            spawnerCheck = false;
        }
    }
}
