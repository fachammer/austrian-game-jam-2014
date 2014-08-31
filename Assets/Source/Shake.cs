using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour {


    float shake = 0;
    float shakeAmount = 0.3f;
    float decreaseFactor = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (shake > 0) {
            transform.localPosition = Random.insideUnitSphere * shakeAmount;
            shake -= Time.deltaTime * decreaseFactor;

        }
        else {
            shake = 0.0f;
        }
	}

    public void DoShake(float intensity) {
        shakeAmount = intensity;
        shake = intensity;
    }
}
