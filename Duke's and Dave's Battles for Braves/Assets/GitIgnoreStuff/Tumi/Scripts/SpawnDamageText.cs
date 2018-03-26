using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDamageText : MonoBehaviour {
    public DamageText damageText;
    private DamageText temp;

    public void spawnText(string text)
    {
        temp = Instantiate(damageText, transform);
        temp.SetText(text);
    }
}
