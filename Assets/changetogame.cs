using UnityEngine;
using System.Collections;

public class changetogame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Jump")) {
            Application.LoadLevel("intro");
        }
	}
}
