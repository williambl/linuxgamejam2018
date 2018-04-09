using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour {

    int timer = 160;

    // Use this for initialization
    void Start () {
		
    }
	
    // Update is called once per frame
    void Update () {
        if (timer == 0)
            Destroy(gameObject);

        transform.position = (Vector2)transform.position + new Vector2(0, 0.1f);
        timer--;
    }
}
