using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRangeCircle : MonoBehaviour {

    Camera cam;

    // Use this for initialization
    void Start () {
	cam = Camera.main;
    }
	
    // Update is called once per frame
    void Update () {
	transform.position = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.2f);
    }
}
