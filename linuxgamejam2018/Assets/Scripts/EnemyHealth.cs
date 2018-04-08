using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health = 5;

    // Use this for initialization
    void Start () {
		
    }
	
    // Update is called once per frame
    void Update () {
        if (health == 0)
            Destroy(gameObject);        
    }

    public void RemoveHealth () {
        health--;
    }
}
