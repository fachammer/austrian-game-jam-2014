using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	public Transform spawnObject;

	public float interval = 0f;
	private float timer;

	void Start () {
	
	}
	
	void Update () {
	
		if (interval > 0) {
			timer += Time.deltaTime;
		}

	}
}
