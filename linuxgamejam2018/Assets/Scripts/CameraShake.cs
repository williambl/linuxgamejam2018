using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    //Based on https://forum.unity.com/threads/screen-shake-effect.22886/#post-153233
    Camera cam;
    public float shakiness = 0f;
    float overallShakiness = 0.4f;
    float shakeDecrease = 3.0f;
    Vector3 originalCameraPos;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        originalCameraPos = cam.transform.localPosition;        
    }
	
    // Update is called once per frame
    void Update () {
        if (shakiness > 0)  {
            Vector2 rawShake = Random.insideUnitCircle * overallShakiness;
            cam.transform.localPosition = new Vector3(rawShake.x, rawShake.y, -10);
            shakiness -= Time.deltaTime * shakeDecrease;
        } else {
            shakiness = 0f;
            cam.transform.localPosition = originalCameraPos;
        }
    }
}
