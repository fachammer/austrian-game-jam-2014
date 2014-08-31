using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class healthText : MonoBehaviour {

    Health playerHealth;

	void Start () {
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = playerHealth.CurrentHealth.ToString();


	}
}
