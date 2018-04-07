﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float fallAcceleration = 9.81f;
    float terminalVelocity = 5;
    float lateralAcceleration = 5;
    float lateralTopSpeed = 5;
    float jumpAcceleration = 10;

    Rigidbody2D rigid;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
    }
	
    // Update is called once per frame
    void FixedUpdate () {
        Vector2 vel = rigid.velocity;
        if (vel.x > lateralTopSpeed)
            rigid.velocity = new Vector2(lateralTopSpeed, vel.y);
        if (vel.x < -lateralTopSpeed)
            rigid.velocity = new Vector2(-lateralTopSpeed, vel.y);

        rigid.AddForce(new Vector2(Input.GetAxis("Horizontal")*lateralAcceleration, 0), ForceMode2D.Impulse);
    }
}
