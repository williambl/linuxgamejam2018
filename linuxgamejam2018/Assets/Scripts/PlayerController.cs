﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float terminalVelocity = 5;
    float lateralAcceleration = 5;
    float lateralTopSpeed = 5;
    float jumpAcceleration = 25;

    Rigidbody2D rigid;
    bool canJump;

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
        vel = rigid.velocity;
        if (vel.y > terminalVelocity)
            rigid.velocity = new Vector2(vel.x, terminalVelocity);
        if (vel.y < -terminalVelocity)
            rigid.velocity = new Vector2(vel.x, -terminalVelocity);

        rigid.AddForce(new Vector2(Input.GetAxis("Horizontal")*lateralAcceleration, 0), ForceMode2D.Impulse);

        if (Input.GetButtonUp("Jump"))
            Jump();
    }

    void Jump() {
        if (canJump)
            rigid.AddForce(new Vector2(0, jumpAcceleration), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D (Collision2D collision) 
    {
        canJump = true;
        Debug.Log("ayy");
    }
      
    void OnCollisionExit2D (Collision2D collision) 
    {
        canJump = false;
    }
}
