using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    float terminalVelocity = 6;
    float lateralAcceleration = 5;
    float lateralTopSpeed = 3;

    Rigidbody2D rigid;
    Vector2 edgeOffset;

    float coolDown = 2f;
    float nextTime = 0f;

    //True = right, False = left
    bool direction;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        direction = Random.value > 0.5;
        edgeOffset = new Vector2(GetComponent<Collider2D>().bounds.extents.x+0.1f, -1f);
    }
	
    // Update is called once per frame
    void FixedUpdate () {
        //Apply our top speeds
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

        //Apply some friction
        if (createMovementInput() == 0)
            rigid.velocity = new Vector2(vel.x*0.9f, vel.y);

        //Do movement
        rigid.AddForce(new Vector2(createMovementInput()*lateralAcceleration, 0), ForceMode2D.Impulse);

        if (Time.time > nextTime) {
            foreach (Collider2D coll in Physics2D.OverlapCircleAll(transform.position, 2f)) {
                PlayerHealth h = coll.GetComponent<PlayerHealth>();
                if (h != null) {
                    h.RemoveHealth(1);
                    nextTime = Time.time+coolDown;
                    break;
                }
            }
        }

        Debug.Log(direction);
    }

    float createMovementInput () {
        if (direction) {
            if (Physics2D.Raycast((Vector2)transform.position+edgeOffset, Vector2.right, 1f))
                direction = !direction;
            else
                return 1f;

        } else {
            if (Physics2D.Raycast((Vector2)transform.position-edgeOffset, Vector2.left, 1f))
                direction = !direction;
            else
                return -1f;
        }
        return 0;
    }
}
