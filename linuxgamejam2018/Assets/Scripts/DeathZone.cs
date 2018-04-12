using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        Health health = other.GetComponent<Health>();
        if (health == null)
            return;

        PlayerHealth pHealth = other.GetComponent<PlayerHealth>();
        EnemyHealth eHealth = other.GetComponent<EnemyHealth>();
        if (pHealth != null)
            pHealth.RemoveHealth(999);
        else if (eHealth != null)
            eHealth.RemoveHealth(999);
    }
}
