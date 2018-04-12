using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health {

    public int maxHealth = 5;

    public SpriteRenderer rend;

    public GameObject damageIndicator;
    public GameObject deathCanvas;
    
    PlayerController pc;

    // Use this for initialization
    void Start () {
        health = maxHealth;
        rend = GetComponent<SpriteRenderer>();
        pc = GetComponent<PlayerController>();
    }
	
    // Update is called once per frame
    void Update () {
        Material mat = rend.material;
        Color color = mat.color;
        color.a = ((float)health/(float)maxHealth);
        mat.color = color;
        rend.material = mat;

        if (health <= 0) {
            deathCanvas.SetActive(true);
            pc.state = EnumPlayerState.DEAD;
        }
    }

    void OnCollisionEnter2D (Collision2D collision) {
        if (collision.otherCollider.tag == "Enemy")
            RemoveHealth(1);
    }

    public new void RemoveHealth (int toRemove) {
        health -= toRemove;
        if (health < 0)
            toRemove -= Mathf.Abs(health);

        for (int i = 0; i<toRemove; i++){
            GameObject spawnedIndicator = Instantiate(damageIndicator);
            spawnedIndicator.transform.position = (Vector2)transform.position+Random.insideUnitCircle*2;
        }
        StartCoroutine(DamageEffects());
        Camera.main.GetComponent<CameraShake>().shakiness = (float)toRemove;
    }

    IEnumerator DamageEffects() {
        Material mat = rend.material;
        Color color = mat.color;
        mat.color = Color.white;
        rend.material = mat;
        yield return new WaitForSeconds(0.1f);
        mat = rend.material;
        mat.color = color;
        yield break;
    }
}
