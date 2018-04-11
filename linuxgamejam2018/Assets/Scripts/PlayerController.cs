using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float terminalVelocity = 6;
    float lateralAcceleration = 5;
    float lateralTopSpeed = 5;
    float jumpAcceleration = 25;

    Rigidbody2D rigid;
    bool canJump;
    Vector2 groundOffset;
    bool isTouchingGround;

    GameObject runParticles;
    GameObject burstParticles;

    EnumPlayerState state;
    public float range;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody2D>();
        runParticles = transform.Find("runParticles").gameObject;
        burstParticles = transform.Find("burstParticles").gameObject;
        groundOffset = new Vector2(0, GetComponent<Collider2D>().bounds.extents.y+0.1f);
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
        if (Input.GetAxis("Horizontal") == 0 && canJump)
            rigid.velocity = new Vector2(vel.x*0.9f, vel.y);

        //Do movement
        rigid.AddForce(new Vector2(Input.GetAxis("Horizontal")*lateralAcceleration, 0), ForceMode2D.Impulse);

        //Jump
        if (Input.GetButtonDown("Jump"))
            Jump();
        if (Input.GetButtonDown("Fire1"))
            Attack();

        isTouchingGround = Physics2D.Raycast((Vector2)transform.position-groundOffset, -Vector2.up, 0.1f);

        if (Mathf.Abs(vel.x) == lateralTopSpeed)
            state = EnumPlayerState.RUNNING;
        else
            state = EnumPlayerState.WALKING;

        if (!isTouchingGround)
            state = EnumPlayerState.FLYING;

        switch (state) {
            case EnumPlayerState.WALKING:
                range = 1.5f;
                break;
            case EnumPlayerState.RUNNING:
                range = 5f;
                break;
            case EnumPlayerState.FLYING:
                range = 5f;
                break;
        }
        
        Debug.Log(state);
        runParticles.SetActive((isTouchingGround && Mathf.Abs(vel.x)+0.5 > lateralTopSpeed));
    }

    void Jump() {
        //Only jump if we're on a surface
        if (canJump) {
            rigid.AddForce(new Vector2(0, jumpAcceleration), ForceMode2D.Impulse);
            burstParticles.GetComponent<ParticleSystem>().Play();
            canJump = false;
        }
    }

    void OnCollisionEnter2D (Collision2D collision) 
    {
        burstParticles.GetComponent<ParticleSystem>().Play();
        canJump = true;
    }

    void Attack() {
        Debug.Log("attack");
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        Vector2 pos2 = new Vector2(pos.x, pos.y);
        Collider2D[] colls = Physics2D.OverlapCircleAll(pos2, 3.5f);

        switch (state) {
            case EnumPlayerState.WALKING:
                foreach (Collider2D coll in colls) {
                    Debug.Log(coll.name);
                    Debug.Log(Vector2.Distance(transform.position, coll.transform.position));
                    if (Vector2.Distance(transform.position, coll.transform.position) < range) {
                        if (coll.tag == "Enemy")
                            coll.GetComponent<EnemyHealth>().RemoveHealth(1);
                    }
                }
                break;

            case EnumPlayerState.RUNNING:
                foreach (Collider2D coll in colls) {
                    Debug.Log(coll.name);
                    Debug.Log(Vector2.Distance(transform.position, coll.transform.position));
                    if (Vector2.Distance(transform.position, coll.transform.position) < range) {
                        if (coll.tag == "Enemy") {
                            coll.GetComponent<EnemyHealth>().RemoveHealth(1);
                            rigid.AddForce((Vector2)(coll.transform.position - transform.position).normalized * 50, ForceMode2D.Impulse);
                        }
                    }
                }
                break;
            case EnumPlayerState.FLYING:
                foreach (Collider2D coll in colls) {
                    Debug.Log(coll.name);
                    Debug.Log(Vector2.Distance(transform.position, coll.transform.position));
                    if (Vector2.Distance(transform.position, coll.transform.position) < range) {
                        if (coll.tag == "Enemy") {
                            coll.GetComponent<EnemyHealth>().RemoveHealth(2);
                            rigid.AddForce((Vector2)(coll.transform.position - transform.position).normalized * 50, ForceMode2D.Impulse);
                        }
                    }
                }
                break;

        }
    }
      
}
