using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health;
    public int maxHealth = 2;

    public SpriteRenderer rend;

    public GameObject damageIndicator;

    // Use this for initialization
    void Start () {
        health = maxHealth;
        rend = GetComponent<SpriteRenderer>();
    }
	
    // Update is called once per frame
    void Update () {
        Material mat = rend.material;
        Color color = mat.color;
        color.a = ((float)health/(float)maxHealth);
        mat.color = color;
        rend.material = mat;

        if (health == 0)
            Destroy(gameObject);
    }

    public void RemoveHealth () {
        health--;
        GameObject spawnedIndicator = Instantiate(damageIndicator);
        spawnedIndicator.transform.position = transform.position;
    }
}
