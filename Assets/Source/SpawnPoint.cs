using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	public Transform spawnObject;

	public float interval = 0f;
	private float timer;

	void Start () {
		Spawn ();
	}

	public void Spawn () {
		Instantiate (spawnObject, transform.position, spawnObject.rotation);
	}
	
	void Update () {
	
		if (interval > 0) {
			timer += Time.deltaTime;
			if (timer >= interval) {
				Spawn();
				timer = 0;
			}
		}

	}
}
