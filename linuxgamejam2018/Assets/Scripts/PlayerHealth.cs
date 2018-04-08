using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public int health = 5;

    // Update is called once per frame
    void Update () {
		
    }

    public void RemoveHealth() {
        health--;
    }
}
