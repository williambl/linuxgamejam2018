using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
    }
	
    // Update is called once per frame
    void Update () {
        transform.position = (Vector2)transform.position - new Vector2(0, 0.1f);

        if (transform.position.y < -11)
            transform.position = new Vector2(transform.position.x, 36.3f);
    }
}
