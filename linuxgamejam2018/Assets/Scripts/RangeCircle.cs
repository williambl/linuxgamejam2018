using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCircle : MonoBehaviour {

    PlayerController pc;

    // Use this for initialization
    void Start () {
        pc = transform.parent.GetComponent<PlayerController>();
    }
	
    // Update is called once per frame
    void Update () {
        transform.localScale = new Vector3(pc.range, pc.range, 0);	
    }
}
